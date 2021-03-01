using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;
using DG.Tweening;

namespace Game.UI.Menu.Popup {
	public class LevelsPopup : MonoBehaviour {
		public LevelsPanel LevelsPanel;
		public GameObject LevelCardPrefab;
		public Transform Transform;

		[HideInInspector] public float AnimDuration;

		private UIMenu _uiManager;
		private LevelCard[] _levelCards;
		public LevelCard[] LevelCards { get { return _levelCards; } }

		public void Initialize(UIMenu uiManager, Player player) {
			AnimDuration = 0.1f;

			_uiManager = uiManager;
			_levelCards = new LevelCard[UIMenu.AvailableLevels];

			LevelsPanel.Prepare(_levelCards.Length);
			FillLevelCardPanel(UIMenu.AvailableLevels, player.UnlockedLevels, player.Scores);
		}

		private void FillLevelCardPanel(int availableLevels, bool[] unlockedLevels, int[] scores) {
			for (int i = 0; i < availableLevels; i++) {
				var newLevelCardGo = Instantiate(LevelCardPrefab, LevelsPanel.LevelsParent);
				newLevelCardGo.transform.localPosition = new Vector3(0, -2.25f * i, 0);

				var newLevelCard = newLevelCardGo.GetComponent<LevelCard>();
				newLevelCard.Prepare(_uiManager, _uiManager.LevelInfoPack.Levels[i], unlockedLevels, scores);

				_levelCards[i] = newLevelCard;
			}
		}

		public void Popup() {
			Transform.localScale = Vector3.one * 0.8f;
			gameObject.SetActive(true);
			Transform.DOScale(Vector3.one, AnimDuration);
		}

		public void Disappear() {
			Transform.DOScale(Vector3.one * 0.8f, AnimDuration);
			gameObject.SetActive(false);

			LevelsPanel.ResetPanelPos();
		}
	}
}

