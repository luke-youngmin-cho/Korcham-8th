namespace Inheritance2
{
    internal class Program
    {
        static List<object> s_unitsSelected = new List<object>();

        static void Main(string[] args)
        {
            Zealot zealot1 = new Zealot();
            Tank tank1 = new Tank();
            Tank tank2 = new Tank();
            Vulture vulture1 = new Vulture();

            while (true)
            {
                SelectUnit(zealot1);
                SelectUnit(tank1);
                SelectUnit(tank2);
                SelectUnit(vulture1);

                foreach (var unit in s_unitsSelected)
                {
                    if (unit is IAttacker)
                    {
                        ((IAttacker)unit).Attack();
                    }

                    //if (unit is Zealot)
                    //{
                    //    ((Zealot)unit).Attack();
                    //}
                    //else if (unit is Tank)
                    //{
                    //    ((Tank)unit).Attack();
                    //}
                    //else if (unit is Vulture)
                    //{
                    //    ((Vulture)unit).Attack();
                    //}
                }

                Thread.Sleep(100);
            }
        }

        static void SelectUnit(object unit)
        {
            s_unitsSelected.Add(unit);
        }

        static void SelectZealot(Zealot zealot)
        {

        }

        static void SelectVulture(Vulture vulture)
        {

        }
    }
}
