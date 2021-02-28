using UnityEngine;
using Utils;
using DG.Tweening;
using Settings;

namespace Game.UI.Buttons {
    public class ReturnButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } }

        private ButtonType _buttonType = ButtonType.Return;
        private bool _isSelected = true;

        public UIManager UIManager;
        public BoxCollider2D Collider;
        public Transform TextTransform;

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

            UIManager.SceneLoader.LoadScene(SceneType.MainScene);

            _isSelected = false;
        }

        public void Canceled() {
            _isSelected = false;
        }

        public void PopupText() {
            Collider.enabled = true;
            TextTransform.gameObject.SetActive(true);
            TextTransform.DOScale(Vector3.one, Properties.FadeOutDuration);
        }
    }
}
