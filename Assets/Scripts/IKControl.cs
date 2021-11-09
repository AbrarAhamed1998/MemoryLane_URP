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
            weightValue = Mathf.Lerp(weightValue, 1f, time/actionDuration);
            time += Time.deltaTime;
		}
        if(!ikActive)
		{
            weightValue = Mathf.Lerp(weightValue, 0f, time/actionDuration);
            time += Time.deltaTime;
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

                // Set the look target position, if one has been assigned
                //if (lookObj != null)
                //{
                    //animator.SetLookAtWeight(weightValue);
                    //animator.SetLookAtPosition(lookObj.position);
                //}

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weightValue);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weightValue);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }

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
