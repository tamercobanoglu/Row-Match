using UnityEngine;
using System.Collections;
using Game.UI;
using Game.Mechanics;
using Game.Gameplay.Level;
using Utils;
using Settings;
using PlayerInfo;
using DG.Tweening;
using System.Linq;

namespace Game.Gameplay.Board {
	public class GameBoard : MonoBehaviour {
		public UIGameplay UIManager;
		public SpriteRenderer BoardRenderer;
		public Transform ItemsParent;

		public Cell[,] Cells;
		[HideInInspector] public Item.Item HitItem = null;

		private int _width;
		private int _height;
		private Sprite _matchSprite;

		public void Prepare(LevelInfo levelInfo, Sprite matchSprite) {
			_width = levelInfo.GridWidth;
			_height = levelInfo.GridHeight;
			_matchSprite = matchSprite;

			BoardRenderer.size = new Vector2(_width + 0.2f, _height + 0.2f);

			CreateCells();
		}

		private void CreateCells() {
			var baseYPos = -(float)_height / 2 + 0.5f;
			var baseXPos = -(float)_width / 2 + 0.5f;

			Cells = new Cell[_height, _width];

			ItemsParent.localPosition = new Vector3(baseXPos, baseYPos, 0);

			for (int i = 0; i < _height; i++) {
				for (int j = 0; j < _width; j++) {
					var cell = new Cell();
					Cells[i, j] = cell;
				}
			}
		}

		public void ItemTapped(Item.Item item) {
			HitItem = item;
			UIManager.State = GameState.SelectionStarted;
		}

		public void SwapAttempt(Item.Item item) {
			/// if the two items are diagonally aligned (different row and column), just return
			if (!Utilities.AreVerticalOrHorizontalNeighbors(HitItem,
				item)) {
				UIManager.State = GameState.None;
				return;
			}

			UIManager.State = GameState.Animating;
			Utilities.FixSortingLayer(HitItem.SpriteRenderer, item.SpriteRenderer);
			StartCoroutine(Swap(item));
		}

		private IEnumerator Swap(Item.Item item) {
			/// move the swapped ones
			SwapItems(HitItem, item);
			yield return new WaitForSeconds(Properties.ItemSwapDuration);

			UIManager.ScoreManager.UpdateMoveCount();
			/// check out the needed rows
			yield return StartCoroutine(UpdateData(HitItem, item));

			/// check move count
			UIManager.ScoreManager.MoveSpent();

			UIManager.State = GameState.None;
		}

		private void SwapItems(Item.Item hitItem, Item.Item item) {
			Utilities.SwapItemData(hitItem, item);

			var hitItemPos = hitItem.transform.position;
			var itemPos = item.transform.position;

			hitItem.transform.DOMove(itemPos, Properties.ItemSwapDuration);
			item.transform.DOMove(hitItemPos, Properties.ItemSwapDuration);
		}

		IEnumerator UpdateData(Item.Item hitItem, Item.Item item) {
			/// no need to check if any match occurs
			if (GetData(hitItem) == GetData(item)) {
				SwapData(hitItem, item);
				yield break;
			}

			var itemCount = Player.Instance.IsRowMatch ? _width : _height;

			/// need to check if any match occurs
			if (SwapData(hitItem)) {
				yield return new WaitForSeconds(hitItem.MatchDuration * itemCount);
			}

			if (SwapData(item)) {
				yield return new WaitForSeconds(item.MatchDuration * itemCount);
			}

			yield return new WaitForSeconds(0.1f);
		}

		#region Checker Methods
		/// for swaps that do not need to be checked if any match occurs
		public void SwapData(Item.Item item1, Item.Item item2) {
			var temp = Cells[item1.Row, item1.Column];
			Cells[item1.Row, item1.Column] = Cells[item2.Row, item2.Column];
			Cells[item2.Row, item2.Column] = temp;
		}

		/// for swaps that need to be checked if any match occurs
		public bool SwapData(Item.Item item) {
			Cells[item.Row, item.Column].Item = item;
			var subCells = Cells.SliceArray(GetData(item)).ToArray();

			if (!IsCompleted(subCells))
				return false;

			Match(subCells);
			return true;
		}

		private bool IsCompleted(Cell[] cells) {
			for (int i = 1; i < cells.Length; i++) {
				if (cells[i].Item.ItemType != cells[0].Item.ItemType) {
					return false;
				}
			}

			return true;
		}

		private void Match(Cell[] cells) {
			StartCoroutine(MatchingProcess(cells, Properties.PointsArray[(int)cells[0].Item.ItemType - 1], cells[0].Item.MatchDuration, _matchSprite));
		}

		IEnumerator MatchingProcess(Cell[] cells, int pointPerItem, float itemMatchDuration, Sprite matchSprite) {
			System.Random rnd = new System.Random();
			var direction = rnd.Next(2);

			for (int i = 0; i < cells.Length; i++) {
				var j = direction == 0 ? i : cells.Length - i - 1;

				cells[j].Disable();
				cells[j].Item.Animate(itemMatchDuration);
				yield return new WaitForSeconds(itemMatchDuration);

				cells[j].Item.Complete(matchSprite);
				UIManager.ScoreManager.UpdateScoreText(pointPerItem);
			}
		}
		#endregion

		private int GetData(Item.Item item) {
			return Player.Instance.IsRowMatch ? item.Row : item.Column;
		}

		public void DisableCells() {
			for (int i = 0; i < _height; i++) {
				for (int j = 0; j < _width; j++) {
					Cells[i, j].Disable();
				}
			}
		}
	}
}

