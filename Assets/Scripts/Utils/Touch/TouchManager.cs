﻿using UnityEngine;

namespace Utils.Touch {
    public abstract class TouchManager : MonoBehaviour {
        public Camera Camera;

        protected abstract void GetTouchEditor();
        protected abstract void GetTouchMobile();
        protected abstract void ExecuteSelect(Vector3 pos);
        protected abstract void ExecuteSlide(Vector3 pos);
        protected abstract void ExecuteTouch(Vector3 pos);
    }
}
