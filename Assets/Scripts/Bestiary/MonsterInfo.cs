using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Data/MonsterInfo")]
public class MonsterInfo : ScriptableObject
{
    public LocalizedText Name;
    public LocalizedText Description;
    public Sprite Sprite;
    public int CPS;
}