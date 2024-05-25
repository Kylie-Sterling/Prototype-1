using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum element
    {
        none,
        Fire,
        Water,
        Ground,
        Electric,
    };
    public enum types
    {
        SWORD,
        HAMMER,
    };

    public element FusedElement;
    public types weaponType;

    public bool twoHands;
    public Weapon offHand;

    public float damage;

    public float attackDamage;
    public float heavyAttackDamage;


    /*public float attackKnockbackMultiplier = 1;

    public float heavyAttackKnockbackMultiplier = 1;

    public float weaponArtDamage;
    public float weaponArtKnockbackMultiplier = 1;*/
}
