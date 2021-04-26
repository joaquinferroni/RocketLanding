using Test.Zartis;
using System;
using Xunit;
using FluentAssertions;
namespace Test.Zartis.UnitTest.DomainTests
{
    public class LandingPlatformShould
    {
        [InlineData(10,50)]
        [InlineData(50,50)]
        [InlineData(1000,1000)]
        [InlineData(2, 2)]
        [Theory]
        public void Return_A_New_Instance_Because_Of_Valid_Parameters(int width, int height)
        {
            var landingPlatformResult = LandingPlatform.Create(width,height, new Point(0,0));
            landingPlatformResult.IsSuccess.Should().BeTrue();
            landingPlatformResult.Message.Should().BeNullOrEmpty();
            Assert.IsType<LandingPlatform>(landingPlatformResult.Value);
        }


        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [Theory]
        public void Return_An_Error_Message_Because_Of_Invalid_Parameters(int width, int height)
        {
            var landingPlatformResult = LandingPlatform.Create(width, height, new Point(0, 0));
            landingPlatformResult.IsSuccess.Should().BeFalse();
            landingPlatformResult.Message.Should().NotBeNullOrEmpty();
        }
    }
}