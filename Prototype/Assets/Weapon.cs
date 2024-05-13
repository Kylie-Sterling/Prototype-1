using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum types
    {
        SWORD,
        HAMMER,
    };

    public types weaponType;

    public bool twoHands;
    public Weapon offHand;

    public float attackDamage;
    /*public float attackKnockbackMultiplier = 1;

    public float heavyAttackDamage;
    public float heavyAttackKnockbackMultiplier = 1;

    public float weaponArtDamage;
    public float weaponArtKnockbackMultiplier = 1;*/
}
