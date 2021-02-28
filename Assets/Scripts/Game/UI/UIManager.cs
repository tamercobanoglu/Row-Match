using UnityEngine;
using Utils;
using DG.Tweening;
using Settings;

namespace Game.UI {
    public abstract class UIManager : MonoBehaviour {
        public SceneLoader SceneLoader;
        public ScreenManager ScreenManager;
        public SpriteRenderer FadeRenderer;

        protected abstract void Awake();

        protected virtual void Initialize() {
            SetScreen();
        }

        protected void SetScreen() {
            ScreenManager.Initialize();
        }

        public void Fade(FadeType fade) {
            FadeRenderer.enabled = true;
            FadeRenderer.DOFade((float)fade, Properties.FadeOutDuration);
        }
    }

    public enum FadeType { 
        In = 0,
        Out = 1
    }
}