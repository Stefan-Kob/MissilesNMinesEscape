/*
 * Programmed by: Kihoon Kim and Stefan Kobetich
 * Game Name: MissilesNMinesEscape 
 * Revision history:
 *          01-DEC 2023: Project created
 *          02-DEC 2023: Designed game
 *          03-DEC 2023: Make some entity
 *          03-DEC 2023: Complete collision
 *          05-DEC 2023: Debugging process
 *          06-DEC 2023: Organize scenes
 *          08-DEC 2023: Debugging complete
 *          09-DEC 2023: Project complete        
 */
using MissilesNMinesEscape.Entities;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape.Manager
{
    /*
     *SavingScoreManager.cs
     *Saving score manager class, used to handle the saving, seeding, and loading of the score data
     *
     *Revision History : Finished
     *Kihoon/Stefan, 2023/12/4: Created
     */
    /// <summary>
    /// The class of saving scroe at end of the game
    /// </summary>
    public class SavingScoreManager
    {
        /// <summary>
        /// Constructor of SavaingScoreManger class
        /// </summary>
       
        public SavingScoreManager() 
        {
            MakingFile();
            LoadTop5HighScores();
        }


        private static string projectPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static string folderPath = System.IO.Path.Combine(projectPath,"MissilesNMinesEscape","GameScore");
        private static string logsPath = Path.Combine(folderPath,"GameScores.txt");

        /// <summary>
        /// Method for file creation
        /// </summary>
        public void MakingFile()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (!File.Exists(logsPath))
            {
                var textFile = File.Create(logsPath);
                textFile.Close();
                SeedPlayerScores();
            }

        }

        /// <summary>
        /// Method to seed some data to the file
        /// </summary>
        private void SeedPlayerScores()
        {
            using StreamWriter streamWriter = File.AppendText(logsPath);

            string line1 = "Stefan,1200,Hard Mode";
            string line2 = "Kihoon,300,Easy Mode";
            string line3 = "Jimyung,900,Hard Mode";
            string line4 = "Will,400,Easy Mode";
            string line5 = "Kisun,800,Hard Mode";

            streamWriter.WriteLine(line1);
            streamWriter.WriteLine(line2);
            streamWriter.WriteLine(line3);
            streamWriter.WriteLine(line4);
            streamWriter.WriteLine(line5);
        }

        /// <summary>
        /// Method for loading the highscore leaderboard
        /// </summary>
        /// <returns>highScores</returns>
        public static List<PlayerInfo> LoadGameScores()
        {
            List<PlayerInfo> highScores = new List<PlayerInfo>();
            string tempPlayerInfo;

            using (StreamReader sr = new StreamReader(logsPath))
            {
                // tempInfor is like just One line
                while ((tempPlayerInfo = sr.ReadLine()) != null)
                {
                    string[] eachScore = tempPlayerInfo.Split(',');
                    PlayerInfo playerInfo = new PlayerInfo
                    {
                        PlayerName = eachScore[0],
                        PlayerScore = int.Parse(eachScore[1]),
                        GameMode = eachScore[2]
                    };
                    highScores.Add(playerInfo);
                }
            }
            return highScores;
        }

        /// <summary>
        /// Method for returning the top 5 top scores from the list to display
        /// </summary>
        /// <returns></returns>
        public static List<PlayerInfo> LoadTop5HighScores()
        {
            List<PlayerInfo> allHighScores = LoadGameScores();
            return allHighScores.OrderByDescending(score => score.PlayerScore).Take(5).ToList();
        }

        /// <summary>
        /// Method for adding a new player's score to the file
        /// </summary>
        /// <param name="playerInfo"></param>
        public void AddNewPlayerInfo(PlayerInfo playerInfo)
        {
            using StreamWriter streamWriter = File.AppendText(logsPath);

            string line = $"{playerInfo.PlayerName},{playerInfo.PlayerScore},{playerInfo.GameMode}";
            streamWriter.WriteLine(line);

        }
    }
}
