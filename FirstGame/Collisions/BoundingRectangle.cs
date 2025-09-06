using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace FirstGame.Collisions
{
    /// <summary>
    /// A struct representing a bounding rectangle for collision detection.
    /// </summary>
    public struct BoundingRectangle
    {
        /// <summary>
        /// Width of the bounding circle
        /// </summary>
        public float Width;
        /// <summary>
        /// Height of the bounding circle
        /// </summary>
        public float Height;
        /// <summary>
        /// X coordinate of the bounding Rectangle
        /// </summary>
        public float X;
        /// <summary>
        /// Y coordinate of the bounding Rectangle
        /// </summary>
        public float Y;
        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        /// <summary>
        /// Builds a new bounding rectangle from x, y, width and height
        /// </summary>
        /// <param name="x">The x coordinates of the rectangle</param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Builds a new bounding rectangle from a position vector, width and height
        /// </summary>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Tests for collision between this and another Bounding Rectangle.
        /// </summary>
        /// <param name="other">The other bounding rectangle</param>
        /// <returns>True for collision</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Tests for collision between this and a Bounding Circle.
        /// </summary>
        /// <param name="other">The bounding Circle</param>
        /// <returns>True for collision</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(other, this);
        }

        /// <summary>
        /// Detects a collision between a rectangle and a point
        /// </summary>
        /// <param name="r">The rectangle</param>
        /// <param name="p">The point</param>
        /// <returns>true on collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r, BoundingPoint p)
        {
            return p.X >= r.X && p.X <= r.X + r.Width && p.Y >= r.Y && p.Y <= r.Y + r.Height;
        }
    }
}
