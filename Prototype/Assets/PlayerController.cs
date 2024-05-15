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
    //[SerializeField] float jumpForce;
    [SerializeField] float groundDistance;
    Vector3 normal;
    Vector3 point;
    [SerializeField] bool ground;

    public GameObject lockOnTarget;
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        //movementInput = Vector2.ClampMagnitude(movementInput, 1f);
        movementInput.x = Mathf.Clamp(movementInput.x, -1, 1);
        movementInput.y = Mathf.Clamp(movementInput.y, -1, 1);
    }
    public void OnLockOn(InputAction.CallbackContext context)
    {
        LockOn();
    }
    private void Update()
    {
        if (lockOnTarget != null)
        {

            float totalDistance = Vector3.Distance(transform.position, lockOnTarget.transform.position);
            if (totalDistance > 30)
            {
                MeshRenderer renderer = lockOnTarget.GetComponent<MeshRenderer>();

                renderer.enabled = false; // Disable the Mesh Renderer for other objects


                lockOnTarget = null;
            }
        }
    }
    public void LockOn()
    {
        if (lockOnTarget != null)
        {
            MeshRenderer renderer = lockOnTarget.GetComponent<MeshRenderer>();

            renderer.enabled = false; // Disable the Mesh Renderer for other objects



            lockOnTarget = null;
            return;
        }
        LockOnCore[] targets = FindObjectsOfType<LockOnCore>();
        GameObject[] targetGameObjects = new GameObject[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targetGameObjects[i] = targets[i].gameObject;
        }
        GameObject closestObject = null;
        float shortestDistance = float.MaxValue;

        foreach (GameObject target in targetGameObjects)
        {
            float totalDistance = Vector3.Distance(transform.position, target.transform.position);

            if (totalDistance < shortestDistance)
            {
                shortestDistance = totalDistance;
                closestObject = target; // Assign the closest object found
                lockOnTarget = target;

                Debug.Log(totalDistance);
            }
        }
        foreach (GameObject obj in targetGameObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                LockOnCore core = obj.GetComponent<LockOnCore>();
                    
                // Renderer is not null, you can safely access its properties or methods
                if (obj == closestObject)
                {
                    renderer.enabled = true;
                    core.canvasObject.SetActive(true);
                }
                else
                {
                    renderer.enabled = false; // Disable the Mesh Renderer for other objects
                    core.canvasObject.SetActive(false);

                }
            }
        }
    }
    private void Move()
    {
        if (isMobile)
        {
            Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
            move = Quaternion.LookRotation(cam.forward) * move;

            // Zero out the y-component of the movement vector
            move.y = 0;



            rb.velocity = Vector3.ProjectOnPlane(((cam.transform.right * movementInput.x * speed) + ((cam.transform.forward * movementInput.y * speed))), normal);

            if (move != Vector3.zero)
            {
                Vector3 newForward = new Vector3(move.x, 0, move.z).normalized;


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
    void FixedUpdate()
    {
        Move();

        Ground();
        if (!ground)
        {
            Gravity();
        }


    }
    void Gravity()
    {
        rb.velocity -= Vector3.up * gravityValue * Time.deltaTime;
        Debug.Log(rb.velocity);
    }
    void Ground()
    {
        ground = Physics.Raycast(rb.worldCenterOfMass, -rb.transform.up, out RaycastHit hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore);

        point = ground ? hit.point : rb.transform.position;

        normal = ground ? hit.normal : Vector3.up;
    }
}