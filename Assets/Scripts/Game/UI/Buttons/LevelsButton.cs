using UnityEngine;

namespace Game.UI.Buttons {
    public class LevelsButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return buttonType; } }
        private ButtonType buttonType = ButtonType.Levels;
    }
}
