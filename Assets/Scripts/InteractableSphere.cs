using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using TMPro;

public class InteractableSphere : MonoBehaviourPunCallbacks , IPunObservable
{
	
	public RectTransform PromptPopup;
	public Vector2 OffsetPopup;

	
	public bool playerInRange;
	public bool pickedUp;

	public GameObject InteractableObj;
	public Vector3 defaultPosition;
	public Quaternion defaultRotation;

	#region Private Variables

	Vector2 screenUpperLimit;
	Vector2 screenLowerLimit;
	#endregion
	// Start is called before the first frame update
	void Start()
    {
		defaultPosition = InteractableObj.transform.position;
		defaultRotation = InteractableObj.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !pickedUp)
		{
			tempVector = Camera.main.WorldToScreenPoint(transform.position);
			tempVector.x = Mathf.Clamp(tempVector.x, screenLowerLimit.x, screenUpperLimit.x); 
			tempVector.y = Mathf.Clamp(tempVector.y, screenLowerLimit.y,screenUpperLimit.y );
			PromptPopup.anchoredPosition = tempVector;
		}
    }
	Vector2 tempVector;
	private void OnTriggerEnter(Collider other)
	{
		if(!pickedUp)
		{
			if (other.tag == "Player" && photonView.IsMine)
			{
				other.GetComponent<CharacaterInteractions>().ObjectToHold = InteractableObj; //Parent of Trigger Collider Sphere
																							 //Enable popup and listen for button press
				playerInRange = true;
				DefineScreenLimits(PromptPopup.sizeDelta.x, PromptPopup.sizeDelta.y);
				tempVector = Camera.main.WorldToScreenPoint(InteractableObj.transform.position);
				tempVector = new Vector2(Mathf.Clamp(tempVector.x, screenLowerLimit.x, screenUpperLimit.x), Mathf.Clamp(tempVector.y, screenLowerLimit.y, screenUpperLimit.y));
				PromptPopup.anchoredPosition = tempVector;
				//Prompt intialize to pick up obj
				DefinePrompt("E", "Pick Up");
				PromptPopup.gameObject.SetActive(true);
				other.GetComponent<CharacterMovement>().EnablePickupAction();
			}
		}
		else
		{
			if (other.tag == "Player" && photonView.IsMine)
			{
				if(other.GetComponent<CharacaterInteractions>().ObjectToHold != null)
				{
					if(other.GetComponent<CharacaterInteractions>().ObjectToHold == InteractableObj)
					{
						//option to place back held object
						other.GetComponent<CharacaterInteractions>().ObjectToHold = InteractableObj; //Parent of Trigger Collider Sphere
																									 //Enable popup and listen for button press
						playerInRange = true;
						DefineScreenLimits(PromptPopup.sizeDelta.x, PromptPopup.sizeDelta.y);
						tempVector = Camera.main.WorldToScreenPoint(InteractableObj.transform.position);
						tempVector = new Vector2(Mathf.Clamp(tempVector.x, screenLowerLimit.x, screenUpperLimit.x), Mathf.Clamp(tempVector.y, screenLowerLimit.y, screenUpperLimit.y));
						PromptPopup.anchoredPosition = tempVector;
						//Prompt intialize to pick up obj
						DefinePrompt("E", "Place Back");
						PromptPopup.gameObject.SetActive(true);
						other.GetComponent<CharacterMovement>().EnablePickupAction();
					}
					else
					{
						return;
					}
				}
				else
				{
					return;
				}
			}
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		if(!pickedUp)
		{
			if (other.tag == "Player" && photonView.IsMine)
			{
				other.GetComponent<CharacaterInteractions>().ObjectToHold = null;
				//Disable popup and remove listener for button press
				playerInRange = false;
				PromptPopup.gameObject.SetActive(false);
				other.GetComponent<CharacterMovement>().DisablePickupAction();
			}
		}
		else
		{

		}
	}

	public void DefinePrompt(string bindingKey, string action)
	{
		//text prompt with binding and action
		PromptPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
			"Press '" + bindingKey + "' to " + action + ".";
	}

	public void DefineScreenLimits(float width, float height)
	{
		screenUpperLimit = new Vector2(Screen.width - width, Screen.height - height);
		screenLowerLimit = new Vector2(width/2, height); //anchor is at the bottom middle
	}

	public void OnPhotonSerializeView(PhotonStream photonstream, PhotonMessageInfo info)
	{
		if(photonstream.IsWriting)
		{
			photonstream.SendNext(pickedUp);
		}
		else
		{
			this.pickedUp = (bool) photonstream.ReceiveNext();
		}
	}
}
