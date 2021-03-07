using UnityEngine;
using Game.UI.Buttons;
using Settings;
using DG.Tweening;

namespace Game.UI.Gameplay {
    public class OutcomePanel : MonoBehaviour {
        public ReturnButton ReturnButton;
        public SpriteRenderer Background;
        public Transform TextTransform;

        public void Display() {
            Background.DOFade(0.6f, Properties.FadeOutDuration);

            TextTransform.gameObject.SetActive(true);
            TextTransform.DOLocalMoveY(0.5f, 1.5f);
            TextTransform.DOScale(Vector3.one, Properties.FadeOutDuration);
		}
    }
}

