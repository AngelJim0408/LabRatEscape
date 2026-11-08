using UnityEngine;
using UnityEngine.InputSystem;

public class RatController : MonoBehaviour
{

    private CharacterController controller;

    private InputAction _moveAction;
    private InputAction _jumpAction;

    private float verticalVelocity;

    [Header("Movement Settings")]
    [SerializeField]
    private float speed;
    
    [Header("Jump Settings")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpRememberanceTime = 0.02f; // Lets you press the jump button a moment before touching the ground and still jump
    private float currentJumpRememberanceTime;
    [SerializeField]
    private float coyoteTime = 0.02f; // Lets you jump slightly after falling off an edge and still jump
    private float currentCoyoteTime;
    
    [Header("Gravity Settings")]
    [SerializeField]
    [Min(0f)]
    private float fallForce;
    [SerializeField]
    [Min(0f)]
    private float maxFallSpeed;

    void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
       _moveAction = InputSystem.actions.FindAction("Move");
       _jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal
        Vector2 movementHorizontalVector = _moveAction.ReadValue<Vector2>();

        // Vertical 
        currentCoyoteTime = controller.isGrounded ? coyoteTime : Mathf.Max(currentCoyoteTime - Time.deltaTime, 0);

        bool jumped = _jumpAction.ReadValue<float>() > 0;
        currentJumpRememberanceTime = jumped ? jumpRememberanceTime : currentJumpRememberanceTime -= Time.deltaTime;
        if (currentJumpRememberanceTime > 0) {
            Jump();
        }

        verticalVelocity = currentCoyoteTime > 0 ? -0.1f : verticalVelocity + Mathf.Max(-fallForce * Time.deltaTime, -maxFallSpeed); // Gravity

        // Combined
        Vector3 movementVector = new Vector3(movementHorizontalVector.x, verticalVelocity, movementHorizontalVector.y)  * speed;
        controller.Move(movementVector * Time.deltaTime);
    }

    void Jump() {
        if (currentCoyoteTime > 0) {
            verticalVelocity = jumpForce;
            currentJumpRememberanceTime = 0;
            currentCoyoteTime = 0;
        }
    }
}
