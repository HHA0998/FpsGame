using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float movmentSpeed = 5f;
    public float fallGravityMultiplier = 2f;
    public float jumpHeight = 2f;
    public float mouseSensitivity = 2.0f;
    public float pitchRange = 60.0f;

    public float fovChange = 10.0f;
    float currentZoomLevel;
    float defaultZoom = 110;
    float zoomInView = 30;
    float zoomOutView = 125;

    private float forawrdInputValue;
    private float strafeInputValue;

    private bool jumpInput;
    private bool isRunning;

    private float terminalVelocity = 53f;
    private float verticalVelocity;

    private float rotateCameraPitch;

    private Camera firstPersonCam;
    private CharacterController characterController;
    // Start is called before the first frame update

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        firstPersonCam = GetComponentInChildren<Camera>();
    }
    void Start()
    {
        float oldCamerFov = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        forawrdInputValue = Input.GetAxisRaw("Vertical");
        strafeInputValue = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        Movment();
        JumpAndGravity();
        CameraMovment();
        ZoomFov();
    }
    void Movment()
    {
        if (isRunning)
        {
            movmentSpeed = 10;
        }
        else
        {
            movmentSpeed = 5;
        }
        Vector3 direction = (transform.forward * forawrdInputValue + transform.right * strafeInputValue).normalized * movmentSpeed * Time.deltaTime;
        direction += Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(direction);
    }
    void JumpAndGravity()
    {
        if (characterController.isGrounded)
        {
            if(verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }
            if (jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            }
        }
        else
        {
            if(verticalVelocity < terminalVelocity)
            {
                float gravityMultiplier = 1f;
                if(characterController.velocity.y < -1f)
                {
                    gravityMultiplier = fallGravityMultiplier;
                }
                verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
            }
        }
    }
    void CameraMovment()
    {
        float rotateYaw = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotateYaw, 0);

        rotateCameraPitch += -Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotateCameraPitch = Mathf.Clamp(rotateCameraPitch, -pitchRange, pitchRange);
        firstPersonCam.transform.localRotation = Quaternion.Euler(rotateCameraPitch, 0, 0);
    }

    void ZoomFov()
    {
        if(Input.GetButton("Fire2") && !(Input.GetKey(KeyCode.LeftShift)))
        {
            currentZoomLevel = zoomInView;
            isRunning = false;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !(Input.GetButton("Fire2")))
        {
            currentZoomLevel = zoomOutView;
            isRunning = true;
        }
        else
        {
            currentZoomLevel = defaultZoom;
            isRunning = false;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, currentZoomLevel, fovChange * Time.deltaTime);

    }
}
