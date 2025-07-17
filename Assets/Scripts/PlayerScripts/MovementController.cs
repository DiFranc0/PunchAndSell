using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the player movement
    [SerializeField] private float rotationSpeed = 20f; // Speed of the player rotation
    [SerializeField] private InputActionReference moveAction; // Reference to the Input Action for movement
    [SerializeField] private CharacterController characterController; // Reference to the CharacterController component
    [SerializeField] private Animator animator; // Reference to the Animator component for animations
    [SerializeField] private float gravity = -9.81f; // Gravity value for the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        ApplyGravityToPlayer();
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        if(moveDir != Vector3.zero)
        {
            // Rotate the movement direction by 45 degrees around the Y-axis
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45,0));

            // Apply the rotation to the movement direction
            moveDir = matrix.MultiplyVector(moveDir);

            characterController.Move(moveDir * speed * Time.deltaTime);

            SetWalkAnimation(true);
            Rotate(moveDir);
        }
        else
        {
            SetWalkAnimation(false);
        }
        
    }

    public void SetWalkAnimation(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);

    }

    void Rotate(Vector3 rotationDir)
    {
        Quaternion targetRotation = Quaternion.LookRotation(rotationDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void ApplyGravityToPlayer()
    {
        characterController.Move(new Vector3(0, gravity, 0) * Time.deltaTime);
    }
}
