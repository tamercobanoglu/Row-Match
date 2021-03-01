using Game.UI.Menu;
using Game.Mechanics;
using Game.UI.Menu.Popup;
using UnityEngine;
using Settings;
using System.Collections;

namespace Game.UI {
	public class UIMenu : UIManager {
		public GameObject LevelsButton;
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

			Player.Initialize(LevelInfoPack.Levels.Length);
			Load();
		}

		private void FirstInstall() {
			///Debug.Log("First install!");

			_firstInstall = false;
			_firstSceneLoading = false;
			AvailableLevels = Properties.FirstInstallAvailableLevels;

			Initialize();
		}

		private void FirstSceneLoading() {
			///Debug.Log("First scene loading!");

			_firstSceneLoading = false;

			Initialize();
		}

		private void Load() {
			///Debug.Log("Scene loaded!");

			StartCoroutine(LoadingProcess());
		}

		IEnumerator LoadingProcess() {
			State = MenuState.Animating;
			Initialize();
			LevelsButton.SetActive(false);

			Fade(FadeType.In);
			yield return new WaitForSeconds(Properties.FadeOutDuration);

			if (!Properties.HighestScoreAchieved) {
				LevelsPopup.Popup();
				yield return new WaitForSeconds(LevelsPopup.AnimDuration);
			}

			else {
				/// lock the play button of new level temporarily
				var playButton = LevelsPopup.LevelCards[Properties.CurrentLevel].PlayButton;
				playButton.LockTemp();

				CelebrationPanel.Prepare(Player.Scores[Properties.CurrentLevel - 1]);
				CelebrationPanel.Animate();
				yield return new WaitForSeconds(CelebrationPanel.AnimDuration);

				CelebrationPanel.StopAnimating();
				yield return new WaitForSeconds(CelebrationPanel.DisappearingDuration);
				CelebrationPanel.Deactivate();

				LevelsPopup.Popup();
				yield return new WaitForSeconds(LevelsPopup.AnimDuration);

				playButton.AnimateUnlocking();
				yield return new WaitForSeconds(playButton.AnimDuration);
				playButton.Activate();
			}

			LevelsButton.SetActive(true);
			State = MenuState.None;
		}

		protected override void Initialize() {
			LevelsPopup.Initialize(this, Player);
		}
	}
}