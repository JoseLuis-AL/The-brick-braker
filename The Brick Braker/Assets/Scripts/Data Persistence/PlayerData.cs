using System;
using System.IO;
using UnityEngine;

namespace Data_Persistence
{
    [Serializable]
    public class PlayerData
    {
        #region Properties ---------------------------------------------------------------------------------------------

        public string name;
        public int score;

        #endregion -----------------------------------------------------------------------------------------------------
        
        
        #region Constructor --------------------------------------------------------------------------------------------

        public PlayerData(string name = "Player", int score = 0)
        {
            this.name = name;
            this.score = score;
        }

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
            var playerData = JsonUtility.FromJson<PlayerData>(json);

            name = playerData.name;
            score = playerData.score;
        }

        #endregion -----------------------------------------------------------------------------------------------------
    }
}