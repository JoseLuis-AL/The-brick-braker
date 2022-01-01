using System;
using System.IO;
using UnityEngine;

namespace Data_Persistence
{
    [Serializable]
    public class ScoreboardData
    {
        #region Atributtes ---------------------------------------------------------------------------------------------

        public int highScore;
        public PlayerData player1 = new PlayerData();
        public PlayerData player2 = new PlayerData();
        public PlayerData player3 = new PlayerData();

        #endregion -----------------------------------------------------------------------------------------------------


        #region Methods ------------------------------------------------------------------------------------------------

        public void SaveToFile(string file)
        {
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/" + file, json);
        }

        public void LoadFromFile(string file)
        {
            var path = Application.persistentDataPath + "/" + file;

            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);
            var scoreboardData = JsonUtility.FromJson<ScoreboardData>(json);

            player1 = scoreboardData.player1;
            player2 = scoreboardData.player2;
            player3 = scoreboardData.player3;

            highScore = player1.score;
        }

        public void AddPlayer(PlayerData newPlayer)
        {
            // Check if new player score should be in the scoreboard.
            if (newPlayer.score > player1.score)
            {
                player3 = player2;
                player2 = player1;
                player1 = newPlayer;
                highScore = newPlayer.score;
            }
            else if (newPlayer.score > player2.score)
            {
                player3 = player2;
                player2 = newPlayer;
            }
            else if (newPlayer.score > player3.score)
            {
                player3 = newPlayer;
            }
        }

        public void ClearScoreboard()
        {
            highScore = 0;
            player1 = new PlayerData();
            player2 = new PlayerData();
            player3 = new PlayerData();
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}