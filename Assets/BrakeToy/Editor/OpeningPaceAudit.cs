using System;
using System.Collections.Generic;
using System.IO;
using BrakeToy.Core;
using UnityEngine;

namespace BrakeToy.Editor
{
    // Diagnostic only: phrase reference crossings, not human decisions or retention.
    public static class OpeningPaceAudit
    {
        [Serializable] public sealed class Window
        {
            public int seconds, phrases, uniqueFamilies, laneTransitions, centerEdgeTransitions;
            public float distance, speed, firstPhraseSeconds, meanIntervalSeconds, maxIntervalSeconds;
            public float meanAuthoredGapSeconds;
        }

        [Serializable] public sealed class Run
        {
            public int seed;
            public bool relax;
            public Window[] windows;
        }

        [Serializable] public sealed class Aggregate
        {
            public bool relax;
            public int seconds, seeds, minPhrases, maxPhrases, minFamilies, maxFamilies;
            public float meanPhrases, meanFamilies, meanIntervalSeconds, meanAuthoredGapSeconds;
            public float centerEdgeTransitionRatio;
        }

        [Serializable] private sealed class Report
        {
            public string rules = AdventureRules.Version;
            public string note = "No powers, no tape slowdown. Counts cross beat.Distance reference, not actor arrivals. 30-unit authored phrase envelope is a diagnostic assumption, not measured empty-screen time. Human decisions, warning readability and retention are not measured.";
            public Run[] runs;
            public Aggregate[] aggregates;
        }

        public static Run Measure(int seed, bool relax)
        {
            var sequence = new AdventureSequence(seed, relax);
            var beats = new List<PlaygroundBeat>();
            for (var beat = sequence.Next(); beat.Distance <= JoyfulRules.Distance(60, relax); beat = sequence.Next())
                beats.Add(beat);
            var windows = new List<Window>();
            foreach (int seconds in new[] { 10, 30, 60 })
            {
                var window = new Window { seconds = seconds, distance = JoyfulRules.Distance(seconds, relax),
                    speed = JoyfulRules.Speed(seconds, relax), firstPhraseSeconds = JoyfulRules.TimeAtDistance(24, relax) };
                var families = new HashSet<int>();
                float intervals = 0, gaps = 0;
                for (int i = 0; i < beats.Count && beats[i].Distance <= window.distance; i++)
                {
                    var beat = beats[i];
                    window.phrases++;
                    families.Add(beat.Family);
                    if (i == 0) continue;
                    var previous = beats[i - 1];
                    float interval = JoyfulRules.TimeAtDistance(beat.Distance, relax) - JoyfulRules.TimeAtDistance(previous.Distance, relax);
                    intervals += interval;
                    gaps += Mathf.Max(0, JoyfulRules.TimeAtDistance(beat.Distance, relax)
                        - JoyfulRules.TimeAtDistance(previous.Distance + 30, relax));
                    window.maxIntervalSeconds = Mathf.Max(window.maxIntervalSeconds, interval);
                    if (beat.SafeLane != previous.SafeLane) window.laneTransitions++;
                    if ((beat.SafeLane == 1) != (previous.SafeLane == 1)) window.centerEdgeTransitions++;
                }
                window.uniqueFamilies = families.Count;
                if (window.phrases > 1)
                {
                    window.meanIntervalSeconds = intervals / (window.phrases - 1);
                    window.meanAuthoredGapSeconds = gaps / (window.phrases - 1);
                }
                windows.Add(window);
            }
            return new Run { seed = seed, relax = relax, windows = windows.ToArray() };
        }

        public static void Capture()
        {
            var runs = new List<Run>();
            foreach (bool relax in new[] { false, true })
                for (int seed = 1; seed <= 100; seed++) runs.Add(Measure(seed, relax));
            var aggregates = new List<Aggregate>();
            foreach (bool relax in new[] { false, true })
            for (int w = 0; w < 3; w++)
            {
                var aggregate = new Aggregate { relax = relax, seconds = new[] { 10, 30, 60 }[w],
                    minPhrases = int.MaxValue, minFamilies = int.MaxValue };
                int transitions = 0, alternations = 0;
                foreach (var run in runs)
                {
                    if (run.relax != relax) continue;
                    var window = run.windows[w];
                    aggregate.seeds++;
                    aggregate.minPhrases = Math.Min(aggregate.minPhrases, window.phrases);
                    aggregate.maxPhrases = Math.Max(aggregate.maxPhrases, window.phrases);
                    aggregate.minFamilies = Math.Min(aggregate.minFamilies, window.uniqueFamilies);
                    aggregate.maxFamilies = Math.Max(aggregate.maxFamilies, window.uniqueFamilies);
                    aggregate.meanPhrases += window.phrases;
                    aggregate.meanFamilies += window.uniqueFamilies;
                    aggregate.meanIntervalSeconds += window.meanIntervalSeconds;
                    aggregate.meanAuthoredGapSeconds += window.meanAuthoredGapSeconds;
                    transitions += window.laneTransitions;
                    alternations += window.centerEdgeTransitions;
                }
                aggregate.meanPhrases /= aggregate.seeds;
                aggregate.meanFamilies /= aggregate.seeds;
                aggregate.meanIntervalSeconds /= aggregate.seeds;
                aggregate.meanAuthoredGapSeconds /= aggregate.seeds;
                aggregate.centerEdgeTransitionRatio = transitions > 0 ? alternations / (float)transitions : 0;
                aggregates.Add(aggregate);
            }
            File.WriteAllText(ReportPaths.Report("OPENING_PACE.json"),
                JsonUtility.ToJson(new Report { runs = runs.ToArray(), aggregates = aggregates.ToArray() }, true));
            Debug.Log("Opening pace audit: 200 seeded runs / 600 windows. Gameplay settings unchanged.");
        }
    }
}
