using UnityEngine;
using Game.Gameplay.Item;
using Game.Gameplay.Board;
using Settings;

namespace Game.Gameplay.Level {
	public class LevelManager : MonoBehaviour {
		public GameBoard GameBoard;
		public Skin Skin;

		public void Initialize(int currentLevel, LevelInfo[] levels) {
			ItemFactory.Prepare(Skin);
			GameBoard.Prepare(levels[currentLevel - 1], Skin.Tick);
			PrepareLevel(levels[currentLevel - 1]);
		}

		private void PrepareLevel(LevelInfo levelInfo) {
			for (int i = 0; i < levelInfo.GridHeight; i++) {

				var row = GameBoard.Rows[i];
				Checker checker = null;

				if (i < GameBoard.Checkers.Length) {
					checker = GameBoard.Checkers[i];
				}

				for (int j = 0; j < levelInfo.GridWidth; j++) {

					var itemType = levelInfo.Items[i * levelInfo.GridWidth + j];

					var item = ItemFactory.CreateItem(itemType, GameBoard.ItemsParent, i, j);
					if (item == null) continue;

					row.Items[j] = item;
					if (Properties.isRowMatch) checker.Items[j] = item;
					item.transform.position = new Vector3(row.BaseXPos + j, row.YPos, 0);
				}
			}

			if (!Properties.isRowMatch) {
				for (int i = 0; i < levelInfo.GridWidth; i++) {

					var Rows = GameBoard.Rows;
					var checker = GameBoard.Checkers[i];

					for (int j = 0; j < levelInfo.GridHeight; j++) {
						checker.Items[j] = Rows[j].Items[i];
					}
				}
			}
		}
	}
}

