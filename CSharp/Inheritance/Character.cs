
namespace Inheritance
{
    // abstract 키워드 : 추상 키워드
    // 클래스 / 함수 앞에 붙어서 해당 정의가 추상용도이므로 인스턴스화 불가능하다는것을 명시
    internal abstract class Character : IHp
    {
        public string NickName;
        public int Lv;
        public float Exp;

        public float HpValue 
        {
            get => _hpValue;
            set
            {
                value = Math.Clamp(value, 0, _hpMax);
                if (value == _hpValue)
                    return;

                _hpValue = value;
                OnHpChanged?.Invoke(value);
            }
        }

        public float HpMax => _hpMax;

        private float _hpValue;
        private float _hpMax;

        public event Action<float> OnHpChanged;

        // virtual 가상 키워드
        // 이 클래스를 상속받은 자식이 이 기능의 구현부를 재정의 할 수 있도록 한다.
        public virtual void Attack(IHp target)
        {
            Console.WriteLine($"{NickName} (이)가 공격했다 .. !");
            target.DepleteHp(100);
        }

        public void DepleteHp(float amout)
        {
            throw new NotImplementedException();
        }

        public void RecoverHp(float amout)
        {
            throw new NotImplementedException();
        }

        // 추상 함수는 이 클래스를 상속받은 자식이 이 기능의 구현부를 재정의 해줘야한다.
        public abstract void Smile();
    }
}
