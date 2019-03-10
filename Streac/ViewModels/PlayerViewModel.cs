using Streac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Diagnostics;
using System.Windows.Input;

namespace Streac.ViewModels
{
    public class PlayerViewModel : Caliburn.Micro.Screen
    {
        public static List<Player> Players = new List<Player>();

        //Player1
        public string Player1Name { get; set; }

        //Player2
        public string Player2Name { get; set; }

        //Player3
        public string Player3Name { get; set; }

        //Player4
        public string Player4Name { get; set; }

        public Player Player1 { get; set; } = new Player();
        public Player Player2 { get; set; } = new Player();
        public Player Player3 { get; set; } = new Player();
        public Player Player4 { get; set; } = new Player();

        public void StartQuiz()
        {
            Player1.Key = Key.LeftShift;
            Player2.Key = Key.RightShift;
            Player3.Key = Key.LeftAlt;
            Player4.Key = Key.RightAlt;
            Player1.Name = Player1Name;
            Player2.Name = Player2Name;
            Player3.Name = Player3Name;
            Player4.Name = Player4Name;
            Players.Add(Player1);
            Players.Add(Player2);
            Players.Add(Player3);
            Players.Add(Player4);

            var parentConductor = (Conductor<object>)(this.Parent);
            parentConductor.ActivateItem(new QuestionViewModel());
        }
    }
}
