using Game.UI.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Popup {
    public class LevelsPanel : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } }

        private ButtonType _buttonType = ButtonType.LevelsPanel;
        private bool _isSelected = true;

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

            /// animate

            _isSelected = false;
        }

        public void Canceled() {
            _isSelected = false;
        }
    }
}

