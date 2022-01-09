using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInScript : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;
    public Transform doorhandle;
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
            //Call to check which hand is closest to the door handle 

            //Determine the direction the character is facing 
            //set IK to door handle position
            other.GetComponent<IKControl>().doorHandleTransform = doorhandle;
            //Enable colliders on hand to push the door 
		}
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag.Contains("Player"))
        {
            //Set Ik top false and back to original pos
            //Disable colliders on hand 
        }
    }
}
