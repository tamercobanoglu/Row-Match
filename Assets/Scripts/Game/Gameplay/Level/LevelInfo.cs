using UnityEngine;
using Game.Gameplay.Item;

namespace Game.Gameplay.Level {
    [CreateAssetMenu(menuName = "Levels/Level Info", fileName = "LevelInfo_Num")]
    public class LevelInfo : ScriptableObject {
        public int LevelNum;
        public int GridWidth;
        public int GridHeight;
        public int MoveCount;

        public ItemType[] Items;
    }
}
