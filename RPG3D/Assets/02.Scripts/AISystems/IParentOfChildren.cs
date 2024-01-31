using System.Collections.Generic;

namespace RPG.AISystems
{
    public interface IParentOfChildren
    {
        List<Node> children { get; set; }
    }
}
