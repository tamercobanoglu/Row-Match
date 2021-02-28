using UnityEngine;
using Game.UI.Buttons;

namespace Utils.Touch {
    public class TouchMenu : TouchManager {
        private const string LevelsButtonTag = "LevelsButton";
        private const string PlayButtonTag = "PlayButton";
        private const string CloseButtonTag = "CloseButton";

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
            GetTouchEditor();
#else
		GetTouchMobile();
#endif
        }

        protected override void GetTouchEditor() {
            if (Input.GetMouseButtonDown(0)) {
                HandleHit(Input.mousePosition, TouchPhase.Began);
            }

            if (Input.GetMouseButton(0)) {
                HandleHit(Input.mousePosition, TouchPhase.Moved);
            }

            if (Input.GetMouseButtonUp(0)) {
                HandleHit(Input.mousePosition, TouchPhase.Ended);
            }
        }

        protected override void GetTouchMobile() {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            HandleHit(touch.position, touch.phase);
        }

        private void HandleHit(Vector3 pos, TouchPhase touchPhase) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null) {
                ExecuteTouch(hit.gameObject, hit.tag, touchPhase);
            }
        }

        private void ExecuteTouch(GameObject go, string tag, TouchPhase touchPhase) {
            switch (tag) {
                case LevelsButtonTag:
                    go.GetComponent<LevelsButton>().Operate(touchPhase);
                    break;
                case PlayButtonTag:
                    go.GetComponent<PlayButton>().Operate(touchPhase);
                    break;
                case CloseButtonTag:
                    go.GetComponent<CloseButton>().Operate(touchPhase);
                    break;
            }
        }
    }
}

