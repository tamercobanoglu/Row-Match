using UnityEngine;
using Utils;
using Game.Mechanics;
using Settings;
using DG.Tweening;

namespace Game.UI.Buttons {
    public class ReturnButton : MonoBehaviour, IButton {
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }
        private bool _isSelected = true;

        public ButtonType ButtonType;
        public UIGameplay UIManager;
        public BoxCollider2D Collider;
        public ReturnButtonType ReturnButtonType;

        [Header("Exit Button")]
        public SpriteRenderer Icon;
        [Header("Last Tapping Image")]
        public Transform TextTransform;

        private static string[] ChoicePanelTexts = new string[] {
            "You'll lose your progress!",
            "Quit",
            "Cancel"
        };

        public void Initialize() {
            /// arrange pos
            float yPos = Camera.main.orthographicSize * -1f;
            float xPos = yPos * (Camera.main.aspect) * -1f;

            /// will bring it from 3 units away
            transform.localPosition = new Vector3(
                    (xPos - 1f) + 3f,
                    yPos + 1f,
                    transform.localPosition.z
                );
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
            UIManager.HitButton = this;
            UIManager.State = GameState.SelectionStarted;

            if (ReturnButtonType == ReturnButtonType.Controlled) {
                transform.DOScale(Vector3.one * 0.9f, Properties.ButtonAnimDuration);
            }
        }

        public void Moved(Vector3 pos) {

        }

        public void Stationary(Vector3 pos) {

        }

        public void Released(Vector3 pos) {
            if (!_isSelected) return;

            /// return icon
			if (ReturnButtonType == ReturnButtonType.Controlled) {
                transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);
            }

            Execute(ReturnButtonType, false);

            UIManager.HitButton = null;
            UIManager.State = GameState.None;
            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            if (!_isSelected) return;

            if (ReturnButtonType == ReturnButtonType.Controlled) {
                transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);
            }

            UIManager.HitButton = null;
            UIManager.State = GameState.None;
            _isSelected = false;
        }

        public void Execute(ReturnButtonType rbt, bool byProxy) {
            if (rbt == ReturnButtonType.Controlled) {
                UIManager.ChoicePanel.Display(this, ButtonType, ChoicePanelTexts);
            }

            else {
                if (byProxy) UIManager.UpdatePlayer(0);
                UIManager.SceneLoader.LoadScene(SceneType.MainScene);
            }
        }

        public void PopupText() {
            Collider.enabled = true;
            TextTransform.gameObject.SetActive(true);
            TextTransform.DOScale(Vector3.one, Properties.FadeOutDuration);
        }

        public void BringButton() {
            gameObject.SetActive(true);
            transform.DOMoveX(transform.localPosition.x - 3f, Properties.FadeOutDuration * 2);
        }
    }

    public enum ReturnButtonType { 
        Instant,
        Controlled
    }
}
