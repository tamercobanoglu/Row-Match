using UnityEngine;
using System.Collections;
using Game.Mechanics;
using Game.UI.Buttons;
using Game.UI.Gameplay;
using Game.UI.Menu.Popup;
using Game.Gameplay.Level;
using Settings;
using PlayerInfo;

namespace Game.UI {
    public class UIGameplay : UIManager {
        public ScoreManager ScoreManager;
        public ChoicePanel ChoicePanel;
        public ReturnButton ReturnButton;
        public GameplayInfo GameplayInfo;
        public OutcomePanel OutcomePanel;
        public LevelManager LevelManager;

        [HideInInspector] public GameState State;
        [HideInInspector] public IButton HitButton = null;
        [HideInInspector] public bool GameStopped = false;
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
            LevelManager.Initialize(this, _currentLevel, LevelInfoPack.Levels);
            ScoreManager.Initialize(this, LevelInfoPack.Levels[_currentLevel - 1].MoveCount);
            ReturnButton.Initialize();

            Fade(FadeType.In);
        }

        public void EndGame(int score) {
            UpdatePlayer(score);

            StartCoroutine(EndingProcess());
        }

        IEnumerator EndingProcess() {
            LevelManager.GameBoard.DisableCells();
            yield return new WaitForSeconds(0.3f);

            OutcomePanel.Display();
            yield return new WaitForSeconds(1.6f);

            OutcomePanel.ReturnButton.PopupText();
        }

        public void UpdatePlayer(int score) {
            Player.Instance.UpdatePlayer(score);
        }

        private void Load() {
            StartCoroutine(LoadingProcess());
        }

        IEnumerator LoadingProcess() {
            Fade(0.65f, Properties.FadeOutDuration);
            yield return StartCoroutine(GameplayInfo.FirstAnimation());

            ReturnButton.BringButton();

            Fade(0f, Properties.FadeOutDuration * 2);
            yield return StartCoroutine(GameplayInfo.SecondAnimation());

            State = GameState.None;
        }

        /// controls the activation of buttons in the background
        /// when the choice panel is activated / deactivated
        public void RearButtonsActivation(bool isActive) {
            GameStopped = !isActive;

            ReturnButton.Collider.enabled = isActive;
        }
    }
}
