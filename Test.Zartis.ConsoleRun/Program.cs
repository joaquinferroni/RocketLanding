using System;
using System.Collections.Generic;

namespace Test.Zartis.ConsoleRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int widthLandingArea = 100;
            int heightLandingArea = 100;
            int widthLandingPlatform = 10;
            int heightLandingPlatform = 10;
            int startXPoint = 5;
            int startYPoint = 5;

            var landingPlatformResult = LandingPlatform.Create(widthLandingPlatform, heightLandingPlatform, new Point(startXPoint,startYPoint));
            if(!landingPlatformResult.IsSuccess) {
                Console.WriteLine(landingPlatformResult.Message);
                return;
            }

            var landingAreaResult = LandingArea.Create(widthLandingArea,heightLandingArea,landingPlatformResult.Value);

            if(!landingAreaResult.IsSuccess) {
                Console.WriteLine(landingAreaResult.Message);
                return;
            }

            var landingArea= landingAreaResult.Value;
            var rocketsToLAnd = RocketsToLand();
            foreach(var r in rocketsToLAnd){
                var landingResult = landingArea.TryToLandRocket(r);
                if(!landingResult.IsSuccess) {
                    Console.WriteLine(landingResult.Message);
                    continue;
                }
                Console.WriteLine(landingResult.Value);
            }
        }

        private static IList<Rocket> RocketsToLand()
        => new List<Rocket>(){
            new Rocket(new Point(5,5)), // valid rocket 
            new Rocket(new Point(5,5)), // not valid rocket, wont be stored -> clash
            new Rocket(new Point(6,5)), // not valid rocket, wont be stored-> clash
            new Rocket(new Point(7,5)), // valid rocket, this position is now the last_landed rocket to be compared with news
            new Rocket(new Point(7,6)), // not valid rocket -> clash
            new Rocket(new Point(5,7)), // not valid rocket-> clash
            new Rocket(new Point(35,5)), // valid rocket -> out of platform
        };
    }
}
