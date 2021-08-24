using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    PlayerInput playerInput;
    Vector2 movementValues;
    Vector2 lookValues;
    Vector2 rotation = Vector2.zero;
    bool movementPressed;
    bool runPressed;
	private void Awake()
	{
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Movement.performed += ctx =>
        {
            movementValues = ctx.ReadValue<Vector2>();
            movementPressed = movementValues.x != 0f || movementValues.y != 0f;
        };
        playerInput.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Look.performed += ctx => lookValues = ctx.ReadValue<Vector2>();
	}
	// Start is called before the first frame update
	void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

	private void OnEnable()
	{
        playerInput.CharacterControls.Enable();
	}

	private void OnDisable()
	{
        playerInput.CharacterControls.Disable();
	}
	// Update is called once per frame
	void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCameraMovement();
    }

    void HandleRotation()
	{
        Vector3 curremntPos = transform.position;
        Vector3 newPos = new Vector3(movementValues.x, 0f, movementValues.y);
        Vector3 posToLookAt = curremntPos + newPos;
        transform.LookAt(Vector3.Lerp(curremntPos,posToLookAt,0.001f));
	}
    void HandleMovement()
	{
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        if (movementPressed && !isWalking)
		{
            animator.SetBool(isWalkingHash, true);
		}

        if(!movementPressed && isWalking)
		{
            animator.SetBool(isWalkingHash, false);
		}

        if(movementPressed && runPressed && !isRunning)
		{
            animator.SetBool(isRunningHash, true);
		}

        if((!movementPressed || !runPressed) && isRunning)
		{
            animator.SetBool(isRunningHash, false);
		}
    }

    void HandleCameraMovement()
	{
        rotation.y += lookValues.x;
        rotation.x += -lookValues.y;
        rotation.x = Mathf.Clamp(rotation.x, -85, 85);
        Camera.main.transform.rotation = Quaternion.Euler(rotation );
        transform.rotation = Quaternion.Euler(new Vector3(0f, rotation.y, 0f));
	}
}
