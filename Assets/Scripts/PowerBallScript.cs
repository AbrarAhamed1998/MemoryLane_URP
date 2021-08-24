using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBallScript : MonoBehaviour
{
    public GameObject mainBodySphere;
    public GameObject prefabSphere;
    public float force;
    GameObject tempprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPrefabSohere()
	{
        tempprefab = Instantiate(prefabSphere,mainBodySphere.transform.position,Quaternion.identity);
        tempprefab.GetComponent<Rigidbody>().AddForce(Vector3.forward * force, ForceMode.Acceleration);
        StartCoroutine(DelayedDestroyer(tempprefab));
	}

    IEnumerator DelayedDestroyer(GameObject prefab)
	{
        yield return new WaitForSeconds(3.0f);
        Destroy(prefab);
	}

    public void LaunchObjectEvent()
	{
        SpawnPrefabSohere();
        mainBodySphere.SetActive(false);
    }
    
    public void ReinitializeObject()
	{
        mainBodySphere.SetActive(true);
    }

}
