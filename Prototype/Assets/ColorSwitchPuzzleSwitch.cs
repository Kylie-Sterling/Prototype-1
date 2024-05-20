using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitchPuzzleSwitch : MonoBehaviour
{
    public int ID;
    public int currentIndex;
    public MeshRenderer m;

    public ColorSwitchPuzzle puzzle;
    private void Start()
    {
        m = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Audio a = FindAnyObjectByType<Audio>();
            a.PlaySound(0);

            currentIndex = currentIndex + 1;

            if (currentIndex >= puzzle.colors.Length)
            {
                currentIndex = 0;
            }
            m.material.color = puzzle.colors[currentIndex];

            puzzle.CheckWin();
        }
    }
}
