using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedRagdoll : MonoBehaviour
{
    public List<Rigidbody> RagdollRigidbodies = new List<Rigidbody>();
    public Collider myBodyCollider;
	private void Awake()
	{
		
	}
	// Start is called before the first frame update
	void Start()
    {
        //TurnONRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnONRagdoll()
	{
        //myBodyCollider.enabled = false;
        GetComponent<Animator>().enabled = false;
        //GetComponent<Animator>().avatar = null;
        for(int i=0;i<RagdollRigidbodies.Count;i++)
		{
            RagdollRigidbodies[i].isKinematic = false;
            //RagdollRigidbodies[i].velocity = velocity;
		}

        //StartCoroutine(WaitToStandBackUP());
	}

    public void TurnOFFRagdoll()
	{
        for (int i = 0; i < RagdollRigidbodies.Count; i++)
        {
            RagdollRigidbodies[i].isKinematic = true;
        }
        GetComponent<Animator>().enabled = true;
        myBodyCollider.enabled = true;
    }


    IEnumerator WaitToStandBackUP()
	{
        yield return new WaitForSeconds(5.0f);
        TurnOFFRagdoll();
	}



    private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Projectile" || other.gameObject.tag == "Enemy")
		{
            TurnONRagdoll();
		}
	}
}
