using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WalkInScript : MonoBehaviour
{
    public Transform doorhandleIn;
    public Transform doorhandleOut;
    public Vector3 handleInOffset;
    public Vector3 handleOutOffset;
    public Transform ikHoldIn;
    public Transform ikHoldOut;
    Transform righthand;
    Transform lefthand;
    Transform closestDoorhandle;
    Transform currentIkRot;
    Vector3 tempEuler;
    Vector3 currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Contains("Player"))
		{
            //Determine closest door handle
            SetClosestDoorHandle(other.transform);
            //Call to check which hand is closest to the door handle 
            AvatarIKGoal hand = SetClosestHand(other.transform,closestDoorhandle);
            tempEuler = currentIkRot.rotation.eulerAngles;
            if (hand == AvatarIKGoal.RightHand)
			{

                tempEuler.z = -90;
			}
            else
			{
                tempEuler.z = 90;
			}
            currentIkRot.rotation = Quaternion.Euler(tempEuler);
            //Determine the direction the character is facing 
            //set IK to door handle position
            other.GetComponent<IKControl>().AssignDoorHandleAndHandTransform(closestDoorhandle,currentOffset, hand, currentIkRot.rotation);
            other.GetComponent<IKControl>().operationType = 1;
            other.GetComponent<IKControl>().time = 0f;
            other.GetComponent<IKControl>().ikActive = true;
            other.GetComponent<CharacaterInteractions>().ClutchHandle();
			Debug.Log("Velocity : " + other.GetComponent<CharacterController>().velocity);
            Debug.Log("Magnitude :" + other.GetComponent<CharacterController>().velocity.magnitude);

            if(other.GetComponent<CharacterController>().velocity.magnitude > 10f)
			{
                closestDoorhandle.GetComponent<Collider>().attachedRigidbody.AddForceAtPosition(other.GetComponent<CharacterController>().velocity, closestDoorhandle.position, ForceMode.Impulse);
            }
            else
			{
                closestDoorhandle.GetComponent<Collider>().attachedRigidbody.AddForceAtPosition(other.GetComponent<CharacterController>().velocity, closestDoorhandle.position, ForceMode.Force);
            }
            
            //Enable colliders on hand to push the door 
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag.Contains("Player"))
        {
            //Set Ik top false and back to original pos
            other.GetComponent<IKControl>().time = 0f;
            other.GetComponent<IKControl>().ikActive = false;
            other.GetComponent<CharacaterInteractions>().UnClutchHandle();
            //Disable colliders on hand 
        }
    }

    public void SetClosestDoorHandle(Transform player)
	{
        if(Vector3.Distance(doorhandleIn.position,player.position) > Vector3.Distance(doorhandleOut.position, player.position))
		{
            closestDoorhandle = doorhandleOut;
            currentIkRot = ikHoldOut;
		}
        else
		{
            closestDoorhandle = doorhandleIn;
            currentIkRot = ikHoldIn;
		}
	}

    public AvatarIKGoal SetClosestHand(Transform player,Transform handle)
	{
         if(player.GetComponent<CharacaterInteractions>().RightArmDistanceFromTransform(handle)
            > player.GetComponent<CharacaterInteractions>().LeftArmDistanceFromTransform(handle))
		{
            return AvatarIKGoal.LeftHand;
		}
         else
		{
            return AvatarIKGoal.RightHand;
		}
	}
}
