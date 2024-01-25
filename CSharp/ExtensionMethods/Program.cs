namespace ExtensionMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot("3PO");
            robot.SayGoodMorning();
            robot.SayGoodAfternoon();
            robot.SayGoodEvening();
            int a = robot[3];
        }
    }
}
