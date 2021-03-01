using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Settings;

namespace Game.UI.Menu {
	public class CelebrationPanel : MonoBehaviour {
		public ParticleSystem Particles;
		public SpriteRenderer Background;
		public TextMeshPro CongratsText;
		public SpriteRenderer Icon;
		public TextMeshPro LabelText;
		public TextMeshPro HighScoreText;

		[HideInInspector] public float AnimDuration;
		[HideInInspector] public float DisappearingDuration;

		public void Prepare(int highScore) {
			AnimDuration = 3f;
			DisappearingDuration = 0.5f;

			HighScoreText.text = highScore.ToString();
		}

		public void Animate() {
			gameObject.SetActive(true);

			/// Particles.Play();

			Background.DOFade(1f, 1f);
			CongratsText.DOFade(1f, 0.5f);

			var seq = DOTween.Sequence();
			seq.Append(Icon.transform.DOMoveX(0f, 0.5f))
				.Append(LabelText.transform.DOScale(Vector3.one, Properties.FadeOutDuration))
				.Append(HighScoreText.transform.DOScale(Vector3.one, Properties.FadeOutDuration));
		}

		public void StopAnimating() {
			/// Particles.Stop();

			Background.DOFade(0f, 0.5f);
			CongratsText.DOFade(0f, 0.5f);
			Icon.transform.DOMoveX(-15f, 0.5f);
			LabelText.DOFade(0f, 0.5f);
			HighScoreText.DOFade(0f, 0.5f);
		}

		public void Deactivate() {
			gameObject.SetActive(false);
		}
	}
}

