using UnityEngine;
using System.Collections;
using Utils;
using Settings;
using Game.Mechanics;
using PlayerInfo;
using DG.Tweening;

namespace Game.UI.Buttons {
	public class PlayButton : MonoBehaviour, IButton {
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }
        private bool _isSelected;

        public ButtonType ButtonType;
        public SpriteRenderer Image;
        public SpriteRenderer Icon;
        public MeshRenderer Text;
        public BoxCollider2D BoxCollider2D;

        [SerializeField] private int _levelNum;
        private UIMenu _uiManager;
        private Skin _skin;

        /// temp solution before masking out colliders
        private float _topPosY, _bottomPosY;

        public void Prepare(UIManager uiManager, Skin skin, int levelNum, bool isUnlocked) {
            _levelNum = levelNum;
            _uiManager = (UIMenu)uiManager;
            _skin = skin;

            _topPosY = 4.5f;
            _bottomPosY = -6.75f;

            if (!isUnlocked) return;

            Unlock();
        }

        private void Unlock() {
            Image.color = Properties.ButtonColor;
            Text.enabled = true;
            BoxCollider2D.enabled = true;

            Icon.enabled = false;
            Icon.sprite = _skin.Unlocked;
            Icon.size = Vector2.one * 0.8f;
        }

        public void LockTemp() {
            Image.color = Properties.LockedButtonColor;
            Text.enabled = false;
            BoxCollider2D.enabled = false;

            Icon.enabled = true;
            Icon.sprite = _skin.Locked;
            Icon.size = Vector2.one * 0.8f;
        }

        public IEnumerator Animate() {
            FirstAnimation();
            yield return new WaitForSeconds(1.6f);

            Icon.sprite = _skin.Unlocked;
            Icon.size = Vector2.one * 0.8f;
            yield return new WaitForSeconds(0.5f);

            SecondAnimation();
            yield return new WaitForSeconds(1.25f);
            Activate();
        }

        private void FirstAnimation() {
            Text.transform.localScale = Vector3.one * 0.01f;
            Text.enabled = true;

            var seq = DOTween.Sequence();
            seq.Append(Icon.transform.DOScale(Vector3.one * 3f, 1.5f))
                .Append(Icon.transform.DOShakeRotation(0.1f));
        }

        private void SecondAnimation() {
            var seq = DOTween.Sequence();
            seq.Append(Icon.transform.DOScale(Vector3.one * 0.01f, 0.5f))
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
            if (!InsidePanel()) return;

            _isSelected = true;
            _uiManager.HitButton = this;
            _uiManager.State = GameState.SelectionStarted;

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

            _uiManager.State = GameState.None;
            _isSelected = false;
        }

        public void Canceled(Vector3 pos) {
            if (!_isSelected) return;

            Image.DOColor(Properties.ButtonColor, Properties.ButtonAnimDuration);
            transform.DOScale(Vector3.one, Properties.ButtonAnimDuration);

            _uiManager.State = GameState.None;
            _isSelected = false;
        }

        private bool InsidePanel() {
            return transform.position.y > _bottomPosY && transform.position.y < _topPosY;
        }
    }
}
