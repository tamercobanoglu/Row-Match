using UnityEngine;
using Game.UI;
using Game.Mechanics;
using Game.UI.Buttons;
using Game.UI.Menu.Popup;

namespace Utils.Touch {
	public class TouchMenu : TouchManager {
		private const string LevelsButtonTag = "LevelsButton";
		private const string PlayButtonTag = "PlayButton";
		private const string CloseButtonTag = "CloseButton";
		private const string LevelsPanelTag = "LevelsPanel";

		public UIMenu UIManager;

		private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
			GetTouchEditor();
#else
		GetTouchMobile();
#endif
		}

		protected override void GetTouchEditor() {
			if (UIManager.State == MenuState.Animating) return;

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
			if (UIManager.State == MenuState.Animating) return;
			if (Input.touchCount <= 0) return;
			var touch = Input.GetTouch(0);

			HandleHit(touch.position, touch.phase);
		}

		private void HandleHit(Vector3 pos, TouchPhase touchPhase) {
			var worldPoint = Camera.ScreenToWorldPoint(pos);
			var hit = Physics2D.OverlapPoint(worldPoint) as BoxCollider2D;

			if (hit != null) {
				ExecuteTouch(hit.gameObject, hit.tag, worldPoint, touchPhase);
			}
		}

		private void ExecuteTouch(GameObject go, string tag, Vector3 pos, TouchPhase touchPhase) {
			switch (tag) {
				case LevelsButtonTag:
					go.GetComponent<LevelsButton>().Operate(pos, touchPhase);
					break;
				case PlayButtonTag:
					go.GetComponent<PlayButton>().Operate(pos, touchPhase);
					break;
				case CloseButtonTag:
					go.GetComponent<CloseButton>().Operate(pos, touchPhase);
					break;
				case LevelsPanelTag:
					go.GetComponent<LevelsPanel>().Operate(pos, touchPhase);
					break;
			}
		}
	}
}

