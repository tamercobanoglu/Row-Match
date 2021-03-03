using UnityEngine;
using Utils;
using Settings;
using DG.Tweening;

namespace Game.UI.Buttons {
    public class ReturnButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }

        private ButtonType _buttonType = ButtonType.ReturnButton;
        private bool _isSelected = true;

        public UIManager UIManager;
        public BoxCollider2D Collider;
        public Transform TextTransform;

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

            UIManager.SceneLoader.LoadScene(SceneType.MainScene);

            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            _isSelected = false;
        }

        public void PopupText() {
            Collider.enabled = true;
            TextTransform.gameObject.SetActive(true);
            TextTransform.DOScale(Vector3.one, Properties.FadeOutDuration);
        }
    }
}
