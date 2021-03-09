namespace PlayerInfo {
	[System.Serializable]
	public class PlayerData {
		public bool[] UnlockedLevels;
		public int[] Scores;
		public int CurrentLevel;
		public bool IsRowMatch;

		public PlayerData(Player player) {
			UnlockedLevels = player.UnlockedLevels;
			Scores = player.Scores;
			CurrentLevel = player.CurrentLevel;
			IsRowMatch = player.IsRowMatch;
		}
	}
}

