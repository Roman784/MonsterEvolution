using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Data/ShopItem")]
public class ShopItemInfo : ScriptableObject
{
    public LocalizedText Title;
    public LocalizedText Description;
    public int Price;
    public Sprite Icon;
}
