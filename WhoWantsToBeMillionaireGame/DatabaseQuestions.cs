using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsToBeMillionaireGame
{
    public class DatabaseQuestions
    {
        private List<Question> listQuestions;

        public DatabaseQuestions()
        {
            this.listQuestions = new List<Question>();
        }

        public void AddQuestionToDatabase(Question questionToAdd)
        {
            this.listQuestions.Add(questionToAdd);
        }

        public Question GetQuestion(int min, int max, string author)
        {
            List<Question> myTempListToGetQuestionsFrom = new List<Question>();

            myTempListToGetQuestionsFrom = this.listQuestions.Where(x => x.Difficulty >= min).Where(x => x.Difficulty <= max).Where(x => x.Author != author).ToList();

            Random myRandom = new Random();

            int num = myRandom.Next(0, myTempListToGetQuestionsFrom.Count);

            Question questionToGet = myTempListToGetQuestionsFrom[num];
            this.listQuestions.Remove(questionToGet);
            return questionToGet;
        }
    }
}
