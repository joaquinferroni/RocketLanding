using System;
using FluentAssertions;
using Xunit;

namespace Test.Zartis.UnitTest
{
    public class LandRocketShould
    {
        private readonly LandingPlatform _landingPlatform;
        private readonly LandingArea _landingArea;
        public LandRocketShould()
        {
            _landingPlatform = LandingPlatform.Create(10, 10, new Point(5, 5)).Value;
            _landingArea = LandingArea.Create(100, 100, _landingPlatform).Value;
        }

        [InlineData(10,10)]
        [InlineData(5, 5)]
        [InlineData(15, 15)]
        [InlineData(15, 7)]
        [Theory]
        public void Land_A_Rocket_In_Valid_Position(int x, int y)
        {
            var rocket = new Rocket(new Point(x,y));
            var landResult= _landingArea.TryToLandRocket(rocket);
            landResult.IsSuccess.Should().BeTrue();
            landResult.Value.Contains("ok for landing").Should().BeTrue();
        }

        [InlineData(0, 0)]
        [InlineData(15, 16)]
        [InlineData(20, 20)]
        [InlineData(100, 7)]
        [Theory]
        public void Return_Error_Message_Because_Of_Out_Of_Platform(int x, int y)
        {
            var rocket = new Rocket(new Point(x, y));
            var landResult = _landingArea.TryToLandRocket(rocket);
            landResult.IsSuccess.Should().BeFalse();
            landResult.Message.Contains("Out of platform").Should().BeTrue();
        }

        [Fact]
        public void Return_Error_Message_Because_Of_Other_Rocket_In_Same_Position()
        {
            var rocket = new Rocket(new Point(5, 5));
            var landResult = _landingArea.TryToLandRocket(rocket);
            landResult.IsSuccess.Should().BeTrue();

            var rocket2 = new Rocket(new Point(5, 5));
            landResult = _landingArea.TryToLandRocket(rocket2);
            landResult.IsSuccess.Should().BeFalse();
            landResult.Message.Contains("clash").Should().BeTrue();
        }

        [Fact]
        public void Return_Error_Message_Because_Of_Other_Rocket_Too_Close()
        {
            var rocket = new Rocket(new Point(5, 5));
            var landResult = _landingArea.TryToLandRocket(rocket);
            landResult.IsSuccess.Should().BeTrue();

            var rocket2 = new Rocket(new Point(6, 6));
            landResult = _landingArea.TryToLandRocket(rocket2);
            landResult.IsSuccess.Should().BeFalse();
            landResult.Message.Contains("clash").Should().BeTrue();
        }
    }
}
