using Game.UI.Popup;

namespace Game.UI {
	public class UIMenu : UIManager {
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

		protected override void Initialize() {
			Fade(FadeType.In);

			Player.Initialize(LevelInfoPack.Levels.Length);
			LevelsPopup.Initialize(this, Player);
		}

		public void NotFirstLaunch() {

		}
	}
}