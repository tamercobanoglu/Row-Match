﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Game.Gameplay.Level;

namespace Game.UI.Popup {
	public class LevelsPopup : MonoBehaviour {
		public UIMenu UIManager;
		public Transform LevelsParent;
		public GameObject LevelCardPrefab;
		public LevelInfoPack LevelInfoPack;

		private LevelCard[] _levelCards;

		public void Initialize() {
			_levelCards = new LevelCard[PlayerInfo.Scores.Length];

			FillLevelCardPanel(UIManager, PlayerInfo.UnlockedLevels, PlayerInfo.Scores);
		}

		private void FillLevelCardPanel(UIManager uiManager, bool[] unlockedLevels, int[] scores) {
			for (int i = 0; i < unlockedLevels.Length; i++) {
				var newLevelCardGo = Instantiate(LevelCardPrefab, LevelsParent);
				newLevelCardGo.transform.localPosition = new Vector3(0, -2.25f * i, 0);

				var newLevelCard = newLevelCardGo.GetComponent<LevelCard>();
				newLevelCard.Prepare(uiManager, LevelInfoPack.Levels[i], unlockedLevels, scores);

				_levelCards[i] = newLevelCard;
			}
		}

		public void Popup() {
			UIManager.LevelsButton.gameObject.SetActive(false);
			gameObject.SetActive(true);

			/// animate
		}

		public void Disappear() {
			/// animate

			gameObject.SetActive(false);
			UIManager.LevelsButton.gameObject.SetActive(true);
		}
	}
}
