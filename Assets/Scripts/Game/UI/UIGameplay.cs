using Game.Gameplay.Level;
using Game.UI.Gameplay;
using Settings;
using PlayerInfo;
using System.Collections;
using UnityEngine;
using Game.UI.Buttons;

namespace Game.UI {
    public class UIGameplay : UIManager {
        public ScoreManager ScoreManager;
        public GameplayInfo GameplayInfo;
        public MessagePanel MessagePanel;
        public ReturnButton ReturnButton;
        public LevelManager LevelManager;
        [SerializeField] private int _currentLevel;

        protected override void Awake() {
            Initialize();

            _currentLevel = Properties.CurrentLevel;

            Player.Initialize(LevelInfoPack.Levels.Length);
            GameplayInfo.Initialize(Player.Scores, _currentLevel, LevelInfoPack.Levels);
            LevelManager.Initialize(_currentLevel, LevelInfoPack.Levels);
            ScoreManager.Initialize(this, LevelInfoPack.Levels[_currentLevel - 1].MoveCount);
        }

        protected override void Initialize() {
            base.Initialize();

            Fade(FadeType.In);
        }

        public void EndGame(int score) {
            UpdatePlayer(score);

            StartCoroutine(EndingProcess());
        }

        IEnumerator EndingProcess() {
            LevelManager.GameBoard.DisableRows();
            yield return new WaitForSeconds(0.3f);

            LevelManager.GameBoard.EndGame();
            MessagePanel.Display();
            yield return new WaitForSeconds(1.6f);

            ReturnButton.PopupText();
        }

        private void UpdatePlayer(int score) {
			if (score > Player.Scores[Properties.CurrentLevel - 1]) {
                Player.Scores[Properties.CurrentLevel - 1] = score;
                Player.UnlockedLevels[Properties.CurrentLevel] = true;

                Properties.HighestScoreAchieved = true;

                Player.SavePlayer();
                return;
			}

            Properties.HighestScoreAchieved = false;
        }
    }
}
