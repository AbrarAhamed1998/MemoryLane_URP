using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Photon.Pun;
using DG.Tweening;

public class CharacterMovement : MonoBehaviourPun
{
    
    public float speed = 50f;
    public float rotSensitivity = 10f;
    public float pushPower;
    
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int velocityXHash;
    int velocityZHash;
    PlayerInput playerInput;
    Vector2 movementAnimValues;
    Vector3 movementControllerValues;
    Vector2 lookValues;
    Vector2 rotation = Vector2.zero;
    bool movementPressed;
    bool runPressed;
    CharacterController charController;
	private void Awake()
	{
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Movement.started += ctx => CatchMovement(ctx);
        playerInput.CharacterControls.Movement.performed += ctx => CatchMovement(ctx);
        playerInput.CharacterControls.Movement.canceled += ctx => CatchMovement(ctx);

        playerInput.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Look.performed += ctx => lookValues = ctx.ReadValue<Vector2>();

        playerInput.CharacterControls.PickupObj.performed += ctx => CheckForPickup();
        //By default  pickupobj is disabled on launch
        playerInput.CharacterControls.PickupObj.Disable();
        
        playerInput.CharacterControls.PhoneControlLeft.performed += ctx => CheckForPhoneControl(playerInput.CharacterControls.PhoneControlLeft);
        playerInput.CharacterControls.PhoneControlRight.performed += ctx => CheckForPhoneControl(playerInput.CharacterControls.PhoneControlRight);
        //By default Phone controls are also disabled on launch
        playerInput.CharacterControls.PhoneControlLeft.Disable();
        playerInput.CharacterControls.PhoneControlRight.Disable();
        

    }
	// Start is called before the first frame update
	void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");

        
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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        HandleMovement();
        //HandleRotation();
        HandleCameraMovement();
        //HandleGravity();
    }

	private void LateUpdate()
	{
        
	}

	void HandleRotation()
	{
        Vector3 curremntPos = transform.position;
        Vector3 newPos = new Vector3(movementAnimValues.x, 0f, movementAnimValues.y);
        Vector3 posToLookAt = curremntPos + newPos;
        transform.LookAt(Vector3.Lerp(curremntPos,posToLookAt,0.001f));
	}
    void HandleMovement()
	{
        bool isRunning = animator.GetBool(isRunningHash);
        /*bool isWalking = animator.GetBool(isWalkingHash);

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
		}*/
        movementAnimValues.x = Mathf.Clamp(movementAnimValues.x, -0.5f, 0.5f);
        movementAnimValues.y = Mathf.Clamp(movementAnimValues.y, -0.5f, 0.5f);

        //Reference for Tweeening float value below
        //DOTween.To(() => cameraController.desiredDistance, x => cameraController.desiredDistance = x, camZOffset, 1f).SetEase(Ease.InOutExpo))


        animator.SetFloat(velocityZHash, movementAnimValues.y);
        animator.SetFloat(velocityXHash, movementAnimValues.x);

        if(runPressed && !isRunning)
		{
            animator.SetBool(isRunningHash, true);
            speed += 20f;
        }
        if(!runPressed && isRunning)
		{
            animator.SetBool(isRunningHash, false);
            speed -= 20f;
        }
        //movementControllerValues.x = movementAnimValues.x;
        //movementControllerValues.z = movementAnimValues.y;
        //movementControllerValues = (transform.right * movementControllerValues.x + transform.forward * movementControllerValues.z) * speed * Time.deltaTime;
        //HandleGravity();
        //charController.Move(movementControllerValues);

    }

	private void OnAnimatorMove()
	{
        movementControllerValues = animator.deltaPosition;
        HandleGravity();
        charController.Move(movementControllerValues);

    }

	void HandleCameraMovement()
	{
        rotation.y += lookValues.x;
        rotation.x += -lookValues.y;
        rotation.x = Mathf.Clamp(rotation.x, -75, 75);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(rotation), rotSensitivity * Time.deltaTime);;
        transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(0f, rotation.y, 0f), rotSensitivity * Time.deltaTime);
	}

    void CatchMovement(InputAction.CallbackContext ctx)
	{
        Vector2 maxmovemmentVals = ctx.ReadValue<Vector2>();
        //movementAnimValues = ctx.ReadValue<Vector2>();
        DOTween.To(() => movementAnimValues, x => movementAnimValues = x, maxmovemmentVals, 0.5f).SetEase(Ease.InOutExpo);
        movementPressed = movementAnimValues.x != 0f || movementAnimValues.y != 0f;
    }

    void HandleGravity()
	{
        if(charController.isGrounded)
		{
            movementControllerValues.y = -0.05f;
            //Debug.Log("Gravity Applied");
		}
        else
		{
            movementControllerValues.y = -9.81f;
        }
        
	}

    public void EnablePickupAction()
	{
        playerInput.CharacterControls.PickupObj.Enable();
	}

    public void DisablePickupAction()
	{
        playerInput.CharacterControls.PickupObj.Disable();
	}

    public void DisablePhoneControls()
	{
        playerInput.CharacterControls.PhoneControlLeft.Disable();
        playerInput.CharacterControls.PhoneControlRight.Disable();
	}

    public void EnablePhoneControls()
	{
        playerInput.CharacterControls.PhoneControlLeft.Enable();
        playerInput.CharacterControls.PhoneControlRight.Enable();
	}
    public void CheckForPickup()
	{
        if(photonView.IsMine)
		{
            if (playerInput.CharacterControls.PickupObj.enabled)
            {
                if (GetComponent<CharacaterInteractions>().ObjectToHold)
                {
                    if(GetComponent<CharacaterInteractions>().ObjectToHold.GetComponent<InteractableObj>().relativeInteractableSphere.pickedUp)
					{
                        GetComponent<CharacaterInteractions>().PlaceBackObj();
                    }
                    else
					{
                        GetComponent<CharacaterInteractions>().GrabObject();
                    }
                    
                }
                
            }
        }
        
	}

    public void CheckForPhoneControl(InputAction action)
	{
        if(action.enabled)
		{
            if(GetComponent<CharacaterInteractions>().ObjectToHold)
			{
                if(action.name.Contains("Left"))
				{
                    //scroll to left
                    GetComponent<CharacaterInteractions>().ScrollLeftProcedure();

                }
                else
				{
                    //scroll to right
                    GetComponent<CharacaterInteractions>().ScrollRightProcedure();
				}
			}
		}
	}
    

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        Vector3 force;

        if (hit.gameObject.tag.Contains("Door"))
		{
            if(hit.collider.attachedRigidbody != null && !hit.collider.attachedRigidbody.isKinematic)
			{
                force = hit.controller.velocity.magnitude * hit.normal * -1;

                hit.collider.attachedRigidbody.AddForceAtPosition(force, hit.point);
			}
		}
	}

    
}
