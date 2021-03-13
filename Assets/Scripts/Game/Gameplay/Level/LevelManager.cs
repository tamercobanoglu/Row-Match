using UnityEngine;
using Game.UI;
using Game.Gameplay.Item;
using Game.Gameplay.Board;

namespace Game.Gameplay.Level {
	public class LevelManager : MonoBehaviour {
		public GameBoard GameBoard;

		private UIGameplay _uiManager;

		public void Initialize(UIGameplay uiManager, int currentLevel, LevelInfo[] levels) {
			_uiManager = uiManager;

			ItemFactory.Prepare(_uiManager.Skin);
			GameBoard.Prepare(levels[currentLevel - 1], _uiManager.Skin.Tick);
			PrepareLevel(levels[currentLevel - 1]);
		}

		private void PrepareLevel(LevelInfo levelInfo) {
			var cells = GameBoard.Cells;

			for (int i = 0; i < levelInfo.GridHeight; i++) {
				for (int j = 0; j < levelInfo.GridWidth; j++) {

					var itemType = levelInfo.Items[i * levelInfo.GridWidth + j];

					var item = ItemFactory.CreateItem(itemType, GameBoard.ItemsParent, i, j);
					if (item == null) continue;

					cells[i, j].Item = item;
					item.transform.localPosition = new Vector3(item.Column, item.Row, 0);
				}
			}
		}
	}
}

