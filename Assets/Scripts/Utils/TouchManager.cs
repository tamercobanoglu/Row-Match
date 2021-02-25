using Game.Gameplay.Board;
using Game.Gameplay.Item;
using Game.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
	public class TouchManager : MonoBehaviour {
        public Camera Camera;
        public GameBoard GameBoard;

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
            GetTouchEditor();
#else
		GetTouchMobile();
#endif
        }

        private void GetTouchEditor() {

            if (GameBoard.State == GameState.None) {

                if (Input.GetMouseButtonDown(0)) {
                    ExecuteSelect(Input.mousePosition);
                }
                //if (Input.GetMouseButtonUp(0)) {
                //    ExecuteRelease(Input.mousePosition);
                //}
            }

            else if (GameBoard.State == GameState.SelectionStarted) {
                if (Input.GetMouseButton(0)) {
                    ExecuteSwipe(Input.mousePosition);
                }
            }
        }

        private void GetTouchMobile() {
            if (Input.touchCount <= 0) return;

            var touch = Input.GetTouch(0);

            if (GameBoard.State == GameState.None) {

                if (touch.phase == TouchPhase.Began) {
                    ExecuteSelect(touch.position);
                }
                //if (touch.phase == TouchPhase.Ended) {
                //    ExecuteRelease(touch.position);
                //}
            }

            else if (GameBoard.State == GameState.SelectionStarted) {
                if (touch.phase == TouchPhase.Moved) {
                    ExecuteSwipe(touch.position);
                }
            }
        }

        private void ExecuteSelect(Vector3 pos) {
			var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;
			//var hit = Physics2D.Raycast(Camera.ScreenToWorldPoint(pos), Vector2.zero);

            if (hit != null) {
                GameBoard.ItemTapped(hit.gameObject.GetComponent<Item>());
                Debug.Log("Select executed!");
            }
        }

        private void ExecuteSwipe(Vector3 pos) {
			var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;
			//var hit = Physics2D.Raycast(Camera.ScreenToWorldPoint(pos), Vector2.zero);

			if (hit != null && GameBoard.HitItem.gameObject != hit.gameObject) {
                GameBoard.SwapAttempt(hit.gameObject.GetComponent<Item>());
                Debug.Log("Swipe executed!");
            }
        }

        private void ExecuteRelease(Vector3 pos) {

        }
    }
}

