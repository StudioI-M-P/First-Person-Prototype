using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] InputActions inputActions;
                     InputAction jumpAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] CharacterController characterController;
    [Space]
    [SerializeField] Camera cam;
    [SerializeField] Transform camHolder;
    [SerializeField] private float camSens = 10f;
    [Space]
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = .05f;
                     private bool isGrounded;


    Vector2 moveInput;
    Vector2 lookInput;
    Vector3 playerVelocity;
    #endregion
    private void Awake()
    {
        inputActions = new InputActions();
        jumpAction = inputActions.Player.Jump;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        jumpAction.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        InputReader();
        Movement();
        GravityAndGroundCheck();
        Jump();
    }

    private void LateUpdate()
    {
        CameraMovement();
    }

    void InputReader()
    {
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    void Movement()
    {
        
        Vector3 moveDirection = cam.transform.forward * moveInput.y + cam.transform.right * moveInput.x;
        moveDirection = Vector3.Normalize(moveDirection);
        moveDirection.y = verticalVelocity;
        

        characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
    }

    void CameraMovement()
    {
        var lookDirection = new Vector2(lookInput.x, lookInput.y);

        cam.transform.position = camHolder.transform.position;
        Vector3 euler = new Vector3(lookDirection.x, lookDirection.y, 0);

        float xRotation = euler.y * camSens * Time.fixedDeltaTime;
        float yRotation = euler.x * camSens * Time.fixedDeltaTime;

        camHolder.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    void GravityAndGroundCheck()
    {
        playerVelocity = new Vector3(0f, verticalVelocity, 0f);

        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void Jump()
    {

        if (jumpAction.WasPerformedThisFrame() && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
