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

        private object _player1key;
        public object Player1Key
        {
            get { return _player1key; }
            set
            {
                _player1key = value;
                NotifyOfPropertyChange("PlayerKey");
            }
        }

        //Player2
        public string Player2Name { get; set; }

        private object _player2key;
        public object Player2Key
        {
            get { return _player2key; }
            set
            {
                _player2key = value;
                NotifyOfPropertyChange("Player2Key");
            }
        }

        //Player3
        public string Player3Name { get; set; }

        private object _player3key;
        public object Player3Key
        {
            get { return _player3key; }
            set
            {
                _player3key = value;
                NotifyOfPropertyChange(() => Player3Key);
            }
        }

        //Player4
        public string Player4Name { get; set; }

        private object _player4key;
        public object Player4Key
        {
            get { return _player4key; }
            set
            {
                _player4key = value;
                NotifyOfPropertyChange("PlayerKey");
            }
        }

        public Player Player1 { get; set; } = new Player();
        public Player Player2 { get; set; } = new Player();
        
        public void StartQuiz()
        {
            Player1.Name = Player1Name;
            Player1.Key = Key.LeftShift;
            Player2.Key = Key.RightShift;
            Player2.Name = Player2Name;
            Players.Add(Player1);
            Players.Add(Player2);

            var parentConductor = (Conductor<object>)(this.Parent);
            parentConductor.ActivateItem(new QuestionViewModel());
        }

        //public void SetPlayer1Key(ActionExecutionContext context)
        //{
        //    var keyArgs = context.EventArgs as KeyEventArgs;
        //    Key key = (keyArgs.Key == Key.System ? keyArgs.SystemKey : keyArgs.Key);
        //    Player1Name = key.ToString();
        //    NotifyOfPropertyChange("Player1Name");
        //    Debug.Write(Player1.Name);
        //}
    }
}
