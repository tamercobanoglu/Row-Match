using UnityEngine;

namespace Utils {
	public class ScreenManager : MonoBehaviour {
		public Camera Cam;

		public void Initialize() {
			PrepareCamera();
		}

		private void PrepareCamera() {
			var size = (Screen.height / Screen.width < 1.8f) ? 8.8f : 10.8f;
			Cam.orthographicSize = size;
		}
	}
}
