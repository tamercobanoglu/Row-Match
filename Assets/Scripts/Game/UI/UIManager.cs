using UnityEngine;
using Game.Gameplay.Level;
using Utils;
using Settings;
using DG.Tweening;

namespace Game.UI {
    public abstract class UIManager : MonoBehaviour {
        public SceneLoader SceneLoader;
        public ScreenManager ScreenManager;
        public SpriteRenderer FadeRenderer;
        public LevelInfoPack LevelInfoPack;

        protected abstract void Awake();

        protected virtual void Initialize() {
            SetScreen();
        }

        protected void SetScreen() {
            ScreenManager.Initialize();
        }

        public void Fade(FadeType fade) {
            var color = FadeRenderer.color;
            color.a = fade == FadeType.In ? 1f : 0f;
            FadeRenderer.color = color;

            FadeRenderer.enabled = true;
            FadeRenderer.DOFade((float)fade, Properties.FadeOutDuration);
        }

        public void Fade(float fade, float time) {
            FadeRenderer.enabled = true;
            FadeRenderer.DOFade((float)fade, time);
        }
    }

    public enum FadeType { 
        In = 0,
        Out = 1
    }
}