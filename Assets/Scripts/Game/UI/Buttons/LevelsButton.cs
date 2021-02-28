using UnityEngine;

namespace Game.UI.Buttons {
    public class LevelsButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } }

        private ButtonType _buttonType = ButtonType.LevelsButton;
        private bool _isSelected;

        public UIMenu UIManager;

        public void Operate(TouchPhase touchPase) {

            switch (touchPase) {
                case TouchPhase.Began:
                    Selected();
                    break;
                case TouchPhase.Moved:
                    Moved();
                    break;
                case TouchPhase.Stationary:
                    Stationary();
                    break;
                case TouchPhase.Ended:
                    Released();
                    break;
                case TouchPhase.Canceled:
                    Canceled();
                    break;
            }
        }

        public void Selected() {
            _isSelected = true;
        }

        public void Moved() {

        }

        public void Stationary() {

        }

        public void Released() {
            if (!_isSelected) return;

            UIManager.LevelsPopup.Popup();

            _isSelected = false;
        }

        public void Canceled() {
            _isSelected = false;
        }
    }
}
