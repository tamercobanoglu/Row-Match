using UnityEngine;
using Settings;
using PlayerInfo;

namespace Game.Gameplay.Board {
	public class Row {
		public float YPos { get { return _yPos; } }
		public float BaseXPos { get { return _baseXPos; } }

		public Item.Item[] Items = null;
		public float RowMatchDuration;

		private bool _isDisabled;
		private float _yPos;
		private float _baseXPos;
		private Sprite _matchSprite;
		private float _itemMatchDuration;

		public void Prepare(float yPos, float baseXPos, int gridWidth, Sprite matchSprite) {
			_itemMatchDuration = 0.1f;
			RowMatchDuration = gridWidth * _itemMatchDuration;

			_yPos = yPos;
			_baseXPos = baseXPos;
			_matchSprite = matchSprite;

			Items = new Item.Item[gridWidth];
		}

		/// for horizontally swapped ones
		public void UpdateRowData(int col1, int col2) {
			var temp = Items[col1];
			Items[col1] = Items[col2];
			Items[col2] = temp;
		}

		/// for vertically swapped ones
		public bool UpdateRowData(Item.Item item, ScoreManager sm) {
			Items[item.Column] = item;

			if (!IsCompleted()) 
				return false;
			
			RowMatch(sm);
			return true;
		}

		private bool IsCompleted() {
			for (int i = 1; i < Items.Length; i++) {
				if (Items[i].ItemType != Items[0].ItemType) 
					return false;
			}

			return true;
		}

		private void RowMatch(ScoreManager sm) {
			_isDisabled = true;

			sm.RowMatch(Items, Properties.PointsArray[(int)Items[0].ItemType - 1], _itemMatchDuration, _matchSprite);
		}

		public void Disable() {
			if (_isDisabled) return;

			for (int i = 0; i < Items.Length; i++) {
				Items[i].BoxCollider2D.enabled = false;
			}
		}
	}
}

