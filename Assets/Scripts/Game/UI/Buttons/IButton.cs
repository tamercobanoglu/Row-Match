using UnityEngine.UI;

namespace Game.UI.Buttons {
	public interface IButton {
		ButtonType ButtonType { get; }
	}

	public enum ButtonType {
		Levels,
		Play,
		Close
	}
}
