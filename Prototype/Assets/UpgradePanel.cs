using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public GameObject panel;
    public void ChangePanelState()
    {
        if(panel.activeSelf)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ApplyFire(Weapon w)
    {
        w.FusedElement = Weapon.element.Fire;
    }
    public void ApplyEarth(Weapon w)
    {
        w.FusedElement = Weapon.element.Water;
    }
}
