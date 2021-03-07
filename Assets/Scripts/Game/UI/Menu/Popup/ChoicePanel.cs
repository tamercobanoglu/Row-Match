using UnityEngine;
using System.Collections;
using Game.UI.Buttons;
using Settings;
using DG.Tweening;
using TMPro;

namespace Game.UI.Menu.Popup {
	public class ChoicePanel : MonoBehaviour {
		public UIGameplay UIManager;
		public SpriteRenderer Background;
		public TextMeshPro MessageText;
		public ChoiceButton FirstOption;
		public ChoiceButton SecondOption;

		private ButtonType _buttonType;
		private IButton _button = null;

		/// prepare choice panel for possible other questions
		private void Prepare(IButton button, ButtonType buttonType, string[] texts) {
			_button = button;
			_buttonType = buttonType;

			MessageText.text = texts[0];
			FirstOption.Text.text = texts[1];
			SecondOption.Text.text = texts[2];
		}

		public void Display(IButton button, ButtonType buttonType, string[] texts) {
			Prepare(button, buttonType, texts);

			transform.localScale = Vector3.one * 0.95f;

			UIManager.RearButtonsActivation(false);
			gameObject.SetActive(true);

			Background.DOFade(0.65f, Properties.FadeOutDuration);
			transform.DOScale(Vector3.one, Properties.FadeOutDuration);
		}

		public void FirstOptionSelected() {
			switch (_buttonType) {
				case ButtonType.ReturnButton:
					((ReturnButton)_button).Execute(ReturnButtonType.Instant, true);
					break;
			}
		}

		public void SecondOptionSelected() {
			StartCoroutine(Disappearing());
		}

		IEnumerator Disappearing() {
			UIManager.RearButtonsActivation(true);
			Background.DOFade(0f, 0.04f);
			yield return new WaitForSeconds(0.045f);

			gameObject.SetActive(false);
		}

		private void Disappear() {
			UIManager.RearButtonsActivation(true);

			var temp = Background.color;
			temp.a = 0;
			Background.color = temp;

			gameObject.SetActive(false);
		}
	}

	public enum ChoiceType { 
		Option1,
		Option2
	}
}

