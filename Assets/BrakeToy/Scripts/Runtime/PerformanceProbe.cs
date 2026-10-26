using System;
using System.Collections.Generic;
using System.IO;
using BrakeToy.Core;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace BrakeToy
{
    /// <summary>Explicit local performance capture. No network or unique device identifiers.</summary>
    [DefaultExecutionOrder(-500)]
    public sealed class PerformanceProbe : MonoBehaviour
    {
        public Button CaptureButton;
        public Text Label;
        public bool Capturing { get; private set; }
        private readonly float[] frameMs = new float[12000];
        private GameManager game;
        private int count;
        private float seconds, warmup;
        private bool benchmark;
        private List<ArcadeRow> rows;
        private int rowIndex;
        private float witnessX;
        private List<PlaygroundBeat> adventureBeats;
        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR && DEVELOPMENT_BUILD
            using (var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = player.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var intent = activity.Call<AndroidJavaObject>("getIntent"))
                if (intent.Call<bool>("getBooleanExtra", "brakeToyBenchmark", false))
                    IsolateBenchmark();
#endif
        }
        public void IsolateBenchmark()
        {
            // Must run before GameManager.Start initializes the career and starter powers.
            game = GetComponent<GameManager>();
            game.Review.Persist = false; game.Arcade.PersistRecords = false;
            GetComponent<PlaygroundRun>().PersistPreferences = false;
            GetComponent<ToyAudioSettings>().PersistPreferences = false;
            GetComponent<GraphicsProfileController>().PersistPreferences = false;
        }
        public void PrepareWitness()
        {
            game = GetComponent<GameManager>(); rowIndex = 0; witnessX = 0;
            if (GetComponent<AdventureEncounters>())
            {
                adventureBeats = new List<PlaygroundBeat>(256);
                var stream = new AdventureSequence(game.Arcade.CurrentSeed, game.Review.Mode == PlayMode.Relax,
                    GetComponent<AdventureEncounters>().ActiveDenseCourse);
                for(int i=0;i<256;i++)adventureBeats.Add(stream.Next());
            }
            else
            {
                rows = new List<ArcadeRow>(256);
                var stream = new ArcadeSequence(game.Arcade.CurrentSeed,true,game.Arcade.Choreographed);
                for(int i=0;i<256;i++)rows.Add(stream.Next());
            }
        }
        [Serializable] public sealed class Report
        {
            public string version, utc, deviceModel, os, cpu, gpu, profile, inputValidation;
            public int width, height, frames, framesOver50ms;
            public float gameplaySeconds, meanFps, p95FrameMs, maxFrameMs;
            public long allocatedBytes, reservedBytes;
            public bool editor, automatedWitness;
        }
        private System.Collections.IEnumerator Start()
        {
            yield return null;
            game = GetComponent<GameManager>(); CaptureButton.onClick.AddListener(BeginCapture);
#if UNITY_ANDROID && !UNITY_EDITOR && DEVELOPMENT_BUILD
            using (var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = player.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var intent = activity.Call<AndroidJavaObject>("getIntent"))
            {
                if (intent.Call<bool>("getBooleanExtra", "brakeToyBenchmark", false))
                {
                    var quality = GetComponent<GraphicsProfileController>(); quality.PersistPreferences = false;
                    quality.SetLow(intent.Call<bool>("getBooleanExtra", "brakeToyLow", false));
                    benchmark = true; game.Arcade.PersistRecords = false; game.Arcade.VaryRuns = false;
                    game.enabled = false; game.StartRun(); PrepareWitness();
                    BeginCapture();
                }
            }
#endif
        }
        public void BeginCapture()
        { count = 0; seconds = 0; warmup = 3; Capturing = true; Label.text = "ÖLÇÜM: 60 sn sürüş"; }
        private void Update()
        {
            if (!game) return;
            CaptureButton.gameObject.SetActive(game.State != RunState.Running);
            if (benchmark && Capturing)
            {
                float target;
                if(adventureBeats!=null)
                {
                    while(rowIndex<adventureBeats.Count-1&&TabletopCourseRules.ExitDistance(adventureBeats[rowIndex])<game.Arcade.Distance)rowIndex++;
                    target=TabletopCourseRules.WitnessX(adventureBeats[rowIndex],game.Arcade.Distance);
                }
                else
                {
                    while(rowIndex<rows.Count-1&&rows[rowIndex].Distance+3<game.Arcade.Distance)rowIndex++;
                    target=ArcadeRules.LaneX(rows[rowIndex].SafeLane);
                }
                float next = Mathf.MoveTowards(witnessX, target, game.Config.MaxLateralSpeed * Mathf.Min(Time.deltaTime,.1f));
                game.Arcade.Step(Mathf.Min(Time.deltaTime,.1f), witnessX,next); witnessX=next;game.Player.StopAt(next);
                if (game.State == RunState.Crashed) { Debug.LogError("Benchmark witness collision; capture rejected."); Capturing=false; return; }
            }
            if (!Capturing || game.State != RunState.Running) return;
            float dt = Time.unscaledDeltaTime;
            if (warmup > 0) { warmup -= dt; return; }
            seconds += dt; if (count < frameMs.Length) frameMs[count++] = dt * 1000;
            if (seconds >= 60 || count == frameMs.Length) Save();
        }
        private void Save()
        {
            Capturing=false;float total=0,max=0;int slow=0;
            for(int i=0;i<count;i++){total+=frameMs[i];max=Mathf.Max(max,frameMs[i]);if(frameMs[i]>50)slow++;}
            Array.Sort(frameMs,0,count);var quality=GetComponent<GraphicsProfileController>();
            var report=new Report{version=Application.version,utc=DateTime.UtcNow.ToString("O"),deviceModel=SystemInfo.deviceModel,os=SystemInfo.operatingSystem,cpu=SystemInfo.processorType,gpu=SystemInfo.graphicsDeviceName,profile=quality.Low?"low":"high",width=Screen.width,height=Screen.height,frames=count,framesOver50ms=slow,gameplaySeconds=seconds,meanFps=count*1000/Mathf.Max(1,total),p95FrameMs=frameMs[Mathf.Clamp(Mathf.CeilToInt(count*.95f)-1,0,count-1)],maxFrameMs=max,allocatedBytes=Profiler.GetTotalAllocatedMemoryLong(),reservedBytes=Profiler.GetTotalReservedMemoryLong(),editor=Application.isEditor,automatedWitness=benchmark,inputValidation="Frame pacing only; physical touch latency and thermal acceptance require separate checks."};
            string json=JsonUtility.ToJson(report,true),path=Path.Combine(Application.persistentDataPath,"performance-"+Application.version+"-"+report.profile+".json");File.WriteAllText(path,json);
            Debug.Log("BRAKE_TOY_PERFORMANCE "+json);Label.text=report.meanFps.ToString("0.0")+" FPS  •  p95 "+report.p95FrameMs.ToString("0.0")+" ms";
            if(benchmark)game.Pause();
        }
    }
}
