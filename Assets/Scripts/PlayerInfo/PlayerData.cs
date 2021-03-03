using System.Collections;
using System.Collections.Generic;
namespace PlayerInfo {
	[System.Serializable]
	public class PlayerData {
		public bool[] UnlockedLevels;
		public int[] Scores;

		public PlayerData(Player player) {
			UnlockedLevels = player.UnlockedLevels;
			Scores = player.Scores;
		}
	}
}

