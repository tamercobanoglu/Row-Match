using UnityEngine;
using System.Collections;
using Settings;
using Game.Mechanics;
using PlayerInfo;
using TMPro;
using DG.Tweening;

namespace Game.UI.Buttons {
    public class MatchTypeButton : MonoBehaviour, IButton {
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }
        private bool _isSelected;

        private const string RowMatchText = "Row Match";
        private const string ColumnMatchText = "Column Match";

        public ButtonType ButtonType;
        public SpriteRenderer Icon;
        public SpriteRenderer Image;
        public TextMeshPro Text;

        private UIMenu _uiManager;
        private bool _isRowMatch;

        public void Initialize(UIMenu uiManager, bool isRowMatch) {
            _uiManager = uiManager;
            _isRowMatch = isRowMatch;

            if (_isRowMatch) {
                Icon.sprite = _uiManager.Skin.RowMatchIcon;
                Text.text = RowMatchText;
            }
            else {
                Icon.sprite = _uiManager.Skin.ColumnMatchIcon;
                Text.text = ColumnMatchText;
            }
        }

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
            _uiManager.HitButton = this;
            _uiManager.State = GameState.SelectionStarted;

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

            Execute();

            _uiManager.State = GameState.None;
            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.OrangeButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            _uiManager.State = GameState.None;
            _isSelected = false;
        }

        private void Execute() {
            _isRowMatch = !_isRowMatch;

            Player.Instance.UpdatePlayer(_isRowMatch);

            StopAllCoroutines();
            StartCoroutine(ChangeProperties(_isRowMatch));
        }

        IEnumerator ChangeProperties(bool isRowMatch) {
            Icon.DOFade(0f, Properties.ButtonAnimDuration / 2);
            Text.text = isRowMatch ? RowMatchText : ColumnMatchText;

            yield return new WaitForSeconds(0.1f);

            Icon.sprite = isRowMatch 
                ? _uiManager.Skin.RowMatchIcon 
                : _uiManager.Skin.ColumnMatchIcon;

            Icon.DOFade(1f, Properties.ButtonAnimDuration / 2);
        }
    }
}
