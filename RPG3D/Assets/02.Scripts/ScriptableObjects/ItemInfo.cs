using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/ItemInfo", order = 1)]
public class ItemInfo : ScriptableObject
{
    public int id;
    public string description;
    public int levelRequired;
    public int numMax;
    public Sprite icon;
    public GameObject model;
}
