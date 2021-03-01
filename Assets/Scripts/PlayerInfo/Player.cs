using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo {
	public class Player : MonoBehaviour {
		[HideInInspector] public bool[] UnlockedLevels;
		[HideInInspector] public int[] Scores;

		private int _levelCount;

		public bool Initialize(int levelCount) {
			_levelCount = levelCount;

			if (!LoadPlayer()) {
				CreatePlayer();
				return true;
			}

			return false;
		}

		public void SavePlayer() {
			SaveSystem.SavePlayer(this);
			///Debug.Log("Player saved!");
		}

		private bool LoadPlayer() {
			PlayerData data = SaveSystem.LoadPlayer();

			if (data != null) {
				UnlockedLevels = data.UnlockedLevels;
				Scores = data.Scores;

				///Debug.Log("Player loaded!");

				return true;
			}

			return false;
		}

		private void CreatePlayer() {
			UnlockedLevels = new bool[_levelCount];
			UnlockedLevels[0] = true;

			for (int i = 1; i < UnlockedLevels.Length; i++) {
				UnlockedLevels[i] = false;
			}

			Scores = new int[_levelCount];

			for (int i = 0; i < Scores.Length; i++) {
				Scores[i] = 0;
			}

			///Debug.Log("Player created!");

			SavePlayer();
		}
	}
}
