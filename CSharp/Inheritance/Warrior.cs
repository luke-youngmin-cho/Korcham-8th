namespace Inheritance
{
    internal class Warrior : Character
    {
        public float anger;

        public void Smash()
        {
            Console.WriteLine($"{NickName} (이)가 휘둘렀다 .. !");
        }

        // override : 재정의 키워드. 
        // 상위타입에서 정의된 추상/가상 기능을 재정의 하는 키워드
        public override void Smile()
        {
            Console.WriteLine($"{NickName} (이)가 한쪽 입고리를 올리며 웃는다 ...");
        }

        public override void Attack(IHp target)
        {
            base.Attack(target); // base 키워드 : 기반 클래스 멤버에 접근하는 키워드.
            Console.WriteLine($"[{NickName}] : 하이얍 !");
        }
    }
}
