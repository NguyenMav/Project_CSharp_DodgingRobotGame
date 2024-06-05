using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Player
{
    public class PlayerA
    {
        private Bitmap _PlayerBitmap;
        public double X {get;private set;}
        public double Y {get;private set;}
        public bool Quit {get;private set;}
        public int Lives {get;set;} = 5;
        public int Score {get;set;}
        public List<Bullet> Bullets { get; private set; }
        public int width
        {
            get
            {
                return _PlayerBitmap.Width;
            }
        }  
        public int height
        {
            get
            {
                return _PlayerBitmap.Height;
            }
        }

        public PlayerA(Window gameWindow)
        {
            Quit = false;
            _PlayerBitmap = new Bitmap("Player", "Player.png");
            X = (gameWindow.Width - width)/2;
            Y = (gameWindow.Height - height)/2;
            Bullets = new List<Bullet>();
        }

        public void Draw()
        {
            _PlayerBitmap.Draw(X,Y);
        }

        public void HandleInput()
        {
            const int speed = 5;

            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Y = Y - speed;
            }

            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Y = Y + speed;
            }

            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                X = X - speed;
            }

            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                X = X + speed;
            }

            if (SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                Quit = true;
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                ShootBullet(SplashKit.MouseX(), SplashKit.MouseY());
            }

            if (Lives <= 0)
            {
                Quit = true;
            }
        }

        public void StayOnWindow(Window gameWindow)
        {
            const int GAP = 10;

            if (X < GAP)
            {
                X = GAP;
            }

            else if (X > gameWindow.Width - GAP - width)
            {
                X = gameWindow.Width - GAP - width;
            }

            else if (Y < GAP)
            {
                Y = GAP;
            }

            else if (Y > gameWindow.Height - GAP - height)
            {
                Y = gameWindow.Height - GAP - height;
            }
        }
        
        public bool CollideWith(Robot other)
        {
            return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
        }

        private void ShootBullet(double targetX, double targetY)
        {
            Bullets.Add(new Bullet(X, Y, targetX, targetY));
        }
    }
}