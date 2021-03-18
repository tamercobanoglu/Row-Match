using UnityEngine;
using Game.Gameplay.Board;
using DG.Tweening;

namespace Game.Gameplay.Item {
	public class Item : MonoBehaviour {
		private const string Glow = "_Glow";

		public SpriteRenderer SpriteRenderer;
		public BoxCollider2D BoxCollider2D;

		public int Row;
		public int Column;

		private ItemType _itemType;
		private float _matchDuration;

		public ItemType ItemType { get { return _itemType; } }
		public float MatchDuration { get { return _matchDuration; } }

		public void Prepare(ItemType itemType, ItemBase itembase, Sprite sprite, int row, int col) {
			SpriteRenderer = itembase.SpriteRenderer;
			SpriteRenderer.sprite = sprite;
			SpriteRenderer.size = Vector2.one * 0.9f;

			BoxCollider2D = itembase.BoxCollider2D;

			Row = row;
			Column = col;
			_itemType = itemType;
			_matchDuration = 0.1f;
		}

		public void Animate(float itemMatchDuration) {
			transform.DOShakeRotation(itemMatchDuration);
		}

		public void Complete(Sprite matchSprite) {
			SpriteRenderer.sprite = matchSprite;
			SpriteRenderer.size = Vector2.one * 0.9f;
			BoxCollider2D.enabled = false;
		}

		public void Light(float intensity) {
			SpriteRenderer.material.SetFloat(Glow, intensity);
		}

		public void Jiggle(Direction dir) {
			StopAllCoroutines();
			Vector3 refPos = transform.position;
			var seq = DOTween.Sequence();

			if (dir == Direction.Up || dir == Direction.Down) {
				float diff = dir == Direction.Up ? 0.15f : -0.15f;

				seq.Append(transform.DOMoveY(refPos.y + diff, _matchDuration))
				.Append(transform.DOMoveY(refPos.y - diff, _matchDuration))
				.Append(transform.DOMoveY(refPos.y, _matchDuration / 2f));
			}

			else if (dir == Direction.Right || dir == Direction.Left) {
				float diff = dir == Direction.Right ? 0.15f : -0.15f;

				seq.Append(transform.DOMoveX(refPos.x + diff, _matchDuration))
				.Append(transform.DOMoveX(refPos.x - diff, _matchDuration))
				.Append(transform.DOMoveX(refPos.x, _matchDuration / 2f));
			}
		}
	}
}

