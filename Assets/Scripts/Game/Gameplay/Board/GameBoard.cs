using UnityEngine;
using System.Collections;
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

		[HideInInspector] public Checker[] Checkers = null;
		[HideInInspector] public Checker[] Rows = null;
		[HideInInspector] public Item.Item HitItem = null;

		private int _width;
		private int _height;
		private Sprite _matchSprite;

		public void Prepare(LevelInfo levelInfo, Sprite matchSprite) {
			_width = levelInfo.GridWidth;
			_height = levelInfo.GridHeight;
			_matchSprite = matchSprite;

			BoardRenderer.size = new Vector2(_width + 0.2f, _height + 0.2f);

			CreateCheckers();
			CreateRows();
		}

		private void CreateCheckers() {
			var baseYPos = -(float)_height / 2 + 0.5f;
			var baseXPos = -(float)_width / 2 + 0.5f;

			int checkerCount, itemCount;
			float pos, basePos;

			#region arrange parameters
			if (Player.Instance.IsRowMatch) {
				checkerCount = _height;
				itemCount = _width;
				pos = baseYPos;
				basePos = baseXPos;
			}

			else {
				checkerCount = _width;
				itemCount = _height;
				pos = baseXPos;
				basePos = baseYPos;
			}
			#endregion

			Checkers = new Checker[checkerCount];

			for (int i = 0; i < checkerCount; i++) {
				var checker = new Checker();
				checker.Prepare(pos + i, basePos, itemCount, _matchSprite);

				Checkers[i] = checker;
			}
		}

		private void CreateRows() {
			var baseYPos = -(float)_height / 2 + 0.5f;
			var baseXPos = -(float)_width / 2 + 0.5f;

			Rows = new Checker[_height];

			for (int i = 0; i < _height; i++) {
				var row = new Checker();
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

			ScoreManager.UpdateMoveCount();
			/// check out the needed rows
			yield return StartCoroutine(UpdateData(HitItem, item));

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

		IEnumerator UpdateData(Item.Item hitItem, Item.Item item) {
			/// items swapped in the same row or opposite
			if (GetData(hitItem, true) == GetData(item, true)) {
				Checkers[GetData(item, true)].UpdateData(GetData(hitItem, false), GetData(item, false));
				yield break;
			}

			/// items swapped in the same column or opposite
			if (Checkers[GetData(hitItem, true)].UpdateData(hitItem, ScoreManager)) {
				yield return new WaitForSeconds(Checkers[GetData(hitItem, true)].MatchDuration);
			}

			if (Checkers[GetData(item, true)].UpdateData(item, ScoreManager)) {
				yield return new WaitForSeconds(Checkers[GetData(item, true)].MatchDuration);
			}
		}

		private int GetData(Item.Item item, bool groupData) {
			return Player.Instance.IsRowMatch ? 
				groupData ? item.Row : item.Column : 
				groupData ? item.Column : item.Row;
		}

		public void DisableCheckers() {
			for (int i = 0; i < Checkers.Length; i++) {
				Checkers[i].Disable();
			}
		}
	}
}

