using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Weapon;

public class WeaponSelecter : MonoBehaviour
{
    public Weapon[] weapons;
    public int selectedWeaponIndex = 0;
    public bool hitboxOn = false;
    public string attackType;

    public float strength;
    public float dex;

    public int consectutiveHits = 0;
    public int consectutiveArtHits = 0;
    public PlayerController playerController;
    public Animator animator;
    public Charge playerHealth;

    void Start()
    {
        SelectWeapon();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Charge>();
    }
    private void Update()
    {

        string currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (currentAnimation == "idle" && consectutiveHits > 0)
        {
            consectutiveHits = 0;
        }
    }
    private void SelectWeapon()
    {
        int index = selectedWeaponIndex;
        if (index >= 0 && index < weapons.Length)
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
                if (weapon.offHand != null)
                {
                    weapon.offHand.gameObject.SetActive(false);
                }                
            }
            weapons[index].gameObject.SetActive(true);
            if (weapons[index].offHand != null)
            {
                weapons[index].offHand.gameObject.SetActive(true);
            }
        }
    }
    public void CycleWeapon(InputAction.CallbackContext context)
    {
        string currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (currentAnimation == "idle" || currentAnimation == "run")
        {
            selectedWeaponIndex = selectedWeaponIndex + 1;

            if (selectedWeaponIndex >= weapons.Length)
            {
                selectedWeaponIndex = 0;
            }
            SelectWeapon();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        animator.SetInteger("Hits", consectutiveHits);

        Weapon currentWeapon = weapons[selectedWeaponIndex];

        string currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (currentAnimation == "idle" || consectutiveHits > 0 || currentAnimation == "run")
        {
            attackType = "attack";
            consectutiveHits++;

            switch (currentWeapon.weaponType)
            {
                case types.SWORD:
                    if (consectutiveHits == 1)
                    {
                        Debug.Log("Attacking with broad type weapon 0");
                        animator.SetTrigger("sword_attack");
                    }
                    if (consectutiveHits == 2)
                    {
                        Debug.Log("Attacking with broad type weapon 1");
                        //animator.SetTrigger("broadAttack1");
                        animator.SetInteger("Hits", consectutiveHits);
                    }

                    break;
                case types.HAMMER:
                    if (consectutiveHits == 1)
                    {
                        animator.SetTrigger("hammer_attack");
                    }
                    if (consectutiveHits >= 2)
                    {
                        animator.SetInteger("Hits", consectutiveHits);
                    }
                    break;
                default:
                    Debug.LogWarning("Unknown weapon type");
                    break;
            }
        }
    }
}