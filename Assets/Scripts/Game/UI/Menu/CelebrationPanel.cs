using UnityEngine;
using System.Collections;
using Settings;
using DG.Tweening;
using TMPro;

namespace Game.UI.Menu {
	public class CelebrationPanel : MonoBehaviour {
		public ParticleSystem Particles;
		public SpriteRenderer Background;
		public TextMeshPro CongratsText;
		public SpriteRenderer Icon;
		public TextMeshPro LabelText;
		public TextMeshPro HighScoreText;

		private ParticleSystem particles;
		private float _fadeDuration;
		private float _particleDuration;

		private void Prepare(int highScore) {
			_particleDuration = 3.5f;
			_fadeDuration = 0.5f;

			HighScoreText.text = highScore.ToString();
		}

		public IEnumerator Animate(int highScore) {
			Prepare(highScore);

			BringElements();
			yield return new WaitForSeconds(_fadeDuration);
			StartParticles();
			yield return new WaitForSeconds(_particleDuration);
			StopAnimating();
			yield return new WaitForSeconds(_fadeDuration);
			Deactivate();
		}

		private void BringElements() {
			gameObject.SetActive(true);

			Background.DOFade(0.65f, _fadeDuration);
			CongratsText.DOFade(1f, _fadeDuration);
			Icon.transform.DOMoveX(0f, _fadeDuration);
		}

		private void StartParticles() {
			particles = Instantiate(Particles, Icon.transform.position, Quaternion.identity, Icon.transform);

			particles.Play();
			LabelText.gameObject.SetActive(true);
			HighScoreText.gameObject.SetActive(true);

			var seq = DOTween.Sequence();
			seq.Append(LabelText.transform.DOScale(Vector3.one, Properties.FadeOutDuration))
				.Append(HighScoreText.transform.DOScale(Vector3.one, Properties.FadeOutDuration));
		}

		private void StopAnimating() {
			Background.DOFade(0f, _fadeDuration);
			CongratsText.DOFade(0f, _fadeDuration);
			Icon.transform.DOMoveX(-15f, _fadeDuration);
			LabelText.DOFade(0f, _fadeDuration);
			HighScoreText.DOFade(0f, _fadeDuration);
		}

		private void Deactivate() {
			Destroy(particles.gameObject, 1f);
			gameObject.SetActive(false);
		}
	}
}

