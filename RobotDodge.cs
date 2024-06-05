using System;
using Player;
using SplashKitSDK;
using System.Collections.Generic;

public class Robotdodge
{
    private PlayerA _Player;
    private Window _GameWindow;
    private List<Robot> _Robots;
    private Bitmap _HeartBitmap = new Bitmap("Heart", "Heart.png"); // Load heart image
    public SplashKitSDK.Timer myTimer;
    public bool Quit 
    {
        get
        {
            return _Player.Quit;
        }
    }

    public Robotdodge(Window gameWindow)
    {
        _GameWindow = gameWindow;
        _Player = new PlayerA(gameWindow);
        _Robots = new List<Robot>(); 
        myTimer = new SplashKitSDK.Timer("My Timer");
        myTimer.Start();
    }

    public void HandleInput()
    {
        _Player.HandleInput();
        _Player.StayOnWindow(_GameWindow);
    }

    public void Update()
    {
        foreach (var robot in _Robots)
        {
            robot.Update();
        }
        if (SplashKit.Rnd() < 0.01)
        {
            AddRandomRobot();
        }
        CheckCollision();
        _Player.Score = Convert.ToInt32(myTimer.Ticks / 1000);
        foreach (var bullet in _Player.Bullets)
        {
            bullet.Update();
        }

        CheckBulletCollision();
    }

    public void Draw()
    {
        _GameWindow.Clear(Color.White);
        foreach (var robot in _Robots)
        {
            robot.Draw();
        }
        _Player.Draw();
        DrawHearts(_Player.Lives);
        SplashKit.DrawText("Score: " + _Player.Score, Color.Black, 700, 20);
        foreach (var bullet in _Player.Bullets)
        {
            bullet.Draw();
        }
        _GameWindow.Refresh(60);
    }

    private void AddRandomRobot()
    {
        _Robots.Add(RandomRobot(_GameWindow, _Player));
    }

    public Robot RandomRobot(Window gameWindow, PlayerA player)
    {
        double randomNumber = SplashKit.Rnd(900);

        if (randomNumber < 300)
        {
            return new Boxy(gameWindow, player);
        }
         else if (randomNumber > 300 & randomNumber < 600)
        {
            return new Roundy(gameWindow, player);
        }
        else
        {
            return new Custom(gameWindow, player);
        }
    }

    private void CheckCollision()
    {
        List<Robot> robotsToRemove = new List<Robot>();
        foreach (Robot robot in _Robots)
        {
            if (_Player.CollideWith(robot))
            {
                _Player.Lives--;
            }
            if (_Player.CollideWith(robot) || robot.IsOffscreen(_GameWindow))
            {
                robotsToRemove.Add(robot);
            }
        }
        foreach (var robotToRemove in robotsToRemove)
        {
            _Robots.Remove(robotToRemove);
        }
    }

    public void DrawHearts(int numberOfHearts)
    {
        int heartX = 0;
        for (int i = 0; i < numberOfHearts; i ++ )
        {
            if (heartX < 300)
            {
                SplashKit.DrawBitmap(_HeartBitmap, heartX, 0);
                heartX = heartX + 50;
            }
        }
    }
    
    private void CheckBulletCollision()
    {
        List<Robot> robotsToRemove = new List<Robot>();
        List<Bullet> bulletsToRemove = new List<Bullet>();
        foreach (var bullet in _Player.Bullets)
        {
            foreach (var robot in _Robots)
            {
                if (bullet.CollideWith(robot))
                {
                    robotsToRemove.Add(robot);
                    bulletsToRemove.Add(bullet);
                }
            }
        }
        foreach (var bulletToRemove in bulletsToRemove)
        {
            _Player.Bullets.Remove(bulletToRemove);
        }
        foreach (var robotToRemove in robotsToRemove)
        {
            _Robots.Remove(robotToRemove);
        }
    }
}