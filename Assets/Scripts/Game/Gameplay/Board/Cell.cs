namespace Game.Gameplay.Board {
	public class Cell {
		public Item.Item Item = null;

		private bool _isDisabled;
		public bool IsDisabled { get { return _isDisabled; } }

		public void Disable() {
			if (_isDisabled) return;
			Item.BoxCollider2D.enabled = false;
			_isDisabled = true;
		}
	}
}

