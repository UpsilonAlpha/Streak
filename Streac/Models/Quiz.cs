using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Streac.Models
{
    class Quiz
    {
        private List<Question> _questions;

        public Quiz(String filepath)
        {
            string json = File.ReadAllText(filepath);
            _questions = JsonConvert.DeserializeObject<List<Question>>(json);
        }

        public String GetTerm(int number)
        {
            return _questions[number].Term;
        }

        public String GetDef(int number)
        {
            return _questions[number].Definition;
        }

        public int GetNumberOfQuestions()
        {
            return _questions.Count();
        }
    }
}
