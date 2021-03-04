using UnityEngine;
using DG.Tweening;

namespace Game.Gameplay.Item {
	public class Item : MonoBehaviour {
		public SpriteRenderer SpriteRenderer;
		public BoxCollider2D BoxCollider2D;

		public int Row;
		public int Column;

		private ItemType _itemType;

		public ItemType ItemType {
			get { return _itemType; }
		}

		public void Prepare(ItemType itemType, ItemBase itembase, Sprite sprite, int row, int col) {
			SpriteRenderer = itembase.SpriteRenderer;
			SpriteRenderer.sprite = sprite;
			SpriteRenderer.size = Vector2.one * 0.9f;

			BoxCollider2D = itembase.BoxCollider2D;

			Row = row;
			Column = col;
			_itemType = itemType;
		}

		public void Animate(float itemMatchDuration) {
			transform.DOShakeRotation(itemMatchDuration);
		}

		public void Complete(Sprite matchSprite) {
			SpriteRenderer.sprite = matchSprite;
			SpriteRenderer.size = Vector2.one * 0.9f;
			BoxCollider2D.enabled = false;
		}
	}
}

