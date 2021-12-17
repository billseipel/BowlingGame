using BowlingGameLibrary;
using System;

namespace BowlingOnConsole
{
    class Program
    {
        static readonly int tableWidth = 73;
        static readonly int[] numRolls = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 };
        static void Main(string[] args)
        {
            Console.WriteLine("Let's play a game of bowling.\n Press Enter to start playing.\n \n Continue to press Enter to keep rolling the ball.");
            Console.ReadLine();
            BowlingGame game = new BowlingGame();

            int pinsLeft = 10;
            for (int frame = 1; frame < 11; frame++)
            {
                for (int numroll = 0; numroll < numRolls[frame - 1]; numroll++)
                {
                    int result = getRandomBowlingScore(pinsLeft);

                    game.Roll(result);
                    pinsLeft -= result;

                    if (isStrike(result))
                    {
                        PrintStatusMessage(result, pinsLeft, frame, numroll);
                        pinsLeft = 10;
                        if (frame != 10) { break; }
                    }
                    else if (isSpare(result, pinsLeft))
                    {
                        PrintStatusMessage(result, pinsLeft, frame, numroll);
                        pinsLeft = 10;
                        if (frame != 10) { break; }
                    }
                    else
                    {
                        if (frame != 10)
                        {
                            PrintStatusMessage(result, pinsLeft, frame, numroll);
                            if (numroll == 1)
                            {
                                pinsLeft = 10;
                            }
                        }
                        else
                        {
                            if (numroll == 1 && pinsLeft > 0)
                            {
                                PrintStatusMessage(result, pinsLeft, frame, numroll);
                                break;
                            }
                            PrintStatusMessage(result, pinsLeft, frame, numroll);
                        }
                    }
                }
            }

            PrintGrid(game);

            Console.WriteLine($"\n Your score was: {game.GetScore() } \n\n");
            Console.WriteLine("Congratulations! Thank you for playing the game.");
            Console.ReadLine();

        }

        private static void PrintGrid(BowlingGame game)
        {
            int[] rowNums = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            Console.WriteLine(new string('-', tableWidth));
            PrintRow("Frame", "Roll 1", "Roll 2", "Roll 3");
            Console.WriteLine(new string('-', tableWidth));

            int rollnum = 0;
            for (int frame = 1; frame < 11; frame++)
            {
                string[] storedVal = new string[4];
                for (int coll = 0; coll < rowNums[frame - 1]; coll++)
                {
                    if (frame != 10)
                    {
                        if (coll == 0)
                        {
                            storedVal[coll] = frame.ToString();
                        }
                        else if (coll == 1)
                        {
                            if (game.IsStrike(game.Rolls[rollnum]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                storedVal[coll + 1] = " ";
                                storedVal[coll + 2] = " ";
                                rollnum += 1;
                                break;
                            }
                            else
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                        }
                        else if (coll == 2)
                        {
                            if (game.IsStrike(game.Rolls[rollnum]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                storedVal[coll + 1] = " ";
                                rollnum += 1;
                                break;
                            }
                            else if (game.IsSpare(game.Rolls[rollnum], game.Rolls[rollnum + 1]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                storedVal[coll + 1] = " ";
                                rollnum += 1;
                                break;
                            }
                            else
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                        }
                        else if (coll == 3)
                        {
                            storedVal[coll] = " ";
                        }
                    }
                    else //frame 10
                    {
                        if (coll == 0)
                        {
                            storedVal[coll] = frame.ToString();
                        }
                        else if (coll == 1)
                        {
                            if (game.IsStrike(game.Rolls[rollnum]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                            else
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                        }
                        else if (coll == 2)
                        {
                            if (game.IsStrike(game.Rolls[rollnum]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                            else if (game.IsSpare(game.Rolls[rollnum], game.Rolls[rollnum + 1]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                storedVal[coll + 1] = " ";
                                rollnum += 1;
                            }
                            else
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                        }
                        else if (coll == 3)
                        {
                            if (game.IsStrike(game.Rolls[rollnum]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                            }
                            else if (game.IsSpare(game.Rolls[rollnum], game.Rolls[rollnum - 1]))
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                storedVal[coll + 1] = " ";
                                rollnum += 1;
                                break;
                            }
                            else
                            {
                                storedVal[coll] = game.Rolls[rollnum].ToString();
                                rollnum += 1;
                            }
                        }
                    }
                }

                PrintRow(storedVal);
            }
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        private static void PrintStatusMessage(int result, int pinsLeft, int frame, int roll)
        {
            roll++;
            if (frame != 10)
            {
                if (isStrike(result))
                {
                    Console.WriteLine($"Frame:{frame}. Roll:{roll} :Strike! You hit {result} pins and have {pinsLeft} left.");
                    Console.WriteLine("Resetting pins. Press Enter to roll again.");

                }
                else if (isSpare(result, pinsLeft))
                {
                    Console.WriteLine($"Frame {frame}. Roll:{roll} :Spare! You hit {result} pins and have {pinsLeft} left.");
                    Console.WriteLine("Resetting pins. Press Enter to roll again.");
                }
                else
                {
                    Console.WriteLine($"Frame {frame}. Roll:{roll}: Open frame. You hit {result} pins and have {pinsLeft} left. ");
                    if (roll == 2) { Console.WriteLine("Resetting pins. Press Enter to roll again."); }
                }
            }
            else  //frame 10
            {
                if (roll == 3 && pinsLeft > 0)
                {
                    Console.WriteLine($"Frame {frame}. Roll:{roll} :Open Frame! You hit {result} pins and have {pinsLeft} left. \n\n Game Over \n\n Press Enter to view your score.");
                }
                else if (roll == 2 && pinsLeft > 0)
                {
                    Console.WriteLine($"Frame {frame}. Roll:{roll} :Open Frame! You hit {result} pins and have {pinsLeft} left. \n\n Game Over \n\n Press Enter to view your score.");
                }
                else
                {
                    if (isStrike(result))
                    {
                        Console.WriteLine($"Frame {frame}. Roll:{roll} :Strike! You hit {result} pins and have {pinsLeft} left.");
                        Console.WriteLine("Resetting pins. Press Enter to roll again.");
                    }
                    else if (isSpare(result, pinsLeft))
                    {
                        Console.WriteLine($"Frame {frame}. Roll:{roll} :Spare! You hit {result} pins and have {pinsLeft} left.");
                        Console.WriteLine("Resetting pins. Press Enter to roll again.");

                    }
                    else
                    {
                        Console.WriteLine($"Frame {frame}. Roll:{roll} :Open frame. You hit {result} pins and have {pinsLeft} left. ");
                    }
                }
            }
            Console.ReadLine();
        }

        private static bool isSpare(int result, int pinsLeft)
        {
            return result > 0 && pinsLeft == 0;
        }

        private static bool isStrike(int result)
        {
            return result == 10;
        }

        private static int getRandomBowlingScore(int max)
        {
            //max + 1 because just max will never INCLUDE the max number
            Random num = new Random();
            return num.Next(max + 1);
        }
    }
}
