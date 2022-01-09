using UnityEngine;
using System;
using System.Collections;
using UnityEngine.InputSystem.XR.Haptics;
using DG.Tweening;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    Transform rightHandObj = null;
    Transform lookObj = null;
    public float weightValue = 0f;
    public float actionDuration = 1f;
    public Transform doorHandleTransform;
    public AvatarIKGoal handToOpenDoor;
    /// <summary>
    /// Operation type defines the type of IK operation to take place:
    /// <list type="bullet">
    ///<item>
    /// <term>0</term> <description> IK active means to pickup object</description>
    /// </item>
    /// <item>
    /// <term>1</term> <description> Ik active to open doors with the correct hand based on which direction the character is facing.</description>
    /// </item>
    /// </list>
    /// </summary>
    public int operationType;
    [HideInInspector]
    public float time;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
	private void Update()
	{
        if (ikActive)
		{
            time += Time.deltaTime;
            weightValue = Mathf.Lerp(weightValue, 1f, time/actionDuration);
            
		}
        if(!ikActive)
		{
            time += Time.deltaTime;
            weightValue = Mathf.Lerp(weightValue, 0f, time/actionDuration);
            
        }
        weightValue = Mathf.Clamp(weightValue, 0f, 1f);

        
    }
	//a callback for calculating IK
	void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive) 
            {
                switch(operationType)
				{
                    case 0:
                        // Set the right hand target position and rotation, if one has been assigned
                        if (rightHandObj != null)
                        {
                            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weightValue);
                            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weightValue);
                            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                        }
                        break;
                    case 1:
                        //door open IK code
                        animator.SetIKPositionWeight(handToOpenDoor, weightValue);
                        animator.SetIKRotationWeight(handToOpenDoor, weightValue);
                        animator.SetIKPosition(handToOpenDoor, doorHandleTransform.position);
                        animator.SetIKRotation(handToOpenDoor, doorHandleTransform.rotation);
                        break;
                }
                // Set the look target position, if one has been assigned
                //if (lookObj != null)
                //{
                    //animator.SetLookAtWeight(weightValue);
                    //animator.SetLookAtPosition(lookObj.position);
                //}





                

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weightValue);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weightValue);
                animator.SetLookAtWeight(weightValue);
                if(rightHandObj != null)
				{
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                
            }
        }
    }

    public void TweenWeightValuesTo(float targetValue) //Add a tweener here for float values
    {
       
        
    }

    public void AssignRightHandAndLookAtObj(Transform objTransform)
	{
        rightHandObj = objTransform;
        lookObj = objTransform;
	}
    
}
