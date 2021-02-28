using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class ScoreManager : MonoBehaviour {
        public UIGameplay UIManager;

        private int _score;
        private int _moveCount;

        public void Initialize(int moveCount) {
            _score = 0;
            _moveCount = 2;
        }

        public void MoveSpent() {
            _moveCount--;
            UIManager.GameplayInfo.UpdateMoveCountText(_moveCount);

			if (_moveCount < 1) {
                UIManager.EndGame();
			}
        }

        public void UpdateScore(int amount) {
            _score += amount;

            UIManager.GameplayInfo.UpdateScoreText(_score);
        }
    }
}

