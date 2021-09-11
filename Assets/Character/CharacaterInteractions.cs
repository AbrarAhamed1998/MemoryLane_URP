using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacaterInteractions : MonoBehaviour
{
    #region Public members

    public GameObject ObjectToHold;
    public Transform holdTransform;
	#endregion
	#region Coroutine variables

	WaitUntil ikWait;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ikWait = new WaitUntil(() => GetComponent<IKControl>().ikActive = false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabObject(Transform objectToHold, Transform lookAtobj)
	{
        StartCoroutine(IKGrab(objectToHold,lookAtobj));
	}

    IEnumerator IKGrab(Transform objectToHold, Transform lookAtobj)
	{
        GetComponent<IKControl>().ikActive = true;
        yield return new WaitForSeconds(1f);
        GetComponent<IKControl>().ikActive = false;

        //Assign object to right hand hierarchy of this character and place into pose.
	}
}
