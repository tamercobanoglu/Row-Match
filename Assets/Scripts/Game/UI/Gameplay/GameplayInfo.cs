using Game.Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Game.UI.Gameplay {
    public class GameplayInfo : MonoBehaviour {
        public TextMesh HighestScoreText;
        public TextMesh ScoreText;
        public TextMesh MoveCountText;

        public void Initialize(int currentLevel, LevelInfo[] levels) {
            HighestScoreText.text = PlayerInfo.Scores[currentLevel - 1].ToString();
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

