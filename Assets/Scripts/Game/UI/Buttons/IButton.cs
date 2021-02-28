
namespace Game.UI.Buttons {
	public interface IButton {
		ButtonType ButtonType { get; }
		bool IsSelected { get; }

		void Operate(UnityEngine.TouchPhase touchPase);
		void Selected();
		void Moved();
		void Stationary();
		void Released();
		void Canceled();
	}

	public enum ButtonType {
		Levels,
		Play,
		Close,
		Return
	}
}
