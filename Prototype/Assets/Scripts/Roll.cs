using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Roll : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public bool isRolling;
    public bool canRoll;
    public float rollForce = 20;
    public float coolDown;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (canRoll)
        {
            isRolling = true;
            canRoll = false;
        }
    }
    private void FixedUpdate()
    {
        if(isRolling)
        {
            isRolling = false;
            Charge c = GetComponent<Charge>();
            c.invincible = true;
            Vector3 force = rollForce * gameObject.transform.forward;
            rb.AddForce(force, ForceMode.Impulse);
            anim.SetTrigger("roll");
            StartCoroutine(RollCoolDown());
        }
    }
    IEnumerator RollCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        canRoll = true;
        Charge c = GetComponent<Charge>();
        c.invincible = false;
    }
}
