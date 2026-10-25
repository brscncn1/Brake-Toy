using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrakeToy.Tests
{
    internal static class SceneTestPersistence
    {
        // sceneLoaded runs before Start: starter-power initialization must not write real saves.
        internal static IEnumerator Load(string name)
        {
            SceneManager.sceneLoaded += DisableWrites;
            try { yield return SceneManager.LoadSceneAsync(name); }
            finally { SceneManager.sceneLoaded -= DisableWrites; }
        }

        private static void DisableWrites(Scene scene, LoadSceneMode mode)
        {
            var game = Object.FindFirstObjectByType<GameManager>();
            if (!game) return;
            game.enabled = false;
            if (game.Review) game.Review.Persist = false;
            if (game.Arcade) game.Arcade.PersistRecords = false;
            var run = game.GetComponent<PlaygroundRun>();
            if (run) run.PersistPreferences = false;
            var audio = game.GetComponent<ToyAudioSettings>();
            if (audio) audio.PersistPreferences = false;
            var graphics = game.GetComponent<GraphicsProfileController>();
            if (graphics) graphics.PersistPreferences = false;
        }
    }
}
