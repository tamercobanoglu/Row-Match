using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo {
    public class ScoreManager : MonoBehaviour {
        private int _score;
        private int _moveCount;

        private UIGameplay _uiManager;

        public void Initialize(UIGameplay uiManager, int moveCount) {
            _score = 0;
            _moveCount = 10;

            _uiManager = uiManager;
        }

        public void MoveSpent() {
            _moveCount--;
            _uiManager.GameplayInfo.UpdateMoveCountText(_moveCount);

			if (_moveCount < 1) {
                _uiManager.EndGame(_score);
			}
        }

        public void UpdateScore(int amount) {
            _score += amount;

            _uiManager.GameplayInfo.UpdateScoreText(_score);
        }
    }
}

