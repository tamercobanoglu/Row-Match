﻿using UnityEngine;
using Settings;
using Game.Mechanics;
using DG.Tweening;

namespace Game.UI.Buttons {
    public class LevelsButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }

        private ButtonType _buttonType = ButtonType.LevelsButton;
        private bool _isSelected;

        public UIMenu UIManager;
        public SpriteRenderer Image;

        public void Operate(Vector3 pos, TouchPhase touchPhase) {
            switch (touchPhase) {
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
            UIManager.HitButton = this;
            UIManager.State = MenuState.SelectionStarted;

            Image.DOColor(Properties.PressedButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one * 0.95f, Properties.ButtonAnimDuration);
        }

        public void Moved(Vector3 pos) {

        }

        public void Stationary(Vector3 pos) {

        }

        public void Released(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            UIManager.LevelsPopup.Popup();

            UIManager.State = MenuState.None;
            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            UIManager.State = MenuState.None;
            _isSelected = false;
        }
    }
}
