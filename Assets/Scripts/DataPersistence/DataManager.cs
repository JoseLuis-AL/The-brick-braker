using System;
using System.IO;
using UnityEngine;

namespace DataPersistence
{
    public static class DataManager
    {
        #region Constants ----------------------------------------------------------------------------------------------

        public const string CurrentPlayerDataFile = "currentPlayerData.json";

        public const string ScoreboardDataFile = "scoreboardData.json";

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------

        public static void SavePlayerData(PlayerData newPlayerData, string file)
        {
            var json = JsonUtility.ToJson(newPlayerData);
            File.WriteAllText(Application.persistentDataPath + "/" + file, json);
        }

        public static PlayerData LoadPlayerData(string file)
        {
            var path = Application.persistentDataPath + "/" + file;

            if (!File.Exists(path)) return new PlayerData();

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }

        public static void SavePlayerToScoreboard(PlayerData newPlayerData, string file)
        {
            // Scoreboard data
            ScoreboardData scoreboardData;

            // Load saved players.
            var path = Application.persistentDataPath + "/" + file;
            string json;
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
                scoreboardData = JsonUtility.FromJson<ScoreboardData>(json);
            }
            else
            {
                scoreboardData = NewScoreboardData();
            }

            // Check if new player score should be in the scoreboard.
            if (newPlayerData.score > scoreboardData.Player1.score)
            {
                scoreboardData.Player3 = scoreboardData.Player2;
                scoreboardData.Player2 = scoreboardData.Player1;
                scoreboardData.Player1 = newPlayerData;
            }
            else if (newPlayerData.score > scoreboardData.Player2.score)
            {
                scoreboardData.Player3 = scoreboardData.Player2;
                scoreboardData.Player2 = newPlayerData;
            }
            else if (newPlayerData.score > scoreboardData.Player3.score)
            {
                scoreboardData.Player3 = newPlayerData;
            }

            // Save the new player scoreboard.
            json = JsonUtility.ToJson(scoreboardData);
            File.WriteAllText(Application.persistentDataPath + "/" + file, json);
        }

        public static ScoreboardData LoadScoreboard(string file)
        {
            var path = Application.persistentDataPath + "/" + file;

            if (!File.Exists(path)) return NewScoreboardData();

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<ScoreboardData>(json);

            return data;
        }

        private static ScoreboardData NewScoreboardData()
        {
            const string defaultPlayerName = "Player";
            return new ScoreboardData
            {
                Player1 =
                {
                    name = defaultPlayerName,
                    score = 0
                },
                Player2 =
                {
                    name = defaultPlayerName,
                    score = 0
                },
                Player3 =
                {
                    name = defaultPlayerName,
                    score = 0
                }
            };
        }

        #endregion

        #region Structs ------------------------------------------------------------------------------------------------

        [Serializable]
        public class PlayerData
        {
            public string name;
            public int score;

            public PlayerData(string name = "", int score = 0)
            {
                this.name = name;
                this.score = score;
            }
        }

        [SerializeField]
        public class ScoreboardData
        {
            public PlayerData Player1 = new PlayerData();
            public PlayerData Player2 = new PlayerData();
            public PlayerData Player3 = new PlayerData();

            public ScoreboardData()
            {
            }
        }

        #endregion
    }
}