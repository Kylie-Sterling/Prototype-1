using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        WeaponSelecter w = FindAnyObjectByType<WeaponSelecter>();
        if (weapon != null && w.hitboxOn)
        {
            Audio a = FindAnyObjectByType<Audio>();
            a.PlaySound(1);
            if (weapon.weaponType == Weapon.types.SWORD)
            {
                Charge c = FindAnyObjectByType<Charge>();
                c.charge--;
                Destroy(parent);
            }
        }
    }
}
