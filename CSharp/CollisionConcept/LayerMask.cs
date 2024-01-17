using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionConcept
{
    // 충돌체의 layer 값으로 1 을 bit-shift-left 한다음 LayerMask 의 Value 와 bit-& 연산을 했을때
    // 0보다 크면 충돌 가능, 0이면 무시
    internal struct LayerMask
    {
        public int Value;
        // 00000000 00000000 00000000 00000000
        // ex) 검사 또는 유령을 검출하고싶다 
        // Value = 1 << 2 |  1 << 1
        // 00000000 00000000 00000000 00000110


        // collider.Layer (검사의 collider 라면 값은 2)
        // (1 << collider.Layer & Value)
        // 00000000 00000000 00000000 00000110
        // 00000000 00000000 00000000 10000000
        //--------------------------------------- &
        // 00000000 00000000 00000000 00000000 > 0
    }
}
