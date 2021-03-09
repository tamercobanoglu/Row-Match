using UnityEngine;

namespace Settings {
	[CreateAssetMenu(menuName = "Skin", fileName = "New Skin")]
	public class Skin : ScriptableObject {
		[Header("Item Skins")]
		public Sprite Red;
		public Sprite Green;
		public Sprite Blue;
		public Sprite Yellow;

		[Header("Gameplay Sprites")]
		public Sprite Tick;

		[Header("Menu Sprites")]
		public Sprite Locked;
		public Sprite Unlocked;
		public Sprite RowMatchIcon;
		public Sprite ColumnMatchIcon;
	}
}

