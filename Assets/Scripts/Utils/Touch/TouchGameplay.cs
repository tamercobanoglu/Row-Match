using Game.Gameplay.Board;
using Game.Gameplay.Item;
using Game.Mechanics;
using UnityEngine;

namespace Utils.Touch {
    public class TouchGameplay : TouchManager {
        public GameBoard GameBoard;

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
            GetTouchEditor();
#else
		GetTouchMobile();
#endif
        }

        protected override void GetTouchEditor() {
            if (GameBoard.State == GameState.None) {
                if (Input.GetMouseButtonDown(0)) {
                    ExecuteSelect(Input.mousePosition);
                }
            }

            else if (GameBoard.State == GameState.SelectionStarted) {
                if (Input.GetMouseButton(0)) {
                    ExecuteSwipe(Input.mousePosition);
                }
            }
        }

        protected override void GetTouchMobile() {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            if (GameBoard.State == GameState.None) {
                if (touch.phase == TouchPhase.Began) {
                    ExecuteSelect(touch.position);
                }
            }

            else if (GameBoard.State == GameState.SelectionStarted) {
                if (touch.phase == TouchPhase.Moved) {
                    ExecuteSwipe(touch.position);
                }
            }
        }

        protected override void ExecuteSelect(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null) {
                GameBoard.ItemTapped(hit.gameObject.GetComponent<Item>());
            }
        }

        protected override void ExecuteSwipe(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null && GameBoard.HitItem.gameObject != hit.gameObject) {
                GameBoard.SwapAttempt(hit.gameObject.GetComponent<Item>());
            }
        }
    }
}

