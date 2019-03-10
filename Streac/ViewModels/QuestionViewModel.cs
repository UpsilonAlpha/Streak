using Streac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caliburn.Micro;
using System.Diagnostics;
using System.Windows.Input;

namespace Streac.ViewModels
{
    public class QuestionViewModel : Caliburn.Micro.Screen
    {
        Quiz Quiz = new Quiz(MenuViewModel.FileName);

        private string _isRight = "No Answer";
        private string _player1Active = "No";
        private string _player2Active = "No";
        private string _player3Active = "No";
        private string _player4Active = "No";
        private string _selectedTerm;
        private string _answer;
        private string _question;

        static int currentQuestion = 0;
        bool buzzerPressed = false;
        int printIndex;

        public BindableCollection<string> Terms { get; set; } = new BindableCollection<string>();
        public string IsRight
        {
            get { return _isRight; }
            set
            {
                _isRight = value;
                NotifyOfPropertyChange(() => IsRight);
            }
        }
        public string SelectedTerm
        {
            get { return _selectedTerm; }
            set
            {
                _selectedTerm = value;
                NotifyOfPropertyChange(() => SelectedTerm);
            }
        }
        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                NotifyOfPropertyChange(() => Answer);
            }
        }
        public string Question
        {
            get {return _question; }
            set
            {
                _question = value;
                NotifyOfPropertyChange(() => Question);
            }
        }
        public Player currentPlayer;

        Timer PrinTimer = new Timer();
        Timer AnswerTimer = new Timer();

        public string Player1Name { get; set; } = PlayerViewModel.Players[0].Name;
        public string Player2Name { get; set; } = PlayerViewModel.Players[1].Name;
        public string Player3Name { get; set; } = PlayerViewModel.Players[2].Name;
        public string Player4Name { get; set; } = PlayerViewModel.Players[3].Name;
        public int Player1Score { get; set; } = PlayerViewModel.Players[0].Points;
        public int Player2Score { get; set; } = PlayerViewModel.Players[1].Points;
        public int Player3Score { get; set; } = PlayerViewModel.Players[2].Points;
        public int Player4Score { get; set; } = PlayerViewModel.Players[3].Points;
        public string Player1Active
        {
            get { return _player1Active; }
            set
            {
                _player1Active = value;
                NotifyOfPropertyChange(() => Player1Active);
            }
        }
        public string Player2Active
        {
            get { return _player2Active; }
            set
            {
                _player2Active = value;
                NotifyOfPropertyChange(() => Player2Active);
            }
        }
        public string Player3Active
        {
            get { return _player3Active; }
            set
            {
                _player3Active = value;
                NotifyOfPropertyChange(() => Player3Active);
            }
        }
        public string Player4Active
        {
            get { return _player4Active; }
            set
            {
                _player4Active = value;
                NotifyOfPropertyChange(() => Player4Active);
            }
        }

        public QuestionViewModel()
        {
            LisTerms();
            PrintDef();
        }

        public void PrintDef()
        {
            PrinTimer.Tick += new EventHandler(Printerval);
            PrinTimer.Interval = 100;
            PrinTimer.Start();
        }

        private void Printerval(object Object, EventArgs eventArgs)
        {
            if (buzzerPressed == false)
            {
                PrinTimer.Stop();
                if (printIndex < Quiz.GetDef(currentQuestion).Length)
                {
                    Question = Question + Quiz.GetDef(currentQuestion)[printIndex];
                    printIndex++;
                    PrinTimer.Start();
                }
            }
            else
            {
                PrinTimer.Stop();
            }
        }

        public void LisTerms()
        {
            for (int i = 0; i < Quiz.GetNumberOfQuestions(); i++)
            {
                Terms.Add(Quiz.GetTerm(i));
            }
        }

        public void KeyDown(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as System.Windows.Input.KeyEventArgs;

            Debug.WriteLine(keyArgs.Key.ToString());

            if (keyArgs.Key == Key.Enter && buzzerPressed == true)
            {
                AnswerCheck();
            }
            else if (keyArgs.Key == Key.LeftShift && buzzerPressed != true)
            {
                Player1Active = "Yes";
                currentPlayer = PlayerViewModel.Players[0];
            }
            else if (keyArgs.Key == Key.RightShift && buzzerPressed != true)
            {
                Player2Active = "Yes";
                currentPlayer = PlayerViewModel.Players[1];
            }
            else if (keyArgs.SystemKey == Key.LeftAlt && buzzerPressed != true)
            {
                Player3Active = "Yes";
                currentPlayer = PlayerViewModel.Players[2];
                keyArgs.Handled = true;
            }
            else if (keyArgs.SystemKey == Key.RightAlt && buzzerPressed != true)
            {
                Player4Active = "Yes";
                currentPlayer = PlayerViewModel.Players[3];
                keyArgs.Handled = true;
            }

            buzzerPressed = true;
        }

        public void AnswerCheck()
        {
            if (Answer == Quiz.GetTerm(currentQuestion))
            {
                IsRight = "Correct";
                if (currentPlayer != null)
                {
                    currentPlayer.Points++;
                }
            }

            else
            {
                IsRight = "Incorrect";
            }

            AnswerTimer.Tick += new EventHandler(NextDef);
            AnswerTimer.Interval = 1000;
            AnswerTimer.Start();
            currentQuestion++;
        }

        public void NextDef(object Object, EventArgs eventArgs)
        {
            if (currentQuestion < Quiz.GetNumberOfQuestions())
            {
                AnswerTimer.Stop();
                PrinTimer.Stop();
                var parentConductor = (Conductor<object>)(this.Parent);
                parentConductor.ActivateItem(new QuestionViewModel());
            }
            
        }
    }
}
