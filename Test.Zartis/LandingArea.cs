using System;

namespace Test.Zartis
{
    public class LandingArea
    {
        public int Width { get;}
        public int Height { get;}
        public LandingPlatform LandingPlatform { get;}
        private LandingValidator _landingValidator;
        private LandingArea(int width, int height, LandingPlatform landingPlatform)
        {
            this.Width = width;
            this.Height = height;
            this.LandingPlatform = landingPlatform;
            _landingValidator = new PositionInAreaValidator(new DistanceToRocketValidator (new LastStepValidator(null)));
        }

        public static Result<LandingArea> Create(int width, int height, LandingPlatform landingPlatform)
        {
            var validModel = IsValid(width,height,landingPlatform);
            if(!validModel.isValid) return Result<LandingArea>.CreateFailure(validModel.errorMessage);
            return Result<LandingArea>.CreateSuccess(new LandingArea(width,height,landingPlatform));
        }

        private static (bool isValid, string errorMessage) IsValid(int width, int height, LandingPlatform landingPlatform)
        {
            if(width <= 1) return (false, "width not valid, should be higher than 1");
            if(height <= 1) return (false, "height not valid, should be higher than 1");
            if((landingPlatform.StartPoint.X+ landingPlatform.Width) > width) return (false, "Landing Platrofrm could not be placed.");
            if((landingPlatform.StartPoint.Y+ landingPlatform.Height) > height  ) return (false, "Landing Platrofrm could not be placed.");
            return (true,string.Empty);
        }


        public Result<string> TryToLandRocket(Rocket rocket)
        {
            var result = _landingValidator.ValidateLanding(this, rocket);
            if(result.IsSuccess) this.LandingPlatform.Land(rocket);
            return result;
        } 

    }
}
