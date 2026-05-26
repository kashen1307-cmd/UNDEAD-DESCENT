using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public bool isCurrency;

    [Header("Stats")]

    public int itemCost;
    public int currentHealth;
    public int maxHealth;
    public float movementSpeed;
    public int damageBonus;
    public float reloadSpeed;
    public float coinMultiplierBonus;

    [Header("Temporary Buffs")]

    public float duration;
}
