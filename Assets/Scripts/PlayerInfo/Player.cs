using UnityEngine;

namespace PlayerInfo {
	public class Player : MonoBehaviour {
		[HideInInspector] public bool[] UnlockedLevels;
		[HideInInspector] public int[] Scores;

		public static Player Instance { get { return instance; } }
		private static Player instance;

		/// communication between scenes
		[HideInInspector] public int CurrentLevel;
		[HideInInspector] public bool HighestScoreAchieved;
		[HideInInspector] public bool OldLevelRecord;
		[HideInInspector] public bool IsRowMatch;

		private int _levelCount;

		public bool Initialize(int levelCount) {
			if (instance == null) {
				instance = this;
			}

			else if (instance != this) {
				Destroy(this);
			}

			_levelCount = levelCount;

			if (!LoadPlayer()) {
				CreatePlayer();
				return true;
			}

			return false;
		}

		private void SavePlayer() {
			SaveSystem.SavePlayer(this);
		}

		private bool LoadPlayer() {
			PlayerData data = SaveSystem.LoadPlayer();

			if (data != null) {
				UnlockedLevels = data.UnlockedLevels;
				Scores = data.Scores;
				CurrentLevel = data.CurrentLevel;
				IsRowMatch = data.IsRowMatch;

				return true;
			}

			return false;
		}

		private void CreatePlayer() {
			UnlockedLevels = new bool[_levelCount];
			UnlockedLevels[0] = true;
			CurrentLevel = 1;
			IsRowMatch = true;

			for (int i = 1; i < UnlockedLevels.Length; i++) {
				UnlockedLevels[i] = false;
			}

			Scores = new int[_levelCount];

			for (int i = 0; i < Scores.Length; i++) {
				Scores[i] = 0;
			}

			SavePlayer();
		}

		public void UpdatePlayer(int score) {
			if (score > Scores[CurrentLevel - 1]) {

				/// unlock next level if it exists
				if (CurrentLevel != UnlockedLevels.Length) {
					OldLevelRecord = UnlockedLevels[CurrentLevel];

					if (!UnlockedLevels[CurrentLevel]) {
						UnlockedLevels[CurrentLevel] = true;
					}
				}

				Scores[CurrentLevel - 1] = score;
				HighestScoreAchieved = true;
				SavePlayer();
				return;
			}

			HighestScoreAchieved = false;
			SavePlayer();
		}

		public void UpdatePlayer(bool isRowMatch) {
			IsRowMatch = isRowMatch;
			SavePlayer();
		}
	}
}
