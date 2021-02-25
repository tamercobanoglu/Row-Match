using Game.Gameplay.Board;
using Game.Gameplay.Item;
using Settings;
using UnityEngine;

namespace Game.Gameplay.Level {
	public class LevelManager : MonoBehaviour {
		public int LevelNum;
		public GameBoard GameBoard;
		public ItemSkin ItemSkin;

		public LevelInfo[] LevelInfos;

		void Start() {
			ItemFactory.Prepare(ItemSkin);
			GameBoard.Prepare(LevelInfos[LevelNum - 1], ItemSkin.Tick);
			PrepareLevel(LevelInfos[LevelNum - 1]);
		}

		private void PrepareLevel(LevelInfo levelInfo) {
			for (int i = 0; i < levelInfo.GridHeight; i++) {

				var row = GameBoard.Rows[i];

				for (int j = 0; j < levelInfo.GridWidth; j++) {

					var itemType = levelInfo.Items[i * levelInfo.GridWidth + j];

					var item = ItemFactory.CreateItem(itemType, GameBoard.ItemsParent, i, j);
					if (item == null) continue;

					row.Items[j] = item;
					item.transform.position = new Vector3(row.BaseXPos + j, row.YPos, 0);
				}
			}
		}
	}
}

