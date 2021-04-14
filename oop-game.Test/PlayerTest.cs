using System;
using Xunit;
using oop_game;
namespace oop_game.Test
{
    public class PlayerTest
    {
        GameSession gamesession = new GameSession();
        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 5)]
        [InlineData(1, 1)]
        [InlineData(1, 3)]
        [InlineData(4,1)]
        //Theory with inline data
        public void Test_IsValidMove(int x, int y)
        {
            Assert.True(gamesession.IsValidMove(x, y));
        }

        [Theory]
        [InlineData(101)]
        public void Test_Levelup_GivesCorrectExp(int value)
        {
            gamesession.currentPlayer.ExperiencePoints += value;
            Assert.Equal(1, gamesession.currentPlayer.ExperiencePoints);
            
        }
        [Fact]
        public void Test_Levelup_callsLevelupWhenOver100()
        {

            gamesession.currentPlayer.ExperiencePoints += 105;
            Assert.Equal(2, gamesession.currentPlayer.level);
        }
    }
}
