using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Vector3 OffsetDistance;
    public Vector3 OffsetRotation;
    public Vector3 MoveToPos;
    public Transform parentToAssign;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(parentToAssign);
        transform.position = OffsetDistance;
        transform.eulerAngles = OffsetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveToPos = parentToAssign.position + OffsetDistance;
        //transform.position = Vector3.Lerp(transform.position,MoveToPos,0.1f);
        //transform.eulerAngles = OffsetRotation;
    }
}
