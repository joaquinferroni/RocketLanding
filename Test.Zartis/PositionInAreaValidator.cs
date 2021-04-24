namespace Test.Zartis
{
    public class PositionInAreaValidator : LandingValidator
    {
        private readonly LandingValidator _nextValidator;
        public PositionInAreaValidator(LandingValidator nextValidator)
        {
            this._nextValidator = nextValidator;
        }

        public override Result<string> ValidateLanding(LandingArea landingArea, Rocket rocket)
        {
            if (landingArea.LandingPlatform.StartPoint.X > rocket.Position.X
            || landingArea.LandingPlatform.EndXPosition < rocket.Position.X
            ) return Result<string>.CreateFailure("Out of platform");

            if (landingArea.LandingPlatform.StartPoint.Y > rocket.Position.Y
            || landingArea.LandingPlatform.EndYPosition < rocket.Position.Y
            ) return Result<string>.CreateFailure("Out of platform");

            return _nextValidator.ValidateLanding(landingArea, rocket);
        }
    }
}