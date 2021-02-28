using Game.Gameplay.Level;
using Game.UI.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Popup {
	public class LevelCard : MonoBehaviour {
		public TextMesh LevelInfoText;
		public TextMesh ScoreInfoText;
		public PlayButton PlayButton;

		public void Prepare(UIManager uiManager, LevelInfo levelInfo, bool[] unlockedLevels, int[] scores) {
			LevelInfoText.text = $"Level {levelInfo.LevelNum} - {levelInfo.MoveCount} Moves";
			ScoreInfoText.text = SetScoreInfoText(levelInfo.LevelNum - 1, unlockedLevels, scores);

			PlayButton.Prepare(uiManager, levelInfo.LevelNum, unlockedLevels[levelInfo.LevelNum - 1]);
		}

		private string SetScoreInfoText(int levelIndex, bool[] unlockedLevels, int[] scores) {

			if (!unlockedLevels[levelIndex]) {
				return "Locked Level";
			}

			if (scores[levelIndex] == 0) {
				return "No Score";
			}

			return $"Highest Score: {scores[levelIndex]}";
		}

		public void Unlock() {
			ScoreInfoText.text = "No Score";

			PlayButton.AnimateUnlocking();
		}
	}
}

