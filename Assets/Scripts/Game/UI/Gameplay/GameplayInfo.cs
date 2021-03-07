using UnityEngine;
using System.Collections;
using Settings;
using Game.Gameplay.Level;
using DG.Tweening;
using TMPro;

namespace Game.UI.Gameplay {
    public class GameplayInfo : MonoBehaviour {
        private const string RowsInstruction = "Match the rows!";
        private const string ColumnsInstruction = "Match the columns!";

        [Header("Parents")]
        public Transform HighestScoreTransform;
        public Transform MoveCountTransform;
        public Transform ScoreTransform;

        [Header("Texts")]
        public TextMeshPro HighestScoreText;
        public TextMeshPro MoveCountText;
        public TextMeshPro ScoreText;
        public TextMeshPro InstructionText;

        private float _instructionDuration;
        private string _instructionText;

        public void Initialize(int[] scores, int currentLevel, LevelInfo[] levels) {
            _instructionDuration = 2f;
            _instructionText = Properties.isRowMatch ? RowsInstruction : ColumnsInstruction;

            HighestScoreText.text = scores[currentLevel - 1].ToString();
            MoveCountText.text = levels[currentLevel - 1].MoveCount.ToString();

            InstructionText.text = _instructionText;
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
            InstructionText.gameObject.SetActive(false);
        }

        public IEnumerator SecondAnimation() {
            BringGameInfo();
            yield return new WaitForSeconds(Properties.FadeOutDuration * 2);
        }

        private void BringInstructionText() {
            InstructionText.gameObject.SetActive(true);

            var seq = DOTween.Sequence();
            seq.Append(InstructionText.transform.DOScale(Vector3.one, Properties.FadeOutDuration))
                .Append(InstructionText.transform.DOMoveY(InstructionText.transform.position.y + 0.4f, 1.75f));
        }

        private void HideInstructionText() {
            InstructionText.transform.DOScale(Vector3.one * 0.01f, Properties.FadeOutDuration);
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
