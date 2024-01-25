namespace ExtensionMethods
{
    // 확장 메서드 (Extension methods)
    // static 클래스에서, static 함수를 만들고 해당 함수에 참조할 객체의 this 참조를 파라미터로 추가한다.
    internal static class GreetingExtension
    {
        public static void SayGoodAfternoon(this Robot robot)
        {
            Console.WriteLine($"[{robot.Name}] : Good afternoon sir.");
        }

        public static void SayGoodEvening(this Robot robot)
        {
            Console.WriteLine($"[{robot.Name}] : Good evening sir.");
        }
    }
}
