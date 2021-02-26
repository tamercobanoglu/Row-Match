using UnityEngine;

namespace Utils.Touch {
    public class TouchMenu : TouchManager {

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
            GetTouchEditor();
#else
		GetTouchMobile();
#endif
        }

        protected override void GetTouchEditor() {
            if (Input.GetMouseButtonDown(0)) {
                ExecuteSelect(Input.mousePosition);
            }

            if (Input.GetMouseButton(0)) {
                ExecuteSwipe(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0)) {
                ExecuteRelease(Input.mousePosition);
            }
        }

        protected override void GetTouchMobile() {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    ExecuteSelect(touch.position);
                    break;
                case TouchPhase.Moved:
                    ExecuteSwipe(touch.position);
                    break;
                case TouchPhase.Ended:
                    ExecuteRelease(touch.position);
                    break;
            }
        }

        protected override void ExecuteSelect(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null) {

            }
        }

        protected override void ExecuteSwipe(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null) {

            }
        }

        private void ExecuteRelease(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null) {

            }
        }
    }
}

