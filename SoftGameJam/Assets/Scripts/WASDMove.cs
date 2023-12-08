using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed

    void FixedUpdate()
    {
        // Get the raw input values for horizontal and vertical movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Move the camera based on the input and speed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}