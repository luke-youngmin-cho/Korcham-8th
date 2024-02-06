using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG
{
    public static class MathExtensions
    {
        public static float ClampAsNormalizedAngle(this float value, float min, float max)
        {
            float angle = value;
            angle = (angle + 360f) % 360f;
            min = (min + 360f) % 360f;
            max = (max + 360f) % 360f;

            if (min < max)
            {
                return Mathf.Clamp(angle, min, max);
            }
            else if (angle > min || angle < max)
            {
                return angle;
            }
            else
            {
                float diffMin = Mathf.Min(Mathf.Abs(angle - min), Mathf.Abs(angle - 360f - min));
                float diffMax = Mathf.Min(Mathf.Abs(angle - max), Mathf.Abs(angle + 360f - max));

                return diffMin < diffMax ? min : max;
            }
        }
    }
}
