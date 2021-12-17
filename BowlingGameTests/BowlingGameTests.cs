using BowlingGameLibrary;
using BowlingGameLibrary.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BowlingGameTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BowlingGameTests {

        private BowlingGame TestBowlingGame { get; set; }
        //private Mock<IBowlingGameThing> BowlingGameThing

        [TestInitialize]
        public void Intialize()
        {
            // initialize the members of the BowlingGame
            // MockBowlingGameThing = new Mock<IBowlingGameThing>();
            // BowlingGame = new BowlingGame(MockBowlingGameThing.Object)
            // Initialize any test data here

            TestBowlingGame = new BowlingGame();
        }

        [TestMethod]
        public void Game_Can_Be_Created()
        {
            TestBowlingGame = new BowlingGame();
            //asserts
            Assert.IsNotNull(TestBowlingGame);
        }

        [TestMethod]
        public void Roll_Spare_on_First_Frame()
        {
            TestBowlingGame.Roll(9);
            TestBowlingGame.Roll(1);
            // 20 - 2 rolls = 18 remaining rolls
            // first two rolls knocked down 10 pins
            // Spare  10 + next roll, or 9 + 1 + 1 = 11
            RollGame(18, 1);
            Assert.AreEqual(TestBowlingGame.GetScore(), 29);
        }

        [TestMethod]
        public void Roll_All_Spares()
        {
            // roll 21 frames, 5 per roll
            RollGame(21, 5);
            Assert.AreEqual(TestBowlingGame.GetScore(), 150);
        }

        [TestMethod]
        public void Roll_All_Gutter_Balls()
        {
            RollGame(20, 0);
            Assert.AreEqual(TestBowlingGame.GetScore(), 0);
        }

        [TestMethod]
        public void Roll_All_Strikes()
        {
            RollGame(12, 10);
            Assert.AreEqual(TestBowlingGame.GetScore(), 300);
        }

        [TestMethod]
        public void Roll_All_Twos()
        {
            RollGame(20, 2);
            Assert.AreEqual(TestBowlingGame.GetScore(), 40);
        }

        [TestMethod]
        public void Roll_Spares_8_2()
        {
            for(int i=0; i < 10; i++)
            {
                TestBowlingGame.Roll(8);
                TestBowlingGame.Roll(2);
            }
            TestBowlingGame.Roll(8);
            Assert.AreEqual(TestBowlingGame.GetScore(), 180);
        }

        [TestMethod]
        public void TypicalGame_NoBonusRoll()
        {
            TestBowlingGame.Roll(0); TestBowlingGame.Roll(10); 
            TestBowlingGame.Roll(4); TestBowlingGame.Roll(4);  
            TestBowlingGame.Roll(5); TestBowlingGame.Roll(5);  
            TestBowlingGame.Roll(5); TestBowlingGame.Roll(0);  
            TestBowlingGame.Roll(6); TestBowlingGame.Roll(4);  
            TestBowlingGame.Roll(4); TestBowlingGame.Roll(4);  
            TestBowlingGame.Roll(10);                          
            TestBowlingGame.Roll(10);                          
            TestBowlingGame.Roll(4); TestBowlingGame.Roll(4);
            TestBowlingGame.Roll(4); TestBowlingGame.Roll(4);
            Assert.AreEqual(TestBowlingGame.GetScore(), 122);

        }

        [ExcludeFromCodeCoverage]
        public void RollGame(int rolls, int pinsknockedDown)
        {
            for (int i = 0; i < rolls; i++)
            {
                TestBowlingGame.Roll(pinsknockedDown);
            }
        }
    }
    
       

    
}
