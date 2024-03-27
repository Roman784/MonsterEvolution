using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Data/ShopItem")]
public class ShopItemInfo : ScriptableObject
{
    public string Title;
    public string Description;
    public int Price;
    public Sprite Icon;
}
