using UnityEngine;
using System.Collections;
using Settings;
using Game.Gameplay.Level;
using DG.Tweening;
using TMPro;

namespace Game.UI.Gameplay {
    public class GameplayInfo : MonoBehaviour {
        [Header("Parents")]
        public Transform HighestScoreTransform;
        public Transform MoveCountTransform;
        public Transform ScoreTransform;

        [Header("Texts")]
        public TextMeshPro HighestScoreText;
        public TextMeshPro MoveCountText;
        public TextMeshPro ScoreText;
        public Transform InstructionText;

        private float _instructionDuration;

        public void Initialize(int[] scores, int currentLevel, LevelInfo[] levels) {
            _instructionDuration = 2f;

            HighestScoreText.text = scores[currentLevel - 1].ToString();
            MoveCountText.text = levels[currentLevel - 1].MoveCount.ToString();
        }

        public void UpdateScoreText(int score) {
            ScoreText.text = $"Score: {score}";
        }

        public void UpdateMoveCountText(int moveCount) {
            MoveCountText.text = moveCount.ToString();
        }

        public IEnumerator FirstAnimation() {
            BringInstructionText();
            yield return new WaitForSeconds(_instructionDuration);
            HideInstructionText();
            yield return new WaitForSeconds(Properties.FadeOutDuration);
        }

        public IEnumerator SecondAnimation() {
            BringGameInfo();
            yield return new WaitForSeconds(Properties.FadeOutDuration * 2);
            InstructionText.gameObject.SetActive(false);
        }

        private void BringInstructionText() {
            InstructionText.gameObject.SetActive(true);

            var seq = DOTween.Sequence();
            seq.Append(InstructionText.DOScale(Vector3.one, Properties.FadeOutDuration))
                .Append(InstructionText.DOMoveY(InstructionText.position.y + 0.4f, 1.75f));
        }

        private void HideInstructionText() {
            InstructionText.DOScale(Vector3.one * 0.01f, Properties.FadeOutDuration);
        }

        private void BringGameInfo() {
            HighestScoreTransform.gameObject.SetActive(true);
            MoveCountTransform.gameObject.SetActive(true);
            ScoreTransform.gameObject.SetActive(true);

            HighestScoreTransform.DOMoveX(HighestScoreTransform.position.x + 7f, Properties.FadeOutDuration * 2);
            MoveCountTransform.DOMoveX(MoveCountTransform.position.x - 7f, Properties.FadeOutDuration * 2);
            ScoreTransform.DOMoveX(ScoreTransform.position.x + 7f, Properties.FadeOutDuration * 2);
        }
    }
}
