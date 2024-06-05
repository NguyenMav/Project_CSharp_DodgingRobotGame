using System;
using Player;
using SplashKitSDK;

namespace RobotDodge
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Robot Dodge Game", 800, 600);
            Robotdodge robotDodge = new Robotdodge(gameWindow);

            do
            {
                SplashKit.ProcessEvents();
                robotDodge.HandleInput();
                robotDodge.Draw();
                robotDodge.Update();
            } while (!robotDodge.Quit && !gameWindow.CloseRequested);
            gameWindow.Close();
        }
    }
}
