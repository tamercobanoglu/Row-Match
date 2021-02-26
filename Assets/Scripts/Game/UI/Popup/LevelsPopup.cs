using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Game.Gameplay.Level;

namespace Game.UI.Popup {
	public class LevelsPopup : MonoBehaviour {
		public Transform LevelsParent;
		public GameObject LevelCardPrefab;
		public LevelInfoPack LevelInfoPack;

		private LevelCard[] _levelCards;

		public void Initialize() {
			_levelCards = new LevelCard[PlayerInfo.Scores.Length];

			FillLevelCardPanel(PlayerInfo.UnlockedLevels, PlayerInfo.Scores);
		}

		private void FillLevelCardPanel(bool[] unlockedLevels, int[] scores) {
			for (int i = 0; i < unlockedLevels.Length; i++) {
				var newLevelCardGo = Instantiate(LevelCardPrefab, LevelsParent);
				newLevelCardGo.transform.localPosition = new Vector3(0, 4 - 2.25f * i, 0);

				var newLevelCard = newLevelCardGo.GetComponent<LevelCard>();
				newLevelCard.Prepare(LevelInfoPack.Levels[i], unlockedLevels, scores);

				_levelCards[i] = newLevelCard;
			}
		}
	}
}

