using UnityEngine;

namespace BrakeToy
{
    public enum RunState { Ready, Running, Paused, Crashed }

    public sealed class GameManager : MonoBehaviour
    {
        public GameConfig Config;
        public InputController Input;
        public PlayerController Player;
        public WorldScroller World;
        public PrototypeUI UI;
        public ArcadeDirector Arcade;
        public RunState State { get; private set; }

        private void Awake()
        {
            Application.targetFrameRate = Config.TargetFrameRate;
            QualitySettings.vSyncCount = 0;
            Screen.orientation = ScreenOrientation.Portrait;
            Player.Initialize(Config);
            World.Initialize();
            UI.Bind(this);
            if (Arcade) Arcade.Initialize(this);
            ChangeState(RunState.Ready);
        }
        public void StartRun()
        { BeginRun(false); }
        public void StartSameRoute()
        { BeginRun(true); }
        private void BeginRun(bool sameRoute)
        {
            Input.ResetCapture();
            Player.ResetRun();
            World.ResetRun();
            if (Arcade) Arcade.BeginRun(sameRoute);
            ChangeState(RunState.Running);
        }
        public void Resume()
        {
            if (State != RunState.Paused) return;
            Input.ResetCapture();
            ChangeState(RunState.Running);
        }
        public void Pause()
        {
            if (State != RunState.Running) return;
            Input.ResetCapture();
            Player.EndDrag();
            ChangeState(RunState.Paused);
        }
        private void Update()
        {
            if (State != RunState.Running) return;
            Input.Sample(Player);
            // Deliberately slow simulation on long hitches instead of jumping unseen distances.
            float dt = Mathf.Min(Time.deltaTime, .1f);
            float oldX = Player.X;
            Player.Move(dt);
            if (Arcade) { Arcade.Step(dt, oldX, Player.X); return; }
            float distance = Config.WorldSpeed * dt;
            if (CollisionManager.FirstHit(Config, World.Obstacles, oldX, Player.X, distance, out float fraction))
            {
                Player.StopAt(Mathf.Lerp(oldX, Player.X, fraction));
                World.Advance(distance * fraction);
                Input.ResetCapture();
                ChangeState(RunState.Crashed);
                return;
            }
            World.Advance(distance);
        }
        private void OnApplicationFocus(bool focused) { if (!focused) Pause(); }
        public void Crash() { Input.ResetCapture(); Player.EndDrag(); ChangeState(RunState.Crashed); }
        private void OnApplicationPause(bool paused) { if (paused) Pause(); }
        private void ChangeState(RunState state) { State = state; UI.Show(state); }
    }
}
