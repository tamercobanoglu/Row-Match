using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings {
    public static class Properties {
        public static readonly float ItemSwapDuration = 0.2f;
        public static readonly float FadeOutDuration = 0.25f;

        public static readonly int[] PointsArray = new int[] {
            100, 150, 200, 250
        };

        public static readonly Color32 UnlockedLevelColor = new Color32(109, 168, 80, 255);
        public static readonly Color32 LockedLevelColor = new Color32(140, 140, 140, 255);

        public static readonly int FirstInstallAvailableLevels = 10;


        /// [CLEAN] communication between scenes
        public static int CurrentLevel = 1;
        public static bool HighestScoreAchieved;
    }
}

