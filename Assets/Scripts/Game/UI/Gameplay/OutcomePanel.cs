using UnityEngine;
using Game.UI.Buttons;
using Settings;
using TMPro;
using DG.Tweening;

namespace Game.UI.Gameplay {
    public class OutcomePanel : MonoBehaviour {
        private const string OutOfMoves = "Ran out of moves!";
        private const string NonMatchableItems = "No more matching can be made!";

        public ReturnButton ReturnButton;
        public SpriteRenderer Background;
        public TextMeshPro Text;

        public void Display(GameOutcome gameOutcome) {
            Background.DOFade(0.6f, Properties.FadeOutDuration);

            Text.text = gameOutcome == GameOutcome.OutOfMoves
                ? OutOfMoves : NonMatchableItems;

            Text.gameObject.SetActive(true);
            Text.transform.DOLocalMoveY(0.5f, 1.5f);
            Text.transform.DOScale(Vector3.one, Properties.FadeOutDuration);
		}
    }

    public enum GameOutcome { 
        OutOfMoves,
        NonMatchableItems
    }
}

