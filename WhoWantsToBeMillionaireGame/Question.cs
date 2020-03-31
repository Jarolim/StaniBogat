using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhoWantsToBeMillionaireGame
{
    public class Question
    {
        private Dictionary<char, KeyValuePair<char, string>> questionsToPrint;

        public Question()
        {
            this.AnswersWrong = new List<string>();
            this.questionsToPrint = new Dictionary<char, KeyValuePair<char, string>>();
        }

        public int Difficulty { get; set; }

        public string Author { get; set; }

        public string QuestionSentence { get; set; }

        public string Subject { get; set; }

        public string AnswerRight { get; set; }

        public List<string> AnswersWrong { get; set; }               

        public void SetQuestionToPresent()
        {
            List<char> myListLetters = new List<char> { 'A', 'B', 'C', 'D' };
            Random random = new Random();

            for (int i = 0; i < 12; i++)
            {
                int num = random.Next(0, myListLetters.Count);
                Thread.Sleep(i + num);
                char letter = myListLetters[num];
                myListLetters.Remove(letter);
                myListLetters.Add(letter);
            }            

            KeyValuePair<char, string> myKeyValuePair1 = new KeyValuePair<char, string>('N', this.AnswersWrong[0]);
            KeyValuePair<char, string> myKeyValuePair2 = new KeyValuePair<char, string>('N', this.AnswersWrong[1]);
            KeyValuePair<char, string> myKeyValuePair3 = new KeyValuePair<char, string>('N', this.AnswersWrong[2]);
            KeyValuePair<char, string> myKeyValuePair4 = new KeyValuePair<char, string>('Y', this.AnswerRight);

            this.questionsToPrint.Add(myListLetters[0], myKeyValuePair1);
            this.questionsToPrint.Add(myListLetters[1], myKeyValuePair2);
            this.questionsToPrint.Add(myListLetters[2], myKeyValuePair3);
            this.questionsToPrint.Add(myListLetters[3], myKeyValuePair4);

            this.questionsToPrint = this.questionsToPrint.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public void SetQuestion5050()
        {
            List<char> myListLetters = new List<char> { 'D', 'C', 'A', 'B' };
            Random random = new Random();

            for (int i = 0; i < 13254; i++)
            {
                int num = random.Next(0, myListLetters.Count);
                char letter = myListLetters[num];
                myListLetters.Remove(letter);
                myListLetters.Add(letter);
            }

            int numDeleted = 0;
            for (int ii = 0; ii < int.MaxValue; ii++)
            {
                if (this.questionsToPrint[myListLetters[ii]].Key == 'N')
                {
                    this.questionsToPrint.Remove(myListLetters[ii]);
                    numDeleted++;
                    if (numDeleted == 2)
                    {
                        break;
                    }
                }
            }
        }

        public bool IsAnswerCorrect(char answerChar)
        {
            if (this.questionsToPrint[answerChar].Key == 'Y')
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuildQuestionToPrint = new StringBuilder();
            stringBuildQuestionToPrint.AppendLine($"\\\\{this.Subject}\\\\");
            stringBuildQuestionToPrint.AppendLine(this.QuestionSentence);
            stringBuildQuestionToPrint.AppendLine();
            foreach (var item in this.questionsToPrint)
            {
                stringBuildQuestionToPrint.AppendLine($"{item.Key}) {item.Value.Value}");
            }
            return stringBuildQuestionToPrint.ToString();
        }

        public string Print5050()
        {
            StringBuilder stringBuildQuestionToPrint5050 = new StringBuilder();
            stringBuildQuestionToPrint5050.AppendLine();
            foreach (var item in this.questionsToPrint)
            {
                stringBuildQuestionToPrint5050.AppendLine($"{item.Key}) {item.Value.Value}");
            }
            return stringBuildQuestionToPrint5050.ToString();
        }
    }
}

