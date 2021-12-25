using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public InteractableSphere relativeInteractableSphere;
	public List<GameObject> objectsToEnable = new List<GameObject>();
	/// <summary>
	/// Actions to execute on picking up this object
	/// </summary>
    public void OnPickupAction()
	{
		for (int i = 0; i < objectsToEnable.Count; i++)
		{
			objectsToEnable[i].SetActive(true);
		}
	}

	public void OnPlaceBack()
	{
		for (int i = 0; i < objectsToEnable.Count; i++)
		{
			objectsToEnable[i].SetActive(false);
		}
	}
}
