using System;
using System.Collections.Generic;
using System.Text;

namespace Excercises
{
    public static class PointsArrayExtension
    {
        /// <summary>
        /// Returns an array of X components in a Points array
        /// </summary>
        /// <param name="pPoints"></param>
        /// <returns></returns>
        public static int[] GetXArray(this Point[] pPoints)
        {
            var Xs = new int[pPoints.Length];
            for (int i = 0; i < pPoints.Length; i++)
            {
                Xs[i] = pPoints[i].X;
            }
            return Xs;
        }

        /// <summary>
        /// Returns an array of Y components in a Points array
        /// </summary>
        /// <param name="pPoints"></param>
        /// <returns></returns>
        public static int[] GetYArray(this Point[] pPoints)
        {
            var Ys = new int[pPoints.Length];
            for (int i = 0; i < pPoints.Length; i++)
            {
                Ys[i] = pPoints[i].Y;
            }
            return Ys;
        }
    }
}
