using UnityEngine;
using Game.UI;
using Game.Gameplay.Item;
using Game.Gameplay.Board;
using PlayerInfo;

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
					if (Player.Instance.IsRowMatch) checker.Items[j] = item;
					item.transform.position = new Vector3(row.BasePos + j, row.Pos, 0);
				}
			}

			if (!Player.Instance.IsRowMatch) {
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

