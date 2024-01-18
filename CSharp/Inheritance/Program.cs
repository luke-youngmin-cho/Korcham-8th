namespace Inheritance
{
    // C# 은 클래스 다중상속을 지원하지않는다.
    internal class Program
    {
        static void Main(string[] args)
        {
            //Character character1 = new Character();
            Warrior warrior1 = new Warrior();
            warrior1.NickName = "전사1";

            Wizard wizard1 = new Wizard();
            wizard1.NickName = "법사1";

            warrior1.Attack(wizard1);
            wizard1.Attack(warrior1);

            warrior1.Smash();
            wizard1.EnergyBolt();

            Character character1 = new Warrior();
            character1.NickName = "캐릭터1";
            character1.Attack(wizard1);

            Character[] characters = new Character[2];
            characters[0] = warrior1;
            characters[1] = wizard1;
            IHp hp = warrior1; // 인터페이스도 사용자정의 자료형이므로 기반타입참조 ok

            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].Attack(wizard1);
            }
        }
    }
}
