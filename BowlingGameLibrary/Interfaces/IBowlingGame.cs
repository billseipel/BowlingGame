using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGameLibrary.Interfaces
{
    public interface IBowlingGame
    {
        void Roll(int pins);
        bool IsStrike(int frameTotal);
        bool IsSpare(int roll1, int roll2);
        int GetScore();


    }
}
