using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;
public class PlayerController : MonoBehaviour
{

    //public PlayerControlsScript controls;
    //public float movementInputZ;
    //public float movementInputX;
    public Vector2 movementInput;

    public float speed = 5;
    
    public bool isMobile;
    public Transform cam;
    public Rigidbody rb;
    //public WeaponSelecter weapon;
    //public PlayerHealth playerHealth;
    //public Animator animator;
    //public Animator playerAnimator;

    public Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float gravityValue = -9.81f;


    public LayerMask layerMask;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDistance;
    Vector3 normal;
    Vector3 point;
    [SerializeField] bool ground;


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        //movementInput = Vector2.ClampMagnitude(movementInput, 1f);
        movementInput.x = Mathf.Clamp(movementInput.x, -1, 1);
        movementInput.y = Mathf.Clamp(movementInput.y, -1, 1);
    }
    private void Update()
    {
        Ground();
        if (!ground)
        {
            Gravity();
        }

        if (isMobile)
        {
            Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
            move = Quaternion.LookRotation(cam.forward) * move;

            // Zero out the y-component of the movement vector
            move.y = 0;



            rb.velocity = Vector3.ProjectOnPlane(((cam.transform.right * movementInput.x * speed) + ((cam.transform.forward * movementInput.y * speed))), normal);

            if (move != Vector3.zero)
            {
                Vector3 newForward = new Vector3(move.x, transform.forward.y, move.z).normalized;
                gameObject.transform.forward = newForward;
                //animator.SetTrigger("walk");
                /*string currentAnimation = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                if (currentAnimation != "roll")
                {
                    playerAnimator.SetBool("run", true);
                }*/
            }
            else
            {
                //playerAnimator.SetBool("run", false);
                playerVelocity = Vector3.zero;
            }
        }

        //playerAnimator.SetBool("roll", isDashing);
    }
    void Gravity()
    {
        rb.velocity -= Vector3.up * gravityValue * Time.deltaTime;
    }
    void Ground()
    {
        ground = Physics.Raycast(rb.worldCenterOfMass, -rb.transform.up, out RaycastHit hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore);

        point = ground ? hit.point : rb.transform.position;

        normal = ground ? hit.normal : Vector3.up;
    }
}