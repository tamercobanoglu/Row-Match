using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Board {
	public class Row {
		private float _yPos;
		private float _baseXPos;

		public float YPos { get { return _yPos; } }
		public float BaseXPos { get { return _baseXPos; } }

		public List<Item.Item> Items { get; private set; }

		public void Prepare(float yPos, float baseXPos) {
			_yPos = yPos;
			_baseXPos = baseXPos;

			Items = new List<Item.Item>();
		}

		public void AddItem(Item.Item item) {
			Items.Add(item);
		}

		public void RemoveItem(Item.Item item) {
			Items.Remove(item);
		}
	}
}

