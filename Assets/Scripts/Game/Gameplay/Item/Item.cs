using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Item {
	public class Item : MonoBehaviour {
		public SpriteRenderer SpriteRenderer;

		private ItemType _itemType;

		public ItemType ItemType {
			get { return _itemType; }
		}

		public void Prepare(ItemType itemType, ItemBase itembase, Sprite sprite) {
			SpriteRenderer = itembase.SpriteRenderer;
			SpriteRenderer.sprite = sprite;
			SpriteRenderer.size = Vector2.one * 0.8f;

			_itemType = itemType;
		}

		public void ChangeSprite(Sprite sprite) {
			SpriteRenderer.sprite = sprite;
		}
	}
}

