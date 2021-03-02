using Settings;
using UnityEngine;
using Utils;
using DG.Tweening;

namespace Game.UI.Buttons {
	public class PlayButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return _buttonType; } }
        public bool IsSelected { get { return _isSelected; } }

        private ButtonType _buttonType = ButtonType.PlayButton;
        private bool _isSelected;

        public SpriteRenderer Image;
        public SpriteRenderer Icon;
        public MeshRenderer Text;
        public BoxCollider2D BoxCollider2D;

        [HideInInspector] public float AnimDuration;
        [SerializeField] private int _levelNum;

        private UIManager _uiManager;

        public void Prepare(UIManager uiManager, int levelNum, bool isUnlocked) {
            AnimDuration = 3.25f;

            _levelNum = levelNum;
            _uiManager = uiManager;

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

        public void AnimateUnlocking() {
            Text.transform.localScale = Vector3.one * 0.01f;
            Text.enabled = true;

            var seq = DOTween.Sequence();
            seq.Append(Icon.transform.DOScale(Vector3.one * 3f, 2f))
                .Append(Icon.transform.DOScale(Vector3.one * 0.01f, 0.5f))
                .Append(Text.transform.DOScale(Vector3.one, 0.25f))
                .Append(Image.DOColor(Properties.ButtonColor, 0.5f));
        }

        public void Activate() {
            Icon.enabled = false;
            BoxCollider2D.enabled = true;
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
            Image.DOColor(Properties.PressedButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one * 0.9f, Properties.ButtonAnimDuration);
        }

        public void Moved(Vector3 pos) {

        }

        public void Stationary(Vector3 pos) {

        }

        public void Released(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            Properties.CurrentLevel = _levelNum;
            _uiManager.SceneLoader.LoadScene(SceneType.LevelScene);

            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);
            _isSelected = false;
        }
    }
}
