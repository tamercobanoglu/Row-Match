using UnityEngine;
using System.Collections;
using Utils;
using Settings;
using Game.Mechanics;
using PlayerInfo;
using DG.Tweening;

namespace Game.UI.Buttons {
	public class PlayButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }

        private ButtonType _buttonType = ButtonType.PlayButton;
        private bool _isSelected;

        public SpriteRenderer Image;
        public SpriteRenderer Icon;
        public MeshRenderer Text;
        public BoxCollider2D BoxCollider2D;

        [SerializeField] private int _levelNum;
        private UIMenu _uiManager;
        private float _animDuration;

        public void Prepare(UIManager uiManager, int levelNum, bool isUnlocked) {
            _animDuration = 3.25f;

            _levelNum = levelNum;
            _uiManager = (UIMenu)uiManager;

            if (!isUnlocked) return;

            Unlock();
        }

        private void Unlock() {
            Image.color = Properties.ButtonColor;
            Icon.enabled = false;
            Text.enabled = true;
            BoxCollider2D.enabled = true;
        }

        public void LockTemp() {
            Image.color = Properties.LockedButtonColor;
            Icon.enabled = true;
            Text.enabled = false;
            BoxCollider2D.enabled = false;
        }

        public IEnumerator Animate() {
            AnimateUnlocking();
            yield return new WaitForSeconds(_animDuration);
            Activate();
        }

        private void AnimateUnlocking() {
            Text.transform.localScale = Vector3.one * 0.01f;
            Text.enabled = true;

            var seq = DOTween.Sequence();
            seq.Append(Icon.transform.DOScale(Vector3.one * 3f, 2f))
                .Append(Icon.transform.DOScale(Vector3.one * 0.01f, 0.5f))
                .Append(Text.transform.DOScale(Vector3.one, 0.25f))
                .Append(Image.DOColor(Properties.ButtonColor, 0.5f));
        }

        private void Activate() {
            Icon.enabled = false;
            BoxCollider2D.enabled = true;
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
            _uiManager.State = MenuState.SelectionStarted;

            Image.DOColor(Properties.PressedButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one * 0.95f, Properties.ButtonAnimDuration);
        }

        public void Moved(Vector3 pos) {
            //if (!_isSelected) return;

            //Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            //transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            ///// hand over the task to panel
            //var panel = _uiManager.LevelsPopup.LevelsPanel;
            //panel.IsSelected = true;
            //_uiManager.HitButton = panel;
            //panel.SetParameters(pos);

            //_isSelected = false;
		}

        public void Stationary(Vector3 pos) {

        }

        public void Released(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            Player.Instance.CurrentLevel = _levelNum;
            _uiManager.SceneLoader.LoadScene(SceneType.LevelScene);

            _uiManager.State = MenuState.None;
            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            _uiManager.State = MenuState.None;
            _isSelected = false;
        }
    }
}
