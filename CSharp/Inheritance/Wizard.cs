namespace Inheritance
{
    internal class Wizard : Character
    {
        public float magicShield;
        public float energy;

        public void EnergyBolt()
        {
            Console.WriteLine($"{NickName} (이)가 에너지볼트를 날렸다 .. !");
        }

        // override : 재정의 키워드. 
        // 상위타입에서 정의된 추상/가상 기능을 재정의 하는 키워드
        public override void Smile()
        {
            Console.WriteLine($"{NickName} (이)가 양쪽 입고리를 올리며 웃는다 ...");
        }
    }
}
