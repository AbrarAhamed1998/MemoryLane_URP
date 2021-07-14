using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    Vector3 MouseWorldPoint;
	[SerializeField]float movementSpeed = 10f;
	[SerializeField]float rotSpeed = 1f;
    float xPos;
    float zPos;
	float xRot;
	float yRot;
	bool startJump;
	float jumptimer;


	[SerializeField] GameObject lowerRayObj;
	[SerializeField] GameObject upperRayobj;
	[SerializeField]float stepheight = 0.3f;
	[SerializeField]float stepSmooth = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
	{
#if UNITY_EDITOR || UNITY_STANDALONE

		

		Rotation();

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleCursorLockStateAndVisibility();
		}
		/*if (Input.GetKeyDown(KeyCode.Space))
		{
			startJump = true;
			jumptimer = Time.time;
			//GetComponent<Rigidbody>().AddForce(transform.up * 50f, ForceMode.Impulse);
		}
		if(startJump && Time.time - jumptimer < 1f && jumptimer != 0f)
		{
			
			GetComponent<Rigidbody>().AddForce(transform.up * 50f, ForceMode.Acceleration);
		}

		if(Time.time - jumptimer >= 1f && jumptimer != 0f)
		{
			jumptimer = 0f;
			startJump = false;
		}*/

#endif
	}
	private void FixedUpdate()
	{
		Translation();
		Gravity();
		ElevationCheck();
	}

	private void Translation()
	{
		xPos = Input.GetAxis("Horizontal") * movementSpeed;
		zPos = Input.GetAxis("Vertical") * movementSpeed;

		//transform.Translate(xPos, 0, zPos); //movement by transform 


		InitializeDirections(zPos,xPos);
		GetComponent<Rigidbody>().velocity = camDirectionz + camDirectionx;


		//GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * zPos + Camera.main.transform.right * xPos;
	}

	private void Rotation()
	{
		if(!Cursor.visible)
		{
			xRot += Input.GetAxis("Mouse X") * rotSpeed;
			if (xRot <= -180)
			{
				xRot += 360;
			}
			else if (xRot > 180)
			{
				xRot -= 360;
			}
			yRot -= Input.GetAxis("Mouse Y") * (rotSpeed / 2);

			Camera.main.transform.localRotation = Quaternion.Euler(yRot, 0f, 0f);
			transform.localRotation = Quaternion.Euler(0f, xRot, 0f);
		}
		
		
	}

	public void ToggleCursorLockStateAndVisibility()
	{
		if(Cursor.lockState == CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}


	public void Gravity()
	{
		GetComponent<Rigidbody>().AddForce(Physics.gravity * GetComponent<Rigidbody>().mass * 20f, ForceMode.Acceleration);
	}


	Vector3 camDirectionz;
	Vector3 camDirectionx;
	public void InitializeDirections(float scaleFactorz,float scaleFactorx)
	{
		camDirectionz = new Vector3(Camera.main.transform.forward.x * scaleFactorz, 0f, Camera.main.transform.forward.z * scaleFactorz);
		camDirectionx = new Vector3(Camera.main.transform.right.x * scaleFactorx, 0f, Camera.main.transform.right.z * scaleFactorx);
	}

	public void ElevationCheck()
	{
		RaycastHit hitlower;
		if(Physics.Raycast(lowerRayObj.transform.position, transform.TransformDirection(Vector3.forward),out hitlower, 0.1f))
		{
			RaycastHit hitupper;
			if(!Physics.Raycast(upperRayobj.transform.position, transform.TransformDirection(Vector3.forward), out hitupper, 0.2f))
			{
				GetComponent<Rigidbody>().position -= new Vector3(0f, -stepSmooth, 0f);
				//Debug.Log("Lifted");
			}
		}

		RaycastHit hitLower45;
		if (Physics.Raycast(lowerRayObj.transform.position, transform.TransformDirection(1.5f,0f,1f), out hitLower45, 0.1f))
		{
			RaycastHit hitupper45;
			if (!Physics.Raycast(upperRayobj.transform.position, transform.TransformDirection(1.5f,0f,1f), out hitupper45, 0.2f))
			{
				GetComponent<Rigidbody>().position -= new Vector3(0f, -stepSmooth, 0f);
				//Debug.Log("Lifted");
			}
		}

		RaycastHit hitLowerMinus45;
		if (Physics.Raycast(lowerRayObj.transform.position, transform.TransformDirection(-1.5f, 0f, 1f), out hitLowerMinus45, 0.2f))
		{
			RaycastHit hitUpperMinus45;
			if (!Physics.Raycast(upperRayobj.transform.position, transform.TransformDirection(-1.5f, 0f, 1f), out hitUpperMinus45, 0.3f))
			{
				GetComponent<Rigidbody>().position -= new Vector3(0f, -stepSmooth, 0f);
				//Debug.Log("Lifted");
			}
		}
	}
}
