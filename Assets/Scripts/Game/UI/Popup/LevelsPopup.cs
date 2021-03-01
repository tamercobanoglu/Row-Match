using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;

namespace Game.UI.Popup {
	public class LevelsPopup : MonoBehaviour {
		public LevelsPanel LevelsPanel;
		public GameObject LevelCardPrefab;

		private UIMenu _uiManager;
		private LevelCard[] _levelCards;

		public void Initialize(UIMenu uiManager, Player player) {
			_uiManager = uiManager;
			_levelCards = new LevelCard[player.Scores.Length];

			LevelsPanel.Prepare(_levelCards.Length);
			FillLevelCardPanel(player.UnlockedLevels, player.Scores);
		}

		private void FillLevelCardPanel(bool[] unlockedLevels, int[] scores) {
			for (int i = 0; i < unlockedLevels.Length; i++) {
				var newLevelCardGo = Instantiate(LevelCardPrefab, LevelsPanel.LevelsParent);
				newLevelCardGo.transform.localPosition = new Vector3(0, -2.25f * i, 0);

				var newLevelCard = newLevelCardGo.GetComponent<LevelCard>();
				newLevelCard.Prepare(_uiManager, _uiManager.LevelInfoPack.Levels[i], unlockedLevels, scores);

				_levelCards[i] = newLevelCard;
			}
		}

		public void Popup() {
			gameObject.SetActive(true);

			/// animate
		}

		public void Disappear() {
			/// animate

			LevelsPanel.ResetPanelPos();
			gameObject.SetActive(false);
		}
	}
}

