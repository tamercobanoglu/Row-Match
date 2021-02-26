using System.Collections;
using System.Collections.Generic;

namespace Player {
	public static class PlayerInfo {

		//public static Dictionary<bool, int> LevelRecords = new Dictionary<bool, int>() {
		//	{ true, 0}, { true, 0}, { true, 0}, { true, 0}, { true, 0},
		//	{ false, 0}, { false, 0}, { false, 0}, { false, 0}, { false, 0},
		//	{ false, 0}, { false, 0}, { false, 0}, { false, 0}, { false, 0},
		//	{ false, 0}, { false, 0}, { false, 0}, { false, 0}, { false, 0},
		//	{ false, 0}, { false, 0}, { false, 0}, { false, 0}, { false, 0}
		//};

		public static bool[] UnlockedLevels = new bool[] {
			true, true, true, true, true,
			false, false, false, false, false,
			false, false, false, false, false,
			false, false, false, false, false,
			false, false, false, false, false
		};

		public static int[] Scores = new int[] {
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
		};
	}
}
