using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Game.UI;
using Settings;

namespace Utils {
    public class SceneLoader : MonoBehaviour {
        public UIManager UIManager;

        public void LoadScene(SceneType sceneType) {
            StartCoroutine(LoadSceneProcess(sceneType));
        }

        IEnumerator LoadSceneProcess(SceneType sceneType) {
            UIManager.Fade(FadeType.Out);

            yield return new WaitForSeconds(Properties.FadeOutDuration);

            SceneManager.LoadScene(sceneType.ToString());
        }
    }

    public enum SceneType { 
        MainScene,
        LevelScene
    }
}
