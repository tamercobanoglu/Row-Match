using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Board {
	public class Row {
		public float YPos { get { return _yPos; } }
		public float BaseXPos { get { return _baseXPos; } }

		public Item.Item[] Items = null;

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
		public void UpdateRowData(Item.Item item, int col) {
			Items[col] = item;

			if (!IsCompleted()) return;

			RowMatch();
		}

		private bool IsCompleted() {
			for (int i = 1; i < Items.Length; i++) {
				if (Items[i].ItemType != Items[0].ItemType) 
					return false;
			}

			return true;
		}

		private void RowMatch() {
			for (int i = 0; i < Items.Length; i++) {
				Items[i].Complete(_tickSprite);
			}
		}
	}
}

