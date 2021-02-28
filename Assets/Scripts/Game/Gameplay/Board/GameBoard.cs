using Game.Gameplay.Level;
using Game.Mechanics;
using Utils;
using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Player;

namespace Game.Gameplay.Board {
	public class GameBoard : MonoBehaviour {
		public ScoreManager ScoreManager;
		public SpriteRenderer BoardRenderer;
		public Transform ItemsParent;

		[HideInInspector] public Row[] Rows = null;
		[HideInInspector] public GameState State = GameState.None;
		[HideInInspector] public Item.Item HitItem = null;

		private int _width;
		private int _height;
		private Sprite _tickSprite;

		public void Prepare(LevelInfo levelInfo, Sprite tickSprite) {
			_width = levelInfo.GridWidth;
			_height = levelInfo.GridHeight;
			_tickSprite = tickSprite;

			BoardRenderer.size = new Vector2(_width + 0.2f, _height + 0.2f);

			CreateRows();
		}

		private void CreateRows() {
			var baseYPos = -(float)_height / 2 + 0.5f;
			var baseXPos = -(float)_width / 2 + 0.5f;

			Rows = new Row[_height];

			for (int i = 0; i < _height; i++) {
				var row = new Row();
				row.Prepare(baseYPos + i, baseXPos, _width, _tickSprite);

				Rows[i] = row;
			}
		}

		public void ItemTapped(Item.Item item) {
			HitItem = item;
			State = GameState.SelectionStarted;
		}

		public void SwapAttempt(Item.Item item) {
			/// if the two items are diagonally aligned (different row and column), just return
			if (!Utilities.AreVerticalOrHorizontalNeighbors(HitItem,
				item)) {
				State = GameState.None;
				return;
			}

			State = GameState.Animating;
			Utilities.FixSortingLayer(HitItem.SpriteRenderer, item.SpriteRenderer);
			StartCoroutine(Swap(item));
		}

		private IEnumerator Swap(Item.Item item) {
			/// move the swapped ones
			SwapItems(HitItem, item);
			yield return new WaitForSeconds(Properties.ItemSwapDuration);

			/// check out the needed rows
			CheckRows(HitItem, item);

			/// check move count
			ScoreManager.MoveSpent();

			State = GameState.None;
		}

		private void SwapItems(Item.Item hitItem, Item.Item item) {
			Utilities.SwapItemData(hitItem, item);

			var hitItemPos = hitItem.transform.position;
			var itemPos = item.transform.position;

			hitItem.transform.DOMove(itemPos, Properties.ItemSwapDuration);
			item.transform.DOMove(hitItemPos, Properties.ItemSwapDuration);
		}

		private void CheckRows(Item.Item hitItem, Item.Item item) {
			/// items swapped in the same row
			if (hitItem.Row == item.Row) {
				Rows[item.Row].UpdateRowData(hitItem.Column, item.Column);
				return;
			}

			/// items swapped in the same column
			Rows[hitItem.Row].UpdateRowData(hitItem, ScoreManager);
			Rows[item.Row].UpdateRowData(item, ScoreManager);
		}

		private void DisableRows() {
			for (int i = 0; i < Rows.Length; i++) {
				Rows[i].Disable();
			}
		}

		public void EndGame() {
			State = GameState.Ended;
			DisableRows();
		}
	}
}

