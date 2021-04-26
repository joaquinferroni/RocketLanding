using Test.Zartis;
using System;
using Xunit;
using FluentAssertions;
namespace Test.Zartis.UnitTest.DomainTests
{
    public class LandingAreaShould
    {
        [InlineData(10,50,5,5)]
        [InlineData(50,50,10,10)]
        [InlineData(1000,1000,50,30)]
        [InlineData(4,4,2, 2)]
        [Theory]
        public void Return_A_New_Instance_Because_Of_Valid_Parameters(int width, int height,int platformWidth, int platformHeight)
        {
            var landingPlatform = LandingPlatform.Create(platformWidth,platformHeight,new Point(1,1)).Value;
            var landingAreaResult = LandingArea.Create(width,height, landingPlatform);
            landingAreaResult.IsSuccess.Should().BeTrue();
            landingAreaResult.Message.Should().BeNullOrEmpty();
            Assert.IsType<LandingArea>(landingAreaResult.Value);
        }


        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [Theory]
        public void Return_An_Error_Message_Because_Of_Invalid_Size(int width, int height)
        {
            var landingPlatform = LandingPlatform.Create(5,5,new Point(1,1)).Value;
            var landingAreaResult = LandingArea.Create(width, height, landingPlatform);
            landingAreaResult.IsSuccess.Should().BeFalse();
            landingAreaResult.Message.Should().Contain("should be higher than 1");
        }

        [InlineData(10,50,5,5,6,10)]
        [InlineData(50,50,10,10,50,50)]
        [InlineData(1000,1000,50,30,951,0)]
        [InlineData(4,4,2, 2,3,2)]
        [InlineData(4,4,6, 6,0,0)]
        [Theory]
        public void Return_An_Error_Message_Because_Platform_Could_Not_Be_Placed(int width, int height,int platformWidth, int platformHeight,int startPointX,int startPointY)
        {
            var landingPlatform = LandingPlatform.Create(platformWidth,platformHeight,new Point(startPointX,startPointY)).Value;
            var landingAreaResult = LandingArea.Create(width, height, landingPlatform);
            landingAreaResult.IsSuccess.Should().BeFalse();
            landingAreaResult.Message.Should().Contain("Landing Platrofrm could not be placed.");
        }
    }
}