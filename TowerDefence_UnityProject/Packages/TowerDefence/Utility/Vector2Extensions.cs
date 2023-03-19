using System;
using UnityEngine;

namespace TowerDefence.Utility
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Tries to fit a rawImage into a parent rectTransform
        /// </summary>
        /// <param name="original">image to resize</param>
        /// <param name="padding">any padding that needs to be applied</param>
        /// <param name="scaleAxis">the axis to scale on. None will well not do anything</param>
        /// <param name="maxConstraintSize"></param>
        /// <param name="sizeConstraints"></param>
        /// <returns></returns>
        public static Vector2 SizeToParent(this Vector2 original, Vector2 bounds, float padding = 0, Axis scaleAxis = Axis.Both, Vector2 maxConstraintSize = default, SizeConstraints sizeConstraints = SizeConstraints.None)
        {
            float w = 0, h = 0;

            // check if there is something to do
            padding = 1 - padding;

            var ratio = original.x / original.y;

            switch (scaleAxis)
            {
                case Axis.Both:
                    //Size by height first
                    h = bounds.y * padding;
                    w = h * ratio;
                    if (w > bounds.x * padding)
                    {
                        //If it doesn't fit, fallback to width;
                        w = bounds.x * padding;
                        h = w / ratio;
                    }

                    break;

                case Axis.Horizontal:
                    h = bounds.y * padding;
                    w = h * ratio;
                    break;

                case Axis.Vertical:
                    w = bounds.x * padding;
                    h = w / ratio;
                    break;
            }

            ConstrainSize();

            return new Vector2(w, h);

            void ConstrainSize()
            {
                switch (sizeConstraints)
                {
                    case SizeConstraints.Horizontal:
                        if (w > maxConstraintSize.x)
                        {
                            w = maxConstraintSize.x;
                            h = w / ratio;
                        }

                        break;

                    case SizeConstraints.Vertical:
                        if (h > maxConstraintSize.y)
                        {
                            h = maxConstraintSize.y;
                            w = h * ratio;
                        }

                        break;

                    case SizeConstraints.Both:
                        if (w > maxConstraintSize.x || h > maxConstraintSize.y)
                        {
                            if (w > maxConstraintSize.x)
                            {
                                w = maxConstraintSize.x;
                                h = w / ratio;
                            }

                            if (h > maxConstraintSize.y)
                            {
                                h = maxConstraintSize.y;
                                w = h * ratio;
                            }

                            ConstrainSize();
                        }

                        break;

                    case SizeConstraints.None:
                    default:
                        break;
                }
            }
        }

        [Serializable]
        public enum SizeConstraints
        {
            None = 0b00,
            Horizontal = 0b01,
            Vertical = 0b10,
            Both = 0b11
        }

        [Serializable]
        public enum Axis
        {
            None = 0b00,
            Horizontal = 0b01,
            Vertical = 0b10,
            Both = 0b11
        }
    }
}
