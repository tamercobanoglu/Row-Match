using Game.Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Board {
	public class GameBoard : MonoBehaviour {
		public SpriteRenderer BoardRenderer;
		public Transform ItemsParent;

		[HideInInspector] public Row[] Rows = null;

		private int _width;
		private int _height;

		public void Prepare(LevelInfo levelInfo) {
			_width = levelInfo.GridWidth;
			_height = levelInfo.GridHeight;

			BoardRenderer.size = new Vector2(_width + 0.2f, _height + 0.2f);

			CreateRows();
		}

		private void CreateRows() {
			var baseYPos = -(float)_height / 2 + 0.5f;
			var baseXPos = -(float)_width / 2 + 0.5f;

			Rows = new Row[_height];

			for (int i = 0; i < _height; i++) {
				var row = new Row();
				row.Prepare(baseYPos + i, baseXPos);

				Rows[i] = row;
			}
		}


	}
}

