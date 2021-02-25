using Settings;
using UnityEngine;

namespace Game.Gameplay.Item {
	public static class ItemFactory {
		private static GameObject _itemBasePrefab;
        private static ItemSkin _itemSkin;

        public static void Prepare(ItemSkin itemSkin) {
            _itemSkin = itemSkin;
        }

        public static Item CreateItem(ItemType itemType, Transform parent, int row, int col) {
            if (_itemBasePrefab == null) {
                _itemBasePrefab = Resources.Load("ItemBase") as GameObject;
            }

            var itemBase = GameObject.Instantiate(
                _itemBasePrefab, Vector3.zero, Quaternion.identity, parent).GetComponent<ItemBase>();

            var item = itemBase.gameObject.AddComponent<Item>();
            item.Prepare(itemType, itemBase, GetSkin(itemType), row, col);

            return item;
        }

        private static Sprite GetSkin(ItemType itemType) {
			Sprite sprite = null;

			switch (itemType) {
				case ItemType.Red:
					sprite = _itemSkin.Red;
					break;
				case ItemType.Green:
					sprite = _itemSkin.Green;
					break;
				case ItemType.Blue:
					sprite = _itemSkin.Blue;
					break;
				case ItemType.Yellow:
					sprite = _itemSkin.Yellow;
					break;
				default:
					Debug.LogWarning($"Can not get skin for the item: {itemType}");
					break;
			}

			return sprite;
		}
    }
}

