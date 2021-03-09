﻿using UnityEngine;

namespace Settings {
    public static class Properties {
        public static readonly float ItemSwapDuration = 0.2f;
        public static readonly float ButtonAnimDuration = 0.2f;
        public static readonly float FadeOutDuration = 0.25f;

        public static readonly int[] PointsArray = new int[] {
            100, 150, 200, 250
        };

        /// for levels button and play buttons
        public static readonly Color32 GreenButtonColor = new Color32(109, 168, 80, 255);
        public static readonly Color32 LockedGreenButtonColor = new Color32(140, 140, 140, 255);
        public static readonly Color32 PressedGreenButtonColor = new Color32(76, 121, 53, 255);

        /// for choice panel buttons and match type button
        public static readonly Color32 OrangeButtonColor = new Color32(159, 95, 0, 255);
        public static readonly Color32 PressedOrangeButtonColor = new Color32(111, 66, 2, 255);

        public static readonly int FirstInstallAvailableLevels = 10;
    }
}
