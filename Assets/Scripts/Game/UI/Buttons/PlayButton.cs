using Settings;
using UnityEngine;
using Utils;

namespace Game.UI.Buttons {
	public class PlayButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } }

        private ButtonType _buttonType = ButtonType.PlayButton;
        private bool _isSelected;

        public SpriteRenderer Image;
        public SpriteRenderer Icon;
        public MeshRenderer Text;
        public BoxCollider2D BoxCollider2D;

        [SerializeField] private int _levelNum;

        private UIManager _uiManager;

        public void Prepare(UIManager uiManager, int levelNum, bool isUnlocked) {
            _levelNum = levelNum;
            _uiManager = uiManager;

            if (!isUnlocked) return;

            Unlock();
        }

        private void Unlock() {
            Image.color = Properties.UnlockedLevelColor;
            Icon.enabled = false;
            Text.enabled = true;
            BoxCollider2D.enabled = true;
        }

        public void AnimateUnlocking() {
            Unlock(); // temp
        }

        public void Operate(Vector3 pos, TouchPhase touchPase) {

            switch (touchPase) {
                case TouchPhase.Began:
                    Selected(pos);
                    break;
                case TouchPhase.Moved:
                    Moved(pos);
                    break;
                case TouchPhase.Stationary:
                    Stationary(pos);
                    break;
                case TouchPhase.Ended:
                    Released(pos);
                    break;
                case TouchPhase.Canceled:
                    Canceled(pos);
                    break;
            }
        }

        public void Selected(Vector3 pos) {
            _isSelected = true;
        }

        public void Moved(Vector3 pos) {

        }

        public void Stationary(Vector3 pos) {

        }

        public void Released(Vector3 pos) {
            if (!_isSelected) return;

            Properties.CurrentLevel = _levelNum;
            _uiManager.SceneLoader.LoadScene(SceneType.LevelScene);

            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            _isSelected = false;
        }
    }
}
