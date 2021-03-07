using UnityEngine;

namespace Game.UI.Buttons {
	public interface IButton {
		bool IsSelected { get; set; }

		void Operate(Vector3 pos, TouchPhase touchPase);
		void Selected(Vector3 pos);
		void Moved(Vector3 pos);
		void Stationary(Vector3 pos);
		void Released(Vector3 pos);
		void Canceled(Vector3 pos);
	}

	public enum ButtonType {
		LevelsButton,
		LevelsPanel,
		PlayButton,
		CloseButton,
		ReturnButton,
		ChoiceButton
	}
}
