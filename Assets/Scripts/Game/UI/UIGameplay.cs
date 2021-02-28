using Game.Gameplay.Level;
using Game.UI.Gameplay;
using Settings;
using Player;
using Utils;
using System.Collections;
using UnityEngine;
using Game.UI.Buttons;

namespace Game.UI {
    public class UIGameplay : UIManager {
        public GameplayInfo GameplayInfo;
        public MessagePanel MessagePanel;
        public LevelManager LevelManager;
        public ScoreManager ScoreManager;
        public ReturnButton ReturnButton;

        [Header("Level Related")]
        [SerializeField] private int _currentLevel;
        public LevelInfoPack LevelInfoPack;

        protected override void Awake() {
            Initialize();

            _currentLevel = Properties.CurrentLevel;

            GameplayInfo.Initialize(_currentLevel, LevelInfoPack.Levels);
            LevelManager.Initialize(_currentLevel, LevelInfoPack.Levels);
            ScoreManager.Initialize(LevelInfoPack.Levels[_currentLevel - 1].MoveCount);
        }

        protected override void Initialize() {
            base.Initialize();

            Fade(FadeType.In);
        }

        public void EndGame() {
            StartCoroutine(EndingProcess());
        }

        IEnumerator EndingProcess() {
            yield return new WaitForSeconds(0.3f);

            LevelManager.GameBoard.EndGame();
            MessagePanel.Display();

            yield return new WaitForSeconds(1.6f);

            ReturnButton.PopupText();
        }
    }
}
