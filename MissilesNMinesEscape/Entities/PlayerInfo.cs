using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape.Entities
{
    /// <summary>
    /// Player info class to share data inbetween classes 
    /// </summary>
    public class PlayerInfo
    {
        private string playerName;
        private int playerScore;
        private string gameMode;

        public string PlayerName { get => playerName; set => playerName = value; }
        public int PlayerScore { get => playerScore; set => playerScore = value; }
        public string GameMode { get => gameMode; set => gameMode = value; }

        /// <summary>
        /// Default constructor 
        /// </summary>
        public PlayerInfo()
        {
            playerName = "";
            playerScore = 0;
            gameMode = "";
        }
    }

}
