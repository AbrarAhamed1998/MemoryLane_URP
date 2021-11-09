using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableObject : MonoBehaviour
{
	
	public RectTransform PromptPopup;
	public Vector2 OffsetPopup;

	
	public bool playerInRange;
	public bool pickedUp;

	

	#region Private Variables

	Vector2 screenUpperLimit;
	Vector2 screenLowerLimit;
	#endregion
	// Start is called before the first frame update
	void Start()
    {
        
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
		if(other.tag == "Player")
		{
            other.GetComponent<CharacaterInteractions>().ObjectToHold = transform.parent.gameObject; //Parent of Trigger Collider Sphere
			//Enable popup and listen for button press
			playerInRange = true;
			DefineScreenLimits(PromptPopup.sizeDelta.x, PromptPopup.sizeDelta.y);
			tempVector = Camera.main.WorldToScreenPoint(transform.parent.position);
			tempVector = new Vector2(Mathf.Clamp(tempVector.x, screenLowerLimit.x, screenUpperLimit.x), Mathf.Clamp(tempVector.y, screenLowerLimit.y, screenUpperLimit.y));
			PromptPopup.anchoredPosition = tempVector;
			PromptPopup.gameObject.SetActive(true);
			other.GetComponent<CharacterMovement>().EnablePickupAction();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<CharacaterInteractions>().ObjectToHold = null;
			//Disable popup and remove listener for button press
			playerInRange = false;
			PromptPopup.gameObject.SetActive(false);
			other.GetComponent<CharacterMovement>().DisablePickupAction();
		}
	}

	public void DefinePrompt(string bindingKey, string action)
	{
		//text prompt with binding and action
	}

	public void DefineScreenLimits(float width, float height)
	{
		screenUpperLimit = new Vector2(Screen.width - width, Screen.height - height);
		screenLowerLimit = new Vector2(width/2, height); //anchor is at the bottom middle
	}
}
