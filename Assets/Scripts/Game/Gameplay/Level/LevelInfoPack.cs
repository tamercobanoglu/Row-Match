using UnityEngine;

namespace Game.Gameplay.Level {
    [CreateAssetMenu(menuName = "Levels/Level Info Pack", fileName = "Level Info Pack")]
    public class LevelInfoPack : ScriptableObject {
        public LevelInfo[] Levels;
    }
}
