using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Settings;

namespace Game.UI.Gameplay {
    public class MessagePanel : MonoBehaviour {
        public SpriteRenderer Background;
        public Transform TextTransform;

        public void Display() {
            gameObject.SetActive(true);

            Background.DOFade(0.6f, Properties.FadeOutDuration);
            TextTransform.DOLocalMoveY(0.5f, 1.5f);
            TextTransform.DOScale(Vector3.one, Properties.FadeOutDuration);
		}
    }
}

