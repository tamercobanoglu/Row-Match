using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings {
    public static class Properties {
        public static readonly float AnimationDuration = 0.2f;

        public static readonly int RedItemPoint = 100;
        public static readonly int GreenItemPoint = 150;
        public static readonly int BlueItemPoint = 200;
        public static readonly int YellowItemPoint = 250;

        public static readonly Color32 UnlockedLevelColor = new Color32(109, 168, 80, 255);
        public static readonly Color32 LockedLevelColor = new Color32(140, 140, 140, 255);

        public static int LevelNum = 1;
    }
}

