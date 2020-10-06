using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Good
{
    public static class Transformation
    {
        public static Vector2 SetX(float x, Vector2 v)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 SetY(float y, Vector2 v)
        {
            return new Vector2(v.x, y);
        }
    }

    public static class Math
    { 
    }
}

