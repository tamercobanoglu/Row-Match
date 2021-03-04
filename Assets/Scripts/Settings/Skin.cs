using UnityEngine;

namespace Settings {
	[CreateAssetMenu(menuName = "Skin", fileName = "New Skin")]
	public class Skin : ScriptableObject {
		public Sprite Red;
		public Sprite Green;
		public Sprite Blue;
		public Sprite Yellow;

		public Sprite Tick;

		public Sprite Locked;
		public Sprite Unlocked;
	}
}

