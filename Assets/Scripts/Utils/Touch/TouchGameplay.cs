using UnityEngine;
using Game.UI;
using Game.Mechanics;
using Game.UI.Buttons;
using Game.Gameplay.Item;
using Game.Gameplay.Board;

namespace Utils.Touch {
    public class TouchGameplay : TouchManager {
        public UIGameplay UIManager;
        public GameBoard GameBoard;

        private const string ReturnButtonTag = "ReturnButton";

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
            GetTouchEditor();
#else
		GetTouchMobile();
#endif
        }

        protected override void GetTouchEditor() {
			if (UIManager.State == GameState.None) {
                if (Input.GetMouseButtonDown(0)) {
					ExecuteSelect(Input.mousePosition);
				}
			}

			else if (UIManager.State == GameState.SelectionStarted) {
				if (Input.GetMouseButton(0)) {
					ExecuteSwipe(Input.mousePosition);
				}
			}

			else if (UIManager.State == GameState.Ended) {
				if (Input.GetMouseButtonUp(0)) {
					ExecuteTouch(Input.mousePosition);
				}
			}
		}

        protected override void GetTouchMobile() {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            if (UIManager.State == GameState.None) {
                if (touch.phase == TouchPhase.Began) {
                    ExecuteSelect(touch.position);
                }
            }

            else if (UIManager.State == GameState.SelectionStarted) {
                if (touch.phase == TouchPhase.Moved) {
                    ExecuteSwipe(touch.position);
                }
            }

            else if (UIManager.State == GameState.Ended) {
                if (touch.phase == TouchPhase.Ended) {
                    ExecuteTouch(touch.position);
                }
            }
        }

        private void ExecuteSelect(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null) {
                GameBoard.ItemTapped(hit.gameObject.GetComponent<Item>());
            }
        }

        private void ExecuteSwipe(Vector3 pos) {
            var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;

            if (hit != null && GameBoard.HitItem.gameObject != hit.gameObject) {
                GameBoard.SwapAttempt(hit.gameObject.GetComponent<Item>());
            }
        }

        private void ExecuteTouch(Vector3 pos) {
            var worldPoint = Camera.ScreenToWorldPoint(pos);
            var hit = Physics2D.OverlapPoint(worldPoint) as BoxCollider2D;

            if (hit != null && hit.CompareTag(ReturnButtonTag)) {
                hit.gameObject.GetComponent<ReturnButton>().Operate(worldPoint, TouchPhase.Ended);
            }
        }
    }
}

