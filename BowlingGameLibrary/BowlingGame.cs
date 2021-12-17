using BowlingGameLibrary.Interfaces;

namespace BowlingGameLibrary
{
    public class BowlingGame : IBowlingGame
    {
        public readonly int[] Rolls = new int[21];
        private int rollCounter;
        private int Score { get; set; }

        public BowlingGame() 
        {
            Score = 0;
        }
        
        /// <summary>
        /// Sets the number of pins knocked down during play.
        /// Then increments the Rolls collection with the result of each roll.
        /// </summary>
        /// <param name="pins"></param>
        public void Roll(int pins)
        {
            Rolls[rollCounter] = pins;
            rollCounter++;
        }
        /// <summary>
        /// Determines whether the provided total from the frame is a Strike.
        /// </summary>
        /// <param name="frameTotal"></param>
        /// <returns>True Or False</returns>
        public bool IsStrike(int frameTotal)
        {
            return frameTotal == 10;
        }

        /// <summary>
        /// Determines whether the provided rolls indicate a Spare
        /// </summary>
        /// <param name="roll1"></param>
        /// <param name="roll2"></param>
        /// <returns>True or False</returns>
        public bool IsSpare(int roll1, int roll2)
        {
            return roll1 + roll2 == 10;
        }

        /// <summary>
        /// Returns the score of the current game instance
        /// </summary>
        /// <returns>An integer</returns>
        public int GetScore()
        {
            int i = 0;
            for(int frame=0; frame < 10; frame++)
            {
                int current = Rolls[i];
                int next = Rolls[i + 1];
                int twoAhead = Rolls[i + 2];

                //strike, spare, or open?
                if (IsStrike(current))
                {
                    Score += 10 + next + twoAhead;
                    i += 1;
                }
                else if(IsSpare(current, next))
                {
                    Score += 10 + twoAhead;
                    i += 2;
                }
                else
                {
                    Score += current + next;
                    i += 2;
                }
            }
            return Score;
        }
    }
}
