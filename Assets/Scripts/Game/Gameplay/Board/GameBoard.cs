using System.Collections;
using UnityEngine;
using Game.UI;
using Game.Mechanics;
using Game.Gameplay.Level;
using Utils;
using Settings;
using PlayerInfo;
using DG.Tweening;

namespace Game.Gameplay.Board {
	public class GameBoard : MonoBehaviour {
		public UIGameplay UIManager;
		public ScoreManager ScoreManager;
		public SpriteRenderer BoardRenderer;
		public Transform ItemsParent;

		[HideInInspector] public Row[] Rows = null;
		[HideInInspector] public Item.Item HitItem = null;

		private int _width;
		private int _height;
		private Sprite _matchSprite;

		public void Prepare(LevelInfo levelInfo, Sprite matchSprite) {
			_width = levelInfo.GridWidth;
			_height = levelInfo.GridHeight;
			_matchSprite = matchSprite;

			BoardRenderer.size = new Vector2(_width + 0.2f, _height + 0.2f);

			CreateRows();
		}

		private void CreateRows() {
			var baseYPos = -(float)_height / 2 + 0.5f;
			var baseXPos = -(float)_width / 2 + 0.5f;

			Rows = new Row[_height];

			for (int i = 0; i < _height; i++) {
				var row = new Row();
				row.Prepare(baseYPos + i, baseXPos, _width, _matchSprite);

				Rows[i] = row;
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

			/// check out the needed rows
			if (CheckRows(HitItem, item)) {
				ScoreManager.UpdateMoveCount();

				yield return new WaitForSeconds(Rows[0].RowMatchDuration);
			}

			else {
				ScoreManager.UpdateMoveCount();
			}

			/// check move count
			ScoreManager.MoveSpent();

			UIManager.State = GameState.None;
		}

		private void SwapItems(Item.Item hitItem, Item.Item item) {
			Utilities.SwapItemData(hitItem, item);

			var hitItemPos = hitItem.transform.position;
			var itemPos = item.transform.position;

			hitItem.transform.DOMove(itemPos, Properties.ItemSwapDuration);
			item.transform.DOMove(hitItemPos, Properties.ItemSwapDuration);
		}

		private bool CheckRows(Item.Item hitItem, Item.Item item) {
			/// items swapped in the same row
			if (hitItem.Row == item.Row) {
				Rows[item.Row].UpdateRowData(hitItem.Column, item.Column);
				return false;
			}

			/// items swapped in the same column
			var a = Rows[hitItem.Row].UpdateRowData(hitItem, ScoreManager);
			var b = Rows[item.Row].UpdateRowData(item, ScoreManager);

			return a || b;
		}

		public void DisableRows() {
			for (int i = 0; i < Rows.Length; i++) {
				Rows[i].Disable();
			}
		}

		public void EndGame() {
			UIManager.State = GameState.Ended;
		}
	}
}

