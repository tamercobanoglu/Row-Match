using UnityEngine;
using Settings;
using Game.UI.Buttons;
using Game.Gameplay.Level;
using TMPro;

namespace Game.UI.Menu.Popup {
	public class LevelCard : MonoBehaviour {
		public TextMeshPro LevelInfoText;
		public TextMeshPro ScoreInfoText;
		public PlayButton PlayButton;

		public void Prepare(UIManager uiManager, LevelInfo levelInfo, Skin skin, bool[] unlockedLevels, int[] scores) {
			LevelInfoText.text = $"Level {levelInfo.LevelNum} - {levelInfo.MoveCount} Moves";
			ScoreInfoText.text = SetScoreInfoText(levelInfo.LevelNum - 1, unlockedLevels, scores);

			PlayButton.Prepare(uiManager, skin, levelInfo.LevelNum, unlockedLevels[levelInfo.LevelNum - 1]);
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
	}
}

