using UnityEngine;
using Game.UI.Buttons;
using Game.UI.Popup;

namespace Game.UI {
	public class UIMenu : UIManager {
		public LevelsButton LevelsButton;
		public LevelsPopup LevelsPopup;

		private bool tempAwakeBool = true;

		protected override void Awake() {
			SetScreen();

			if (tempAwakeBool) {
				FirstLaunch();
				Initialize();
			}

			else {
				Initialize();
				NotFirstLaunch();
			}
		}

		public void FirstLaunch() {

		}

		public override void Initialize() {
			LevelsPopup.Initialize();
		}

		public void NotFirstLaunch() {

		}
	}
}