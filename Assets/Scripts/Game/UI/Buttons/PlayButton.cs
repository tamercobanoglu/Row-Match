using UnityEngine;
using Settings;

namespace Game.UI.Buttons {
	public class PlayButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return buttonType; } }
        private ButtonType buttonType = ButtonType.Play;

        public SpriteRenderer Image;
        public SpriteRenderer Icon;
        public MeshRenderer Text;
        public BoxCollider2D BoxCollider2D;

        public void Prepare(bool unlockedLevel) {
			if (unlockedLevel) {
                Unlock();
            }
        }

        private void Unlock() {
            Image.color = Properties.UnlockedLevelColor;
            Icon.enabled = false;
            Text.enabled = true;
            BoxCollider2D.enabled = true;
        }

        public void AnimateUnlocking() {
            Unlock(); // temp
        }
    }
}
