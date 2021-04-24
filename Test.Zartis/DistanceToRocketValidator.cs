using System;

namespace Test.Zartis
{
    public class DistanceToRocketValidator : LandingValidator
    {
        private const int MAX_DISTANCE_ALLOWED = 1;
        private readonly LandingValidator _nextValidator;
        public DistanceToRocketValidator(LandingValidator nextValidator)
        {
            this._nextValidator = nextValidator;
        }

        public override Result<string> ValidateLanding(LandingArea landingArea, Rocket rocket)
        {
            if(landingArea.LandingPlatform.LastLanded is null ) return _nextValidator.ValidateLanding(landingArea,rocket);
            var xDistance = Math.Abs(landingArea.LandingPlatform.LastLanded.Position.X - rocket.Position.X);
            var yDistance = Math.Abs(landingArea.LandingPlatform.LastLanded.Position.Y - rocket.Position.Y);
            if(Math.Max(xDistance,yDistance) <= MAX_DISTANCE_ALLOWED) return Result<string>.CreateFailure("clash");
            return _nextValidator.ValidateLanding(landingArea, rocket);
        }
    }
}