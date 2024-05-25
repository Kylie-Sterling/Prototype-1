using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    public LockOnCore[] targets;
    public int currentTargetIndex;
    public float lockOnRange = 30f;

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
    public void OnLockOnSwitchDown(InputAction.CallbackContext context)
    {
        if (context.performed)
            CycleTargets(-1);
    }
    public void OnLockOnSwitchUp(InputAction.CallbackContext context)
    {
        if(context.performed)
            CycleTargets(1);
    }
    private void Update()
    {
        if (lockOnTarget != null)
        {

            float totalDistance = Vector3.Distance(transform.position, lockOnTarget.transform.position);
            if (totalDistance > lockOnRange)
            {
                MeshRenderer renderer = lockOnTarget.GetComponent<MeshRenderer>();

                renderer.enabled = false; // Disable the Mesh Renderer for other objects


                lockOnTarget = null;
            }
        }
    }
    bool isValid(GameObject obj)
    {
        float distance = Vector3.Distance(transform.position, obj.transform.position);
        if (distance <= lockOnRange)
        {
            return true;
        }
        return false;
    }
    void Cycle(int direction)
    {
        currentTargetIndex += direction;
        if (currentTargetIndex >= targets.Length)
        {
            currentTargetIndex = 0;
        }
        else if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Length;
        }
        if (!isValid(targets[currentTargetIndex].gameObject))
        {
            Cycle(direction);
        }
    }
    private void CycleTargets(int direction)
    {
        targets = FindObjectsOfType<LockOnCore>();

        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == lockOnTarget)
            {
                currentTargetIndex = i;
                break;
            }
        }
        Cycle(direction);

        DisableLockOn();

        lockOnTarget = targets[currentTargetIndex].gameObject;
        LockOnCore core = lockOnTarget.GetComponent<LockOnCore>();
        core.canvasObject.SetActive(true);
    }

    public void LockOn()
    {
        if (lockOnTarget != null)
        {
            DisableLockOn();
        }
        else
        {
            targets = FindObjectsOfType<LockOnCore>();

            List<GameObject> validTargets = new List<GameObject>();
            foreach (LockOnCore target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= lockOnRange)
                {
                    validTargets.Add(target.gameObject);
                }
            }

            if (validTargets.Count > 0)
            {
                float shortestDistance = float.MaxValue;
                foreach (GameObject obj in validTargets)
                {
                    float totalDistance = Vector3.Distance(transform.position, obj.transform.position);
                    if (totalDistance < shortestDistance)
                    {
                        shortestDistance = totalDistance;
                        lockOnTarget = obj;
                    }
                }

                LockOnCore core = lockOnTarget.GetComponent<LockOnCore>();
                core.canvasObject.SetActive(true);
            }
        }
    }

    private void DisableLockOn()
    {
        for(int i =0; i<targets.Length; i++)
        {
            targets[i].canvasObject.SetActive(false);
        }
        lockOnTarget = null;
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