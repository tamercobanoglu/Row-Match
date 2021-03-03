using UnityEngine;
using System.Collections;
using Game.UI.Menu;
using Game.Mechanics;
using Game.UI.Buttons;
using Game.UI.Menu.Popup;
using Settings;
using PlayerInfo;

namespace Game.UI {
	public class UIMenu : UIManager {
		public Player Player;
		public LevelsButton LevelsButton;
		public LevelsPopup LevelsPopup;
		public CelebrationPanel CelebrationPanel;
		[HideInInspector] public MenuState State;

		///  set once when the app first launched
		public static int AvailableLevels;
		private static bool _firstSceneLoading = true;
		private static bool _firstInstall = true;

		protected override void Awake() {
			State = MenuState.None;
			SetScreen();

			/// when the application is first opened
			if (_firstSceneLoading) {

				AvailableLevels = LevelInfoPack.Levels.Length;
				_firstInstall = Player.Initialize(LevelInfoPack.Levels.Length);

				/// the application is opened for the first time on this device 
				if (_firstInstall) {
					FirstInstall();
					return;
				}

				FirstSceneLoading();
				return;
			}

			Load();
		}

		private void FirstInstall() {
			_firstInstall = false;
			_firstSceneLoading = false;
			AvailableLevels = Properties.FirstInstallAvailableLevels;

			Initialize();
			Fade(FadeType.In);
		}

		private void FirstSceneLoading() {
			_firstSceneLoading = false;

			Initialize();
			Fade(FadeType.In);
		}

		private void Load() {
			StartCoroutine(LoadingProcess());
		}

		IEnumerator LoadingProcess() {
			State = MenuState.Animating;
			Initialize();
			LevelsButton.gameObject.SetActive(false);

			Fade(FadeType.In);
			yield return new WaitForSeconds(Properties.FadeOutDuration);

			if (!Player.Instance.HighestScoreAchieved) {
				yield return StartCoroutine(LevelsPopup.Animate(Player.Instance.CurrentLevel - 1));
			}

			else {
				PlayButton playButton = null;

				/// do not try to animate next play button if it does not exist
				if (Player.Instance.CurrentLevel != AvailableLevels) {
					playButton = LevelsPopup.LevelCards[Player.Instance.CurrentLevel].PlayButton;
				}

				yield return StartCoroutine(CelebrationPanel.Animate(Player.Instance.Scores[Player.Instance.CurrentLevel - 1]));

				/// lock the play button of new level temporarily
				if (playButton != null && !Player.Instance.OldLevelRecord) 
					playButton.LockTemp();

				yield return StartCoroutine(LevelsPopup.Animate(Player.Instance.CurrentLevel - 1));

				/// animate unlocking if it's locked
				if (playButton != null && !Player.Instance.OldLevelRecord) {
					yield return StartCoroutine(playButton.Animate());
				}
			}

			LevelsButton.gameObject.SetActive(true);
			State = MenuState.None;
		}

		protected override void Initialize() {
			LevelsPopup.Initialize(this, Player.Instance);
		}
	}
}