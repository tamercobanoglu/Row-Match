using Game.UI.Popup;
using UnityEngine;

namespace Game.UI.Buttons {
    public class CloseButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } }

        private ButtonType _buttonType = ButtonType.CloseButton;
        private bool _isSelected;

        public LevelsPopup LevelsPopup;

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

            LevelsPopup.Disappear();

            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            _isSelected = false;
        }
    }
}
