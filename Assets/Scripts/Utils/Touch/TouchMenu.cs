﻿using UnityEngine;
using Game.UI;
using Game.Mechanics;
using Game.UI.Buttons;

namespace Utils.Touch {
	public class TouchMenu : TouchManager {
		public UIMenu UIManager;

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
					ExecuteSlide(Input.mousePosition);
				}

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
					ExecuteSlide(touch.position);
				}

				if (touch.phase == TouchPhase.Ended) {
					ExecuteTouch(touch.position);
				}
			}
		}

		protected override void ExecuteSelect(Vector3 pos) {
			var worldPoint = Camera.ScreenToWorldPoint(pos);
			var hit = Physics2D.OverlapPoint(worldPoint) as BoxCollider2D;

			if (hit != null && !hit.CompareTag(BackgroundTag)) {
				hit.gameObject.GetComponent<IButton>().Operate(worldPoint, TouchPhase.Began);
			}
		}

		protected override void ExecuteSlide(Vector3 pos) {
			var worldPoint = Camera.ScreenToWorldPoint(pos);
			var hit = Physics2D.OverlapPoint(worldPoint) as BoxCollider2D;

			if (hit != null) {
				var button = hit.gameObject.GetComponent<IButton>();

				if (((MonoBehaviour)UIManager.HitButton).gameObject != hit.gameObject) {
					UIManager.HitButton.Operate(worldPoint, TouchPhase.Canceled);
				}

				else {
					button.Operate(worldPoint, TouchPhase.Moved);
				}
			}
		}

		protected override void ExecuteTouch(Vector3 pos) {
			var worldPoint = Camera.ScreenToWorldPoint(pos);
			var hit = Physics2D.OverlapPoint(worldPoint) as BoxCollider2D;

			if (hit != null) {
				hit.gameObject.GetComponent<IButton>().Operate(worldPoint, TouchPhase.Ended);
			}
		}
	}
}

