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
        public int Player1Score { get; set; } = PlayerViewModel.Players[0].Points;
        public int Player2Score { get; set; } = PlayerViewModel.Players[1].Points;

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

            buzzerPressed = true;
            if (keyArgs.Key == Key.LeftShift)
            {
                currentPlayer = PlayerViewModel.Players[0];
                currentPlayer.Active = true;
            }
            else if (keyArgs.Key == Key.RightShift)
            {
                currentPlayer = PlayerViewModel.Players[1];
                currentPlayer.Active = true;
            }
            else if (keyArgs.Key == Key.Enter && buzzerPressed == true)
            {
                AnswerCheck();
            }
        }

        public void AnswerCheck()
        {
            if (Answer == Quiz.GetTerm(currentQuestion))
            {
                IsRight = "Correct";
                currentPlayer.Points++;
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
