using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerControlScheme
{
    Player1, // Controls using WASD
    Player2  // Controls using Arrow Keys
}

public class PlayerController : MonoBehaviour
{
    public PlayerControlScheme controlScheme = PlayerControlScheme.Player1;
    // Private Variables
    public float currentSpeed = 0.0f;
    private float maxSpeed = 20.0f;
    private float acceleration = 5.0f;
    private float deceleration = 10.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Determine input axes based on the selected control scheme
        string horizontalAxis = controlScheme == PlayerControlScheme.Player1 ? "Horizontal" : "Horizontal_Arrow";
        string verticalAxis = controlScheme == PlayerControlScheme.Player1 ? "Vertical" : "Vertical_Arrow";

        // This is where we get player input
        horizontalInput = Input.GetAxis(horizontalAxis);
        forwardInput = Input.GetAxis(verticalAxis);

        // If the forward or backward key is pressed, increase speed
        if (forwardInput != 0)
        {
            currentSpeed += acceleration * Time.deltaTime * forwardInput;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed); // Limit speed
        }
        else
        {
            // If the keys are not pressed, the speed gradually decreases
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }
        // We move vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        // We turn the vehicle
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        UpdateWheelTransforms(frontLeftTransform, isFrontWheel: true);
        UpdateWheelTransforms(frontRightTransform, isFrontWheel: true);
        UpdateWheelTransforms(rearLeftTransform, isFrontWheel: false);
        UpdateWheelTransforms(rearRightTransform, isFrontWheel: false);

    }

    private void UpdateWheelTransforms(Transform wheelTransform, bool isFrontWheel)
    {

        wheelTransform.Rotate(Vector3.right, currentSpeed * Time.deltaTime * 360);


        if (isFrontWheel)
        {
            Vector3 currentRotation = wheelTransform.localEulerAngles;
            currentRotation.y = horizontalInput * 30f;
            wheelTransform.localEulerAngles = currentRotation;
        }
    }
}
