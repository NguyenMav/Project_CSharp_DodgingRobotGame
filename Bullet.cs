using System;
using SplashKitSDK;

namespace Player
{
    public class Bullet
    {
        private double _X;
        private double _Y;
        private Vector2D _Velocity;
        private const int _Speed = 3;
        private const int _Radius = 10;
        private Color _Color = Color.Red;
        public bool IsActive { get; private set; }

        public Bullet(double startX, double startY, double targetX, double targetY)
        {
            _X = startX;
            _Y = startY;

            Point2D fromPt = new Point2D() { X = startX, Y = startY };
            Point2D toPt = new Point2D() { X = targetX, Y = targetY };

            Vector2D dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
            _Velocity = SplashKit.VectorMultiply(dir, _Speed);

            IsActive = true;
        }

        public void Update()
        {
            _X += _Velocity.X;
            _Y += _Velocity.Y;
        }

        public void Draw()
        {
            SplashKit.FillCircle(_Color, _X, _Y, _Radius);
        }

        public bool IsOffscreen(Window screen)
        {
            return _X < 0 || _X > screen.Width || _Y < 0 || _Y > screen.Height;
        }

        public bool CollideWith(Robot robot)
        {
            return SplashKit.CirclesIntersect(_X, _Y, _Radius, robot.X + robot.Width / 2, robot.Y + robot.Height / 2, robot.Radius);
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}

