namespace CollisionConcept
{
    public enum Layer
    {
        Ground,  // 0 == ... 00000000
        SwordMan,// 1 == ... 00000001
        Ghost,   // 2 == ... 00000010
        Goblin   // 3 == ... 00000011
    }

    [Flags]
    public enum Layers
    {
        Ground   = 0 << 0, // ... 00000000  
        SwordMan = 1 << 0, // ... 00000001
        Ghost    = 1 << 1, // ... 00000010
        Goblin   = 1 << 2, // ... 00000100
        //SwordManOrGhost = SwordMan | Ghost, // ... 00000011
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Layer layer = Layer.Ground;

            if (layer == Layer.SwordMan) { }
            if (layer == Layer.Ghost) { }

            Layers mask = Layers.SwordMan | Layers.Ghost;
            Console.WriteLine(Layers.Ghost);
            Console.WriteLine(mask.ToString());

            Collider other = new Collider();
            other.Layer = 1; // <- 수정해주세여~
            // ... 00000010
            // ... 00000011
            // ---- & -----
            // ... 00000010
            if ((1 << other.Layer & (int)mask) > 0)
            {
                // 충돌함
            }


            //   0 1 2
            // 2 O X X
            // 1 X X 
            // 0 O  
            LayerCollisionMatrix.Toggle(0, 0);
            LayerCollisionMatrix.Toggle(2, 0);

            Collider collider1 = new Collider();
            collider1.Name = "땅";
            collider1.Layer = 0;

            Collider collider2 = new Collider();
            collider2.Name = "검사";
            collider2.Layer = 2;

            Collider collider3 = new Collider();
            collider3.Name = "유령";
            collider3.Layer = 1;

            // 검사와 땅이 충돌하는지확인하려면? 
            if (LayerCollisionMatrix.array[2, 0])
            {
                // 충돌
            }
            // 검사와 유령이 충돌하는지 확인하려면?
            if (LayerCollisionMatrix.array[2, 1])
            {

            }

            LayerMask layerMask = new LayerMask { Value = 1 << 2};
            if ((1 << collider2.Layer & layerMask.Value) > 0)
            {
                // 검사 검출
            }
        }
    }
}
