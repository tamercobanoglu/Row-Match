using Game.Gameplay.Level;
using Game.UI.Gameplay;
using Settings;
using PlayerInfo;
using System.Collections;
using UnityEngine;
using Game.UI.Buttons;
using Game.Mechanics;

namespace Game.UI {
    public class UIGameplay : UIManager {
        public ScoreManager ScoreManager;
        public GameplayInfo GameplayInfo;
        public MessagePanel MessagePanel;
        public ReturnButton ReturnButton;
        public LevelManager LevelManager;

        [HideInInspector] public GameState State;
        [SerializeField] private int _currentLevel;

        protected override void Awake() {
            State = GameState.Animating;
            Initialize();
            Load();
        }

        protected override void Initialize() {
            base.Initialize();

            _currentLevel = Player.Instance.CurrentLevel;

            GameplayInfo.Initialize(Player.Instance.Scores, _currentLevel, LevelInfoPack.Levels);
            LevelManager.Initialize(_currentLevel, LevelInfoPack.Levels);
            ScoreManager.Initialize(this, LevelInfoPack.Levels[_currentLevel - 1].MoveCount);

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
			if (score > Player.Instance.Scores[Player.Instance.CurrentLevel - 1]) {
                if (Player.Instance.UnlockedLevels[Player.Instance.CurrentLevel]) {
                    Player.Instance.OldLevelRecord = true;
                }

                else {
                    Player.Instance.OldLevelRecord = false;
                    Player.Instance.UnlockedLevels[Player.Instance.CurrentLevel] = true;
                }

                Player.Instance.Scores[Player.Instance.CurrentLevel - 1] = score;
                Player.Instance.HighestScoreAchieved = true;
                Player.Instance.SavePlayer();
                return;
			}

            Player.Instance.HighestScoreAchieved = false;
        }

        private void Load() {
            StartCoroutine(LoadingProcess());
        }

        IEnumerator LoadingProcess() {
            Fade(0.65f, Properties.FadeOutDuration);
            GameplayInfo.BringInstructionText();
            yield return new WaitForSeconds(GameplayInfo.InstructionDuration);

            GameplayInfo.HideInstructionText();
            yield return new WaitForSeconds(Properties.FadeOutDuration);

            Fade(0f, Properties.FadeOutDuration * 2);
            GameplayInfo.BringGameInfo();
            yield return new WaitForSeconds(Properties.FadeOutDuration * 2);

            GameplayInfo.InstructionText.gameObject.SetActive(false);
            State = GameState.None;
        }
    }
}
