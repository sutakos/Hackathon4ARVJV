using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    private float verticalSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            input.x = (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ? 1f : 0f)
                    - (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ? 1f : 0f);
            input.y = (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed ? 1f : 0f)
                    - (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed ? 1f : 0f);
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y).normalized;

        if (characterController.isGrounded)
        {
            verticalSpeed = -2f;

            if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
            {
                verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
                ChangeColorMaterial.ChangeColorOfGround(characterController);
            }
        }
        else
        {
            verticalSpeed += gravity * Time.deltaTime;
        }

        Vector3 velocity = movement * moveSpeed + Vector3.up * verticalSpeed;
        characterController.Move(velocity * Time.deltaTime);
    }
}
