using UnityEngine;

public class FlyScript : MonoBehaviour
{
    public float flySpeed = 10.0f;
    private bool isFlying = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Change this key as needed
        {
            isFlying = !isFlying;

            if (isFlying)
            {
                // Enable flying mode
                GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                // Disable flying mode
                GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (isFlying)
        {
            // Handle flying controls
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
            Vector3 moveVelocity = moveDirection.normalized * flySpeed;

            GetComponent<Rigidbody>().velocity = moveVelocity;
        }
    }
}