using UnityEngine;
using Settings;
using PlayerInfo;

namespace Game.Gameplay.Board {
	public class Checker {
		public float YPos { get { return _pos; } }
		public float BaseXPos { get { return _basePos; } }

		public Item.Item[] Items = null;
		public float MatchDuration;

		private bool _isDisabled;
		private float _pos;
		private float _basePos;
		private float _itemMatchDuration;
		private Sprite _matchSprite;

		public void Prepare(float pos, float basePos, int itemCount, Sprite matchSprite) {
			_itemMatchDuration = 0.1f;
			MatchDuration = itemCount * _itemMatchDuration;

			_pos = pos;
			_basePos = basePos;
			_matchSprite = matchSprite;

			Items = new Item.Item[itemCount];
		}

		/// for horizontally swapped ones
		public void UpdateData(int index1, int index2) {
			var temp = Items[index1];
			Items[index1] = Items[index2];
			Items[index2] = temp;
		}

		/// for vertically swapped ones
		public bool UpdateData(Item.Item item, ScoreManager sm) {
			Items[Properties.isRowMatch ? item.Column : item.Row] = item;

			if (!IsCompleted()) 
				return false;
			
			Match(sm);
			return true;
		}

		private bool IsCompleted() {
			for (int i = 1; i < Items.Length; i++) {
				if (Items[i].ItemType != Items[0].ItemType) 
					return false;
			}

			return true;
		}

		private void Match(ScoreManager sm) {
			_isDisabled = true;

			sm.Match(Items, Properties.PointsArray[(int)Items[0].ItemType - 1], _itemMatchDuration, _matchSprite);
		}

		public void Disable() {
			if (_isDisabled) return;

			for (int i = 0; i < Items.Length; i++) {
				Items[i].BoxCollider2D.enabled = false;
			}
		}
	}
}

