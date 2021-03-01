using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PlayerInfo {
	public static class SaveSystem {
		public static void SavePlayer(Player player) {
			BinaryFormatter formatter = new BinaryFormatter();

			string path = Path.Combine(Application.persistentDataPath, "player.bin");

			using (FileStream stream = new FileStream(path, FileMode.Create)) {
				PlayerData data = new PlayerData(player);
				formatter.Serialize(stream, data);
			}
		}

		public static PlayerData LoadPlayer() {
			string path = Path.Combine(Application.persistentDataPath, "player.bin");

			if (File.Exists(path)) {
				BinaryFormatter formatter = new BinaryFormatter();

				using (FileStream stream = new FileStream(path, FileMode.Open)) {
					PlayerData data = formatter.Deserialize(stream) as PlayerData;

					return data;
				}
			}

			else {
				///Debug.Log($"Save file not found in {path}");
				return null;
			}
		}
	}
}

