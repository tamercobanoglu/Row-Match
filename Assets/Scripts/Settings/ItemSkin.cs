using UnityEngine;

namespace Settings {
	[CreateAssetMenu(menuName = "Item Skin", fileName = "New Item Skin")]
	public class ItemSkin : ScriptableObject {
		public Sprite Red;
		public Sprite Green;
		public Sprite Blue;
		public Sprite Yellow;

		public Sprite Tick;
	}
}

