using UnityEngine;
using System.Collections;
using Game.UI;
using Game.Gameplay.Item;

namespace PlayerInfo {
    public class ScoreManager : MonoBehaviour {
        private UIGameplay _uiManager;

        private int _score;
        private int _moveCount;

        public void Initialize(UIGameplay uiManager, int moveCount) {
            _uiManager = uiManager;

            _score = 0;
            _moveCount = moveCount;
        }

        public void MoveSpent() {
			if (_moveCount < 1) {
                _uiManager.EndGame(_score);
			}
        }

        public void UpdateMoveCount() {
            _moveCount--;
            _uiManager.GameplayInfo.UpdateMoveCountText(_moveCount);
        }

        public void UpdateScoreText(int pointPerItem) {
            _score += pointPerItem;
            _uiManager.GameplayInfo.UpdateScoreText(_score);
        }
    }
}
