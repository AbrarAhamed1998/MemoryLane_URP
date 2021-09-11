using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
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
		if(other.tag == "Player")
		{
            other.GetComponent<CharacaterInteractions>().ObjectToHold = transform.parent.gameObject;
			//Enable popup and listen for button press
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<CharacaterInteractions>().ObjectToHold = null;
			//Disable popup and remove listener for button press
		}
	}
}
