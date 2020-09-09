using System;

namespace HelloWorld
{
    class Program
    {
        //Create an area for the player to manually Upgrade their stats. 
        //This could be in the form of a shop or some other context.
        //Create a function that overloads the function UpgradeStats. This should allow
        //the player to manually upgrade individual stats.
        static void Main(string[] args)
        {
            //Create a new instance of a Game
            Game game = new Game();
            //Run the Game
            game.Run();
            //Wait before closing
            Console.ReadKey();
        }
    }
}
