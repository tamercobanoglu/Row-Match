using UnityEngine;

namespace Game.UI.Buttons {
    public class CloseButton : MonoBehaviour, IButton {
        public ButtonType ButtonType { get { return buttonType; } }
        private ButtonType buttonType = ButtonType.Close;

        public GameObject CloseGO;
    }
}
