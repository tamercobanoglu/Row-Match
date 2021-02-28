using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Settings;

namespace Game.Gameplay.Board {
	public class Row {
		public float YPos { get { return _yPos; } }
		public float BaseXPos { get { return _baseXPos; } }

		public Item.Item[] Items = null;

		private bool _isDisabled;
		private float _yPos;
		private float _baseXPos;
		private Sprite _tickSprite;

		public void Prepare(float yPos, float baseXPos, int gridWidth, Sprite tickSprite) {
			_yPos = yPos;
			_baseXPos = baseXPos;
			_tickSprite = tickSprite;

			Items = new Item.Item[gridWidth];
		}

		/// for horizontally swapped ones
		public void UpdateRowData(int col1, int col2) {
			var temp = Items[col1];
			Items[col1] = Items[col2];
			Items[col2] = temp;
		}

		/// for vertically swapped ones
		public void UpdateRowData(Item.Item item, ScoreManager sm) {
			Items[item.Column] = item;

			if (!IsCompleted()) return;

			RowMatch(sm);
		}

		private bool IsCompleted() {
			for (int i = 1; i < Items.Length; i++) {
				if (Items[i].ItemType != Items[0].ItemType) 
					return false;
			}

			return true;
		}

		private void RowMatch(ScoreManager sm) {
			for (int i = 0; i < Items.Length; i++) {
				Items[i].Complete(_tickSprite);
			}

			_isDisabled = true;
			sm.UpdateScore(Properties.PointsArray[(int)Items[0].ItemType - 1] * Items.Length);
		}

		public void Disable() {
			if (_isDisabled) return;

			for (int i = 0; i < Items.Length; i++) {
				Items[i].BoxCollider2D.enabled = false;
			}
		}
	}
}

