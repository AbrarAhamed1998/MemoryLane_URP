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
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public float weightValue = 0f;
    public float actionSpeed = 0.01f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
	private void Update()
	{
        if (ikActive)
		{
            weightValue = Mathf.Lerp(weightValue, 1f, actionSpeed );
            
		}
        if(!ikActive)
		{
            weightValue = Mathf.Lerp(weightValue, 0f, actionSpeed );
            
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
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(weightValue);
                    animator.SetLookAtPosition(lookObj.position);
                }

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
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }
        }
    }

    public void TweenWeightValuesTo(float targetValue)
    {
       
        
    }
    
}
