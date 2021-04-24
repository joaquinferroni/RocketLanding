namespace Test.Zartis
{
    public class LastStepValidator:LandingValidator{
        private readonly LandingValidator _nextValidator;
        public LastStepValidator(LandingValidator nextValidator)
        {
            this._nextValidator = nextValidator;
        }
        public override Result<string> ValidateLanding(LandingArea landingArea, Rocket rocket)
        {
            return Result<string>.CreateSuccess("ok for landing");
        }
    }
}