namespace Test.Zartis
{
    public abstract class LandingValidator
    {
        public abstract Result<string> ValidateLanding(LandingArea landingArea, Rocket rocket);
    }
}