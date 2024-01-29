using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerCharacterList", menuName = "ScriptableObjects/PlayerCharacterList", order = 1)]
public class PlayerCharacterList : ScriptableObject
{
    public List<GameObject> list;
}
