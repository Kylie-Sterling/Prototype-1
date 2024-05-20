using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPile : MonoBehaviour
{
    public int withTool;
    public int withOutTool;
    public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        WeaponSelecter w = FindAnyObjectByType<WeaponSelecter>();
        if (weapon != null && w.hitboxOn)
        {
            Audio a = FindAnyObjectByType<Audio>();
            a.PlaySound(1);
            if (weapon.weaponType == Weapon.types.HAMMER)
            {
                ScrapCounter.totalscrap += withTool;
                Charge c = FindAnyObjectByType<Charge>();
                c.charge--;
            }
            else
            {
                ScrapCounter.totalscrap += withOutTool;
            }
            Destroy(parent);
        }
    }
}
