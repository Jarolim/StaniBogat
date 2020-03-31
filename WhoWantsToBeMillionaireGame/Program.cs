using System;
using System.IO;
using System.Text;
using System.Threading;

namespace WhoWantsToBeMillionaireGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            //GET DATABASE FROM FILE

            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

            Console.Title = "СТАНИ-THE-SEXYS-SKYPE-CALL-БОГАТ";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetWindowSize(192, 42);
            Console.Clear();

            Console.WriteLine($"Loading... Please wait!");

            string[] linesAll = File.ReadAllLines(@"WhoWantsToBeMillionaireTotal.txt");

            int numberOfLinesAll = linesAll.Length;
            int numberOfQuestions = numberOfLinesAll / 8;

            DatabaseQuestions myDatabaseQuestions = new DatabaseQuestions();

            for (int questionCount = 0; questionCount < numberOfQuestions; questionCount++)
            {
                Console.Write("_"); //LOADING BAR = 1 UNDERSCORE FOR EACH NEW QUESTION ADDED IN DATABASE

                Question myQuestion = new Question();

                for (int line = 0; line < 8; line++)
                {
                    string inputLine = linesAll[line + 8 * questionCount];

                    switch (line)
                    {
                        case 0:
                            myQuestion.Subject = inputLine;
                            break;
                        case 1:
                            myQuestion.Difficulty = int.Parse(inputLine);
                            break;
                        case 2:
                            myQuestion.QuestionSentence = inputLine;
                            break;
                        case 3:
                            myQuestion.AnswerRight = inputLine;
                            break;
                        case 4:
                            myQuestion.AnswersWrong.Add(inputLine);
                            break;
                        case 5:
                            myQuestion.AnswersWrong.Add(inputLine);
                            break;
                        case 6:
                            myQuestion.AnswersWrong.Add(inputLine);
                            break;
                        case 7:
                            myQuestion.Author = inputLine.Remove(0, 1);
                            break;
                    } // SET PROPERTIES OF QUESTION
                }

                myQuestion.SetQuestionToPresent();

                myDatabaseQuestions.AddQuestionToDatabase(myQuestion);
            }

            //GAME            

            Game myGame = new Game();

            while (true)
            {
                PlayIntroMain();

                ChooseGameType();

                Console.Write($"Въведи името си (на латиница): ");

                string playerName = Console.ReadLine();

                Player myPlayer = new Player(playerName);

                Console.Clear();
                Console.WriteLine($"Въведи 3 имена за жокера \"ОБАДИ СЕ НА ПРИЯТЕЛ\":");

                string friend1 = Console.ReadLine();
                string friend2 = Console.ReadLine();
                string friend3 = Console.ReadLine();

                myPlayer.HintCallFriends = new string[3] { friend1, friend2, friend3 };

                Thread.Sleep(1000);
                Console.Clear();

                ExplainRules(playerName);

                for (int stage = 1; stage <= 16; stage++)
                {
                    if (stage == 16)
                    {
                        PlayIntroMain();
                        Console.Clear();
                        Console.WriteLine("ТИ СПЕЧЕЛИ СТАНИ БОГАТ!");
                        Thread.Sleep(2000);
                        Console.WriteLine("ТРЪГВАШ СИ СЪС 100.000 ЛВ!");
                        Thread.Sleep(2000);
                        Console.WriteLine("ЧЕСТИТО!");
                        Thread.Sleep(4000);
                        Console.Clear();
                        PlayIntroNewQuestion();
                        Console.Clear();
                        break;
                    } // YOU WIN 100.000 AFTER 15TH STAGE!
                    if (stage != 1)
                    {
                        PlayIntroNewQuestion();
                    }

                    PrintHints(myPlayer.Hints);

                    PrintTable(stage);

                    switch (stage)
                    {
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Внимание! Въпрос за първа сигурна сума!\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case 10:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Внимание! Внимание! Въпрос за втора сигурна сума!!\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case 15:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Внимание! Внимание! Внимание! Въпрос за 100.000 лв!!!\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                    } // N.B. MESSAGE FOR QUESTIONS 5/10/15 FOR GUARANTEED PROFIT

                    int min = 0;
                    int max = 0;

                    if (stage >= 1 && stage <= 3)
                    {
                        min = 1;
                        max = 3;
                    }
                    else if (stage >= 4 && stage <= 6)
                    {
                        min = 4;
                        max = 6;
                    }
                    else if (stage >= 7 && stage <= 9)
                    {
                        min = 7;
                        max = 9;
                    }
                    else if (stage >= 10 && stage <= 12)
                    {
                        min = 10;
                        max = 12;
                    }
                    else if (stage >= 13 && stage <= 15)
                    {
                        min = 13;
                        max = 15;
                    }

                    Question questionToAsk = myDatabaseQuestions.GetQuestion(min, max, playerName);
                    Console.WriteLine(questionToAsk);

                    char charCommand;

                    while (true)
                    {
                        Console.Write($"Въведи отговор A / B / C / D или H (жокер) или Q (отказвам се): ");
                        string answer = Console.ReadLine();

                        try
                        {
                            charCommand = char.Parse(answer);
                        }
                        catch (Exception)
                        {
                            ThrowManualException();
                            continue;
                        }

                        if (charCommand == 'A' || charCommand == 'B' || charCommand == 'C' || charCommand == 'D')
                        {
                            break;
                        }
                        else if (charCommand == 'H')
                        {
                            if (myPlayer.Hints[0] == 0 && myPlayer.Hints[1] == 0 && myPlayer.Hints[2] == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"Вече са използвани и трите ти жокера!");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Thread.Sleep(2000);
                                continue;
                            }
                            else
                            {
                                int[] hintsLeft = myPlayer.Hints;

                                Console.WriteLine($"\nИзбери един от възможните жокери:");

                                for (int n = 0; n < hintsLeft.Length; n++)
                                {
                                    Console.ForegroundColor = ConsoleColor.Gray;

                                    if (hintsLeft[n] == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    }

                                    if (n == 0)
                                    {
                                        Console.WriteLine($"1. 50/50");
                                    }
                                    else if (n == 1)
                                    {
                                        Console.WriteLine($"2. CALL!");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"3. O O O");
                                    }

                                }
                                Console.ForegroundColor = ConsoleColor.Gray;

                                int index;

                                while (true)
                                {
                                    Console.Write($"Въведи цифрата на съответния възможен жокер (1/2/3 + Enter): ");

                                    string commandHintIndex = Console.ReadLine();

                                    try
                                    {
                                        index = int.Parse(commandHintIndex);
                                    }
                                    catch
                                    {
                                        ThrowManualException();
                                        continue;
                                    }

                                    if ((index == 1 && hintsLeft[0] == 1) || (index == 2 && hintsLeft[1] == 1) || (index == 3 && hintsLeft[2] == 1))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        ThrowManualException();
                                        continue;
                                    }
                                }

                                hintsLeft[index - 1] = 0;
                                myPlayer.Hints = hintsLeft;

                                if (index == 1)
                                {
                                    Console.WriteLine($"\n{myPlayer.Name}, ти избра жокер \"50/50\"!");
                                    questionToAsk.SetQuestion5050();
                                    Console.WriteLine(questionToAsk.Print5050());
                                } // HINT -> 50/50
                                if (index == 2) 
                                {
                                    Console.WriteLine($"\n{myPlayer.Name}, ти избра жокер \"Обади се на приятел\"!");
                                    Console.WriteLine($"\nТвоите 3 възможности са:");
                                    int num = 0;
                                    foreach (var item in myPlayer.HintCallFriends)
                                    {
                                        num++;
                                        Console.WriteLine($"{num}) {item}");
                                    }

                                    int friendToCallInteger;

                                    while (true)
                                    {
                                        Console.Write($"Посочи на кого да се обадим (1/2/3) и натисни Enter: ");

                                        string friendToCall = Console.ReadLine();

                                        try
                                        {
                                            friendToCallInteger = int.Parse(friendToCall);

                                            if (friendToCallInteger >= 1 && friendToCallInteger <= 3)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                ThrowManualException();
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            ThrowManualException();
                                        }
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine($"Добре, нека се обадим на {myPlayer.HintCallFriends[friendToCallInteger - 1]}...");

                                    PlayPhoneCallSound();

                                    Console.WriteLine($"\nРазполагаш с точно половин минута, за да зададеш въпроса си.");
                                    Console.Write($"Натисни Enter за старт на хронометър (30 сек): ");
                                    Console.ReadLine();
                                    
                                    StartStopwatch();
                                } // HINT -> CALL A FRIEND!
                                if (index == 3) 
                                {
                                    Console.WriteLine($"\n{myPlayer.Name}, ти избра жокер \"Помощ от публиката\"!");
                                    Console.WriteLine($"\nУчастникът затваря очи.");
                                    Console.WriteLine($"Всеки гост в публиката записва верния според него отговор на лист и го показва на екрана.");
                                    Console.WriteLine($"Водещият записва всички отговори, пресмята и \"подава\" резултат в %-ти...");
                                    Console.Write($"Натисни Enter, за да продължиш: ");
                                    Console.ReadLine();
                                    Console.WriteLine();
                                } // HINT -> HELP FROM AUDIENCE
                            }
                        }
                        else if (charCommand == 'Q')
                        {
                            Console.WriteLine("\nДлъжен съм да те попитам още веднъж.");
                            Console.WriteLine("Отказваш ли се от играта си в \"Стани Богат\"?");
                            Console.Write("Отговори с Y (yes) или N (no) + Enter: ");

                            string quitCommand = Console.ReadLine();

                            while (true)
                            {
                                if (quitCommand == "Y" || quitCommand == "N")
                                {
                                    break;
                                }
                                else
                                {
                                    ThrowManualException();
                                }

                                quitCommand = Console.ReadLine();
                            }

                            if (quitCommand == "Y")
                            {
                                break;
                            }
                            else if (quitCommand == "N")
                            {
                                continue;
                            }
                        }
                        else
                        {
                            ThrowManualException();
                        }
                    }

                    if (charCommand == 'Q')
                    {
                        QuitByWill(myPlayer.ProfitCurrent);
                        PlayIntroNewQuestion();
                        break;
                    }
                    else if (charCommand == 'A' || charCommand == 'B' || charCommand == 'C' || charCommand == 'D')
                    {
                        if (questionToAsk.IsAnswerCorrect(charCommand))
                        {
                            Thread.Sleep(4000);
                            Console.WriteLine($"\nВерен отговор!");
                            Thread.Sleep(4000);
                            myPlayer.RaiseProfitCurrent(myGame.Prices[stage]);
                        }
                        else
                        {
                            Thread.Sleep(4000);
                            Console.WriteLine($"\nГрешен отговор!");
                            Thread.Sleep(4000);
                            LostGame(myPlayer.PayProfitGuaranteed());
                            break;
                        }
                    }
                }
            }
        }

        public static void ChooseGameType()
        {
            Console.Clear();
            Thread.Sleep(600);
            Console.WriteLine($"1) SINGLE-PLAYER");
            Thread.Sleep(600);
            Console.WriteLine($"2) MULTIPLAYER");
            Thread.Sleep(600);

            int gameTypeInteger = 0;

            while (true)
            {
                Console.Write($"Избери 1 или 2 и натисни Enter: ");

                try
                {
                    gameTypeInteger = int.Parse(Console.ReadLine());

                    if (gameTypeInteger == 1 || gameTypeInteger == 2)
                    {
                        break;
                    }
                    else
                    {
                        ThrowManualException();
                    }
                }
                catch
                {
                    ThrowManualException();
                    continue;
                }
            }

            if (gameTypeInteger == 2)
            {
                ChooseRandomPlayer();
            }

            Console.Clear();
        }

        public static void ChooseRandomPlayer()
        {
            Console.Clear();

            int numberOfPlayers = 0;

            while (true)
            {
                Console.Write($"Въведи брой играчи и натисни Enter: ");

                try
                {
                    numberOfPlayers = int.Parse(Console.ReadLine());

                    if (numberOfPlayers > 0 && numberOfPlayers < 100)
                    {
                        break;
                    }
                    else
                    {
                        ThrowManualException();
                    }
                }
                catch
                {
                    ThrowManualException();
                    continue;
                }                
            }

            Console.Clear();
            Console.WriteLine($"Изчакай джуркането да приключи...");
            Thread.Sleep(1500);
            Console.Clear();

            Random randomPlayer = new Random();

            int indexPlayerInteger = randomPlayer.Next(1, numberOfPlayers);

            for (int j = 1; j < int.MaxValue; j++)
            {
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    Console.Write(i + ") ");
                    Thread.Sleep(10);

                    if (j == 3 && i == indexPlayerInteger)
                    {
                        break;
                    }

                    Console.Clear();
                }
                if (j == 3)
                {
                    break;
                }
            }
            Thread.Sleep(1500);
            Console.Write($"\n\nНатисни Enter, за да продължиш: ");
            Console.ReadLine();
        }

        public static void PlayPhoneCallSound()
        {
            Thread.Sleep(1000);
            for (int i = 0; i < 2; i++)
            {
                Console.Beep(500, 500);
                Thread.Sleep(100);
                Console.Beep(500, 900);
                Thread.Sleep(1000);
            }
            Console.Beep(500, 500);
            Thread.Sleep(100);
        }

        public static void PlaySound()
        {
            Console.Beep(250, 250);
            Console.Beep(250, 200);
            Console.Beep(250, 150);
            Console.Beep(200, 250);
            Console.Beep(200, 200);
            Console.Beep(200, 150);
            Console.Beep(150, 250);
            Console.Beep(150, 200);
            Console.Beep(150, 150);
            Console.Beep(100, 1000);
        }

        public static void PlayMusic()
        {
            Console.Beep(1000, 500);
            Console.Beep(1100, 500);
            Console.Beep(1150, 500);
            Console.Beep(1000, 500);
            Console.Beep(1100, 600);
            Console.Beep(1150, 700);
            Console.Beep(1250, 800);
            Console.Beep(1350, 1000);
        }

        public static void PlayIntroMain()
        {
            Console.Clear();
            new Thread(PlayMusic).Start();
            Console.WriteLine($"~~~ Тан - тааан - тан - тааан - таааан - тааааан - тан - таааааааан! ~~~");
            Thread.Sleep(5000);
            Console.Clear();
        }

        public static void PlayIntroNewQuestion()
        {
            Console.Clear();
            Thread.Sleep(500);
            new Thread(PlaySound).Start();            
            Console.WriteLine($"~~~ Тирудидудудудуууууууууууууу ~~~");
            Thread.Sleep(2000);
            Console.Clear();
            Thread.Sleep(500);
        }

        public static void ExplainRules(string playerName)
        {
            Thread.Sleep(1500);
            Console.Clear();

            Console.WriteLine($"{playerName}, правилата:");
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine($"15 въпроса за 100 000 лв.\n");
            Thread.Sleep(1000);
            PrintTable(0);            
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine($"15 въпроса за 100 000 лв.\n");
            PrintTable(5);
            Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine($"15 въпроса за 100 000 лв.\n");
            PrintTable(10);
            Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine($"15 въпроса за 100 000 лв.\n");
            PrintTable(15);
            Thread.Sleep(750);
            Console.Clear();

            Console.WriteLine($"2 сигурни суми:\n");
            PrintTable(0);
            Thread.Sleep(1500);
            Console.Clear();

            Console.WriteLine($"500 лв. на 5-ти въпрос...\n");
            PrintTable(5);
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine($"... и 2500 лв. на 10-ти въпрос.\n");
            PrintTable(10);
            Thread.Sleep(2000);
            Console.Clear();

            Thread.Sleep(1000);

            Console.WriteLine($"3 жокера:\n");
            Thread.Sleep(1000);
            PrintHints(new int[3] { 1, 1, 1 });
            Thread.Sleep(1000);
            Console.Clear();

            Console.WriteLine($"\"50/50\" - елиминира 2 грешни отговора...\n");
            PrintHints(new int[3] { 0, 1, 1 });
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine($"...\"ОБАДИ СЕ НА ПРИЯТЕЛ\"...\n");
            PrintHints(new int[3] { 1, 0, 1 });
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine($"...и \"ПОМОЩ ОТ ПУБЛИКАТА\".\n");
            PrintHints(new int[3] { 1, 1, 0 });
            Thread.Sleep(2000);
            Console.Clear();

            Thread.Sleep(500);

            Console.WriteLine($"Готов/a ли си, {playerName}?");
            Console.ReadLine();
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine($"Твоята игра вече започна!");
            Thread.Sleep(2000);
            Console.Clear();

            PlayIntroNewQuestion();
        }

        public static void PrintHints(int[] hints)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"| ");
            if (hints[0] == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write($"50:50");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" | ");
            if (hints[1] == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write($"CALL!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" | ");
            if (hints[2] == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write($"O O O");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" |");            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void PrintTable(int lineCurrent)
        {
            int sum = 0;

            for (int j = 15; j >= 1; j--)
            {
                switch (j)
                {
                    case 15:
                        sum = 100000;
                        break;
                    case 14:
                        sum = 20000;
                        break;
                    case 13:
                        sum = 15000;
                        break;
                    case 12:
                        sum = 10000;
                        break;
                    case 11:
                        sum = 5000;
                        break;
                    case 10:
                        sum = 2500;
                        break;
                    case 9:
                        sum = 2000;
                        break;
                    case 8:
                        sum = 1500;
                        break;
                    case 7:
                        sum = 1000;
                        break;
                    case 6:
                        sum = 700;
                        break;
                    case 5:
                        sum = 500;
                        break;
                    case 4:
                        sum = 300;
                        break;
                    case 3:
                        sum = 200;
                        break;
                    case 2:
                        sum = 100;
                        break;
                    case 1:
                        sum = 50;
                        break;
                }

                if (j == lineCurrent)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }

                if (j == 5 || j == 10 || j == 15)
                {                    
                    Console.ForegroundColor = ConsoleColor.White;                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;                    
                }

                Console.WriteLine($"{j}) {sum}");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            Console.WriteLine();
        }        
        
        public static void QuitByWill(int profitCurrentToGoWith)
        {
            Console.Clear();
            Console.WriteLine($"Ти се отказа от играта си в \"Стани Богат\"!");
            Thread.Sleep(2000);
            Console.WriteLine($"Тръгваш си със сумата от {profitCurrentToGoWith} лв.");
            Thread.Sleep(2000);
            Console.WriteLine($"Честито!");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void LostGame(int profitGuaranteedToGoWith)
        {
            Console.Clear();
            Console.WriteLine($"Ти прекратяваш участието си в \"Стани Богат\"!");
            Thread.Sleep(2000);
            Console.WriteLine($"Тръгваш си със сигурна сума от {profitGuaranteedToGoWith} лв.");
            Thread.Sleep(2000);
            Console.WriteLine($"Довиждане!");
            Thread.Sleep(2000);
            Console.Clear();
        }
        
        public static void ThrowManualException()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Invalid input. Try again!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(100);
        }

        public static void StartStopwatch()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int j = 1; j <= 30; j++)
            {
                Console.Write(j + " ");
                Thread.Sleep(1000);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Beep(1000, 1000);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
