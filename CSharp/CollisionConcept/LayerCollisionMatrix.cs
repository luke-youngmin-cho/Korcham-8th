using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionConcept
{
    internal static class LayerCollisionMatrix
    {
        //   0 1 2 3 4 5 ...     //    0 1 2 3 4 5 ... 31
        // 0 X X O               // 31
        // 1 X                   // 30
        // 2 O                   // 29   
        // 3                     // 28
        // 4                     // ...
        // 5                     // 2  O
        // .                     // 1
        // .                     // 0
        // .
        public static bool[,] array = new bool[32, 32];

        public static void Toggle(int x, int y)
        {
            array[y, x] = !array[y, x];
            array[x, y] = array[y, x];
        }
    }
}
