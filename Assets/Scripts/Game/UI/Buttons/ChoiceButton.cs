using UnityEngine;
using Game.Mechanics;
using Game.UI.Menu.Popup;
using Settings;
using DG.Tweening;
using TMPro;

namespace Game.UI.Buttons {
    public class ChoiceButton : MonoBehaviour, IButton {
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }
        private bool _isSelected = true;

        public ButtonType ButtonType;
        public ChoiceType ChoiceType;
        public UIGameplay UIManager;
        public SpriteRenderer Image;
        public TextMeshPro Text;

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

            Image.DOColor(Properties.PressedOrangeButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one * 0.95f, Properties.ButtonAnimDuration);
        }

        public void Moved(Vector3 pos) {

        }

        public void Stationary(Vector3 pos) {

        }

        public void Released(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.OrangeButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            Execute(ChoiceType);

            UIManager.HitButton = null;
            UIManager.State = GameState.None;
            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.OrangeButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            UIManager.HitButton = null;
            UIManager.State = GameState.None;
            _isSelected = false;
        }

        public void Execute(ChoiceType ct) {
            if (ct == ChoiceType.Option1) {
                UIManager.ChoicePanel.FirstOptionSelected();
            }

            else {
                UIManager.ChoicePanel.SecondOptionSelected();
            }
        }
    }
}
