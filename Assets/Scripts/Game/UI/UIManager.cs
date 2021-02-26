using UnityEngine;
using Utils;

namespace Game.UI {
    public abstract class UIManager : MonoBehaviour {
        public ScreenManager ScreenManager;

        protected abstract void Awake();

        public virtual void Initialize() {
            SetScreen();
        }

        public void SetScreen() {
            ScreenManager.Initialize();
        }
    }
}