using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.Gameplay.Item;

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
			if (_moveCount < 1) {
                _uiManager.EndGame(_score);
			}
        }

        public void UpdateMoveCount() {
            _moveCount--;
            _uiManager.GameplayInfo.UpdateMoveCountText(_moveCount);
        }

        public void RowMatch(Item[] items, int pointPerItem, float itemMatchDuration, Sprite matchSprite) {
            StartCoroutine(MatchingProcess(items, pointPerItem, items.Length, itemMatchDuration, matchSprite));
        }

        IEnumerator MatchingProcess(Item[] items, int pointPerItem, int itemCount, float itemMatchDuration, Sprite matchSprite) {
            System.Random rnd = new System.Random();
            var direction = rnd.Next(2);

            for (int i = 0; i < itemCount; i++) {
                var j = direction == 0 ? i : itemCount - i - 1;

                items[j].Animate(itemMatchDuration);
                yield return new WaitForSeconds(itemMatchDuration);

                items[j].Complete(matchSprite);
                UpdateScoreText(pointPerItem);
            }
		}

        private void UpdateScoreText(int pointPerItem) {
            _score += pointPerItem;
            _uiManager.GameplayInfo.UpdateScoreText(_score);
        }
    }
}

