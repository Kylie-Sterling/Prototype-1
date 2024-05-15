using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PlayerController play;
    public GameObject player;
    public float sensitivity;
    public Vector2 movementInput;

    public float minX;
    public float maxX;

    private Vector3 offset;

    void FixedUpdate()
    {
        if (play.lockOnTarget != null)
        {
            // Assuming `lockOnTarget` is the target you want the camera to face
            //Transform lockOnTarget = play.lockOnTarget.transform;

            // Your existing rotation code
            transform.RotateAround(player.transform.position, -Vector3.up, 0); //camInputX * sensitivity
            transform.RotateAround(Vector3.zero, transform.right, 0); //camInputY * sensitivity

            // Look at the lockOnTarget

            transform.LookAt(play.lockOnTarget.transform);


            // Get the current rotation
            Vector3 currentRotation = transform.localRotation.eulerAngles;

            // Clamp the X-axis rotation between minX and maxX
            float clampedX = ClampAngle(currentRotation.x, minX, maxX);

            // Lock the Z-axis rotation to zero
            float clampedZ = 0f;

            // Apply the updated rotation
            transform.localRotation = Quaternion.Euler(clampedX, currentRotation.y, clampedZ);

        }
        else
        {

        // apply the rotation
        transform.RotateAround(player.transform.position, Vector3.up, movementInput.x * sensitivity);
        transform.RotateAround(player.transform.position, transform.right, -movementInput.y * sensitivity);

        // get the current rotation
        Vector3 currentRotation = transform.localRotation.eulerAngles;

        // clamp the X-axis rotation between minX and maxX
        float clampedX = ClampAngle(currentRotation.x, minX, maxX);

        // Lock the Z-axis rotation to zero
        float clampedZ = 0f;

        // Apply the updated rotation
        transform.localRotation = Quaternion.Euler(clampedX, currentRotation.y, clampedZ);
        }
    }

    // Function to clamp angle between -180 and 180 degrees
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -180f)
            angle += 360f;
        if (angle > 180f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movementInput.x = Mathf.Clamp(movementInput.x, -1, 1);
        movementInput.y = Mathf.Clamp(movementInput.y, -1, 1);
        movementInput.x *= -1;
        movementInput.y *= -1;
    }
}
