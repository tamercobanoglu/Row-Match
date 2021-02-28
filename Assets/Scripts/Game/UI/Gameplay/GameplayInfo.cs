using Game.Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Gameplay {
    public class GameplayInfo : MonoBehaviour {
        public TextMesh HighestScoreText;
        public TextMesh ScoreText;
        public TextMesh MoveCountText;

        public void Initialize(int[] scores, int currentLevel, LevelInfo[] levels) {
            HighestScoreText.text = scores[currentLevel - 1].ToString();
            MoveCountText.text = levels[currentLevel - 1].MoveCount.ToString();
        }

        public void UpdateScoreText(int score) {
            ScoreText.text = $"Score: {score}";
        }

        public void UpdateMoveCountText(int moveCount) {
            MoveCountText.text = moveCount.ToString();
        }
    }
}

