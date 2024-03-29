﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // 인터페이스는 외부와 상호작용하기위한 기능들을 제공해야하므로 
    // 멤버들의 접근제한자가 기본적으로 public 임
    //
    // C# 에서 외부와의 상호작용을 하기위한 기능들은 
    // 함수, 프로퍼티, 인덱서, 이벤트 가 있고. 이 네가지는 인터페이스 타입의 멤버로서 포함될 수 있다.
    internal interface IHp
    {
        float HpValue { get; set; }
        float HpMax { get; }
        event Action<float> OnHpChanged;

        void DepleteHp(float amout);
        void RecoverHp(float amout);
    }
}
