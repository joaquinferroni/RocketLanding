using System;

namespace Test.Zartis
{
    public class LandingPlatform
    {
        public int Width { get;}
        public int Height { get;}
        public Point StartPoint{get;}
        public int EndXPosition{
            get=>  Width+StartPoint.X;
        }

        public int EndYPosition{
            get=>  Height+StartPoint.Y;
        }

        public Rocket LastLanded{get;private set;}

        private LandingPlatform(int width, int height, Point startPoint)
        {
            this.Width = width;
            this.Height = height;
            this.StartPoint = startPoint;
        }

        public static Result<LandingPlatform> Create(int width, int height, Point startPoint){
            if(!IsValid(width, height)) return Result<LandingPlatform>.CreateFailure("Not valid width or height");
            return Result<LandingPlatform>.CreateSuccess(new LandingPlatform(width,height,startPoint));
        }

        private static bool IsValid(int width, int height)
            => width > 1 || height >1;

        public void Land(Rocket rocket){
            this.LastLanded = rocket;
        }
    }
}