using UnityEngine;

namespace Utils.Touch {
    public abstract class TouchManager : MonoBehaviour {
        public Camera Camera;

        protected abstract void GetTouchEditor();
        protected abstract void GetTouchMobile();
    }
}
