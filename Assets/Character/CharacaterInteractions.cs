using DG.Tweening;
using DitzelGames.FastIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CharacaterInteractions : MonoBehaviour
{
    #region Public members

    public GameObject ObjectToHold;
    public Transform holdTransform;
    public Animator myAnimator;
    public Transform Thumb_R;
    public Transform Thumb_target;
    public Transform rightHand;
    public Transform leftHand;
    public Quaternion rightHandIdleRot;
    public Quaternion leftHandIdleRot;
    public bool holdObj;

    public float holdAnimDuration = 1;

    public bool clutchHandle;
    public float clutchHandleDuration;
	#endregion
	#region Coroutine variables

	WaitUntil ikWait;
    #endregion


    #region Private Variables
    float time;
    InteractableSphere currentInteractable;
    PictureScroll currentPicScroll;
	#endregion
	// Start is called before the first frame update
	void Start()
    {
        ikWait = new WaitUntil(() => GetComponent<IKControl>().ikActive = false);
        myAnimator = GetComponent<Animator>();
        rightHandIdleRot = rightHand.transform.localRotation;
        leftHandIdleRot = leftHand.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdObj)
		{
            if (time < holdAnimDuration)
            {
                myAnimator.SetLayerWeight(1, Mathf.Lerp((float)GetComponent<Animator>().GetLayerWeight(1), 1f, time / holdAnimDuration));
                time += Time.deltaTime;
            }
            else
            {
                myAnimator.SetLayerWeight(1, 1f);
            }
        }
        else
		{
            if (time < holdAnimDuration)
            {
                myAnimator.SetLayerWeight(1, Mathf.Lerp((float)GetComponent<Animator>().GetLayerWeight(1), 0f, time / holdAnimDuration));
                time += Time.deltaTime;
            }
            else
            {
                myAnimator.SetLayerWeight(1, 0f);
            }
        }

        if (clutchHandle)
        {
            if (time < clutchHandleDuration)
            {
                myAnimator.SetLayerWeight(2, Mathf.Lerp((float)GetComponent<Animator>().GetLayerWeight(2), 1f, time / clutchHandleDuration));
                time += Time.deltaTime;
            }
            else
            {
                myAnimator.SetLayerWeight(2, 1f);
            }
        }
        else
        {
            if (time < holdAnimDuration)
            {
                myAnimator.SetLayerWeight(2, Mathf.Lerp((float)GetComponent<Animator>().GetLayerWeight(2), 0f, time / clutchHandleDuration));
                time += Time.deltaTime;
            }
            else
            {
                myAnimator.SetLayerWeight(2, 0f);
            }
        }

    }

    public void GrabObject()
	{
        IKGrab(ObjectToHold.transform);
	}
    public void PlaceBackObj()
	{
        IKPlaceBack(ObjectToHold.transform); 
	}
    void IKGrab(Transform objectToHold)
	{
        

        Sequence hold = DOTween.Sequence();
        hold
            .AppendCallback(()=>
            {
                GetComponent<IKControl>().AssignRightHandAndLookAtObj(objectToHold.GetComponent<InteractableObj>().relativeInteractableSphere.transform); //passing default pos for smooth transition
                GetComponent<IKControl>().time = 0f;
                GetComponent<IKControl>().operationType = 0; //grab object operation
                GetComponent<IKControl>().ikActive = true;
                //yield return new WaitForSeconds(1f);
                
            })
            .AppendInterval(0.5f)
            .AppendCallback(()=> {
                objectToHold.SetParent(holdTransform);
                objectToHold.localPosition = Vector3.zero;
                objectToHold.localRotation = Quaternion.identity;
                })
            //.Append(objectToHold.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.InOutExpo))
            //.Join(objectToHold.DOLocalRotateQuaternion(Quaternion.identity, 0.2f).SetEase(Ease.InOutExpo))
            .AppendCallback(()=>
            {
                GetComponent<IKControl>().time = 0f;
                GetComponent<IKControl>().ikActive = false;
                //Assign object to right hand hierarchy of this character and place into pose.



                //ObjectToHold.transform.GetChild(2).GetComponent<SphereCollider>().enabled = false;
                currentInteractable = ObjectToHold.GetComponent<InteractableObj>().relativeInteractableSphere;
                //currentInteractable.GetComponent<SphereCollider>().enabled = false;
                currentInteractable.playerInRange = false;
                currentInteractable.pickedUp = true;

                currentInteractable.PromptPopup.gameObject.SetActive(false);

                ObjectToHold.GetComponent<InteractableObj>().OnPickupAction();

                GetComponent<CharacterMovement>().EnablePhoneControls();

                //SetAnimation Weight (Try Lerping this)
                time = 0f;
                holdObj = true;
            })
            .Play();
        
        

        //GetComponent<Animator>().SetLayerWeight(1, 1);

        
	}


    void IKPlaceBack(Transform objectToPlace)
	{
        Sequence hold = DOTween.Sequence();
        hold
            .AppendCallback(() =>
            {
                GetComponent<IKControl>().AssignRightHandAndLookAtObj(objectToPlace.GetComponent<InteractableObj>().relativeInteractableSphere.transform); //passing default pos for smooth transition
                GetComponent<IKControl>().time = 0f;
                GetComponent<IKControl>().operationType = 0; 
                GetComponent<IKControl>().ikActive = true;
                //yield return new WaitForSeconds(1f);

            })
            .AppendInterval(0.5f)
            .AppendCallback(() => {
                objectToPlace.SetParent(objectToPlace.GetComponent<InteractableObj>().relativeInteractableSphere.transform); //place back at sphere center
                objectToPlace.localPosition = Vector3.zero;
                objectToPlace.localRotation = Quaternion.identity;
            })
            //.Append(objectToHold.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.InOutExpo))
            //.Join(objectToHold.DOLocalRotateQuaternion(Quaternion.identity, 0.2f).SetEase(Ease.InOutExpo))
            .AppendCallback(() =>
            {
                GetComponent<IKControl>().time = 0f;
                GetComponent<IKControl>().ikActive = false;
                //Assign object to right hand hierarchy of this character and place into pose.



                //ObjectToHold.transform.GetChild(2).GetComponent<SphereCollider>().enabled = false;
                currentInteractable = ObjectToHold.GetComponent<InteractableObj>().relativeInteractableSphere;
                //currentInteractable.GetComponent<SphereCollider>().enabled = false;
                currentInteractable.playerInRange = true;
                currentInteractable.pickedUp = false;

                currentInteractable.PromptPopup.gameObject.SetActive(false);

                ObjectToHold.GetComponent<InteractableObj>().OnPlaceBack();

                //GetComponent<CharacterMovement>().EnablePhoneControls();

                //SetAnimation Weight (Try Lerping this)
                time = 0f;
                holdObj = false;
            })
            .Play();
    }


    public void PressButtonAction(Transform target)
	{

        Thumb_R.GetComponent<FastIKFabric>().Target = target;
        Thumb_R.GetComponent<FastIKFabric>().enabled = true;
	}

    public void ReleaseButtonAction()
	{
        Thumb_R.GetComponent<FastIKFabric>().Target = null;
        Thumb_R.GetComponent<FastIKFabric>().enabled = false;
	}

    public void ScrollLeftProcedure()
	{
        //press action 
        //simluate button
        //call button event
        //finger falls back

        if(currentPicScroll == null)
		{
            currentPicScroll = ObjectToHold.GetComponent<PictureScroll>();
        }
        
        
        Sequence leftScroll = DOTween.Sequence();

        leftScroll
            .AppendCallback(() =>
            {
                PressButtonAction(ObjectToHold.GetComponent<PictureScroll>().phoneLeftButtonTarget);
                currentPicScroll.SimulateButtonPress(currentPicScroll.leftButton);
            })
            .AppendInterval(0.1f)
            .AppendCallback(() => ReleaseButtonAction())
            .Play();
        
	}

    public void ScrollRightProcedure()
    {
        //press action 
        //simluate button
        //call button event
        //finger falls back

        if (currentPicScroll == null)
		{
            currentPicScroll = ObjectToHold.GetComponent<PictureScroll>();
        }
        

        Sequence leftScroll = DOTween.Sequence();

        leftScroll
            .AppendCallback(() =>
            {
                PressButtonAction(ObjectToHold.GetComponent<PictureScroll>().phoneRightButtonTarget);
                currentPicScroll.SimulateButtonPress(currentPicScroll.rightButton);
            })
            .AppendInterval(0.1f)
            .AppendCallback(() => ReleaseButtonAction())
            .Play();

    }

    public float LeftArmDistanceFromTransform(Transform handle)
	{
        return Vector3.Distance(leftHand.position, handle.position);
	}
    public float RightArmDistanceFromTransform(Transform handle)
	{
        return Vector3.Distance(rightHand.position, handle.position);
	}


    public void ClutchHandle()
	{
        time = 0f;
        clutchHandle = true;
	}

    public void UnClutchHandle()
	{
        time = 0f;
        clutchHandle = false;
	}
}
