using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator myAnimator;
    int isWalkingHash;
    int isRunningHash;
    int velocityHash;
    bool forwardKeyPress;
    bool lshiftKeyPress;
    bool isWalking, isRunning;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        /*isWalking = myAnimator.GetBool(isWalkingHash);
        isRunning = myAnimator.GetBool(isRunningHash);*/


        forwardKeyPress = Input.GetKey(KeyCode.W);
        lshiftKeyPress = Input.GetKey(KeyCode.LeftShift);

        if(forwardKeyPress && velocity != 1.0f)
		{
            velocity += Time.deltaTime * acceleration;
            
		}
        if(!forwardKeyPress && velocity != 0.0f)
		{
            velocity -= Time.deltaTime * deceleration;
		}
        velocity = Mathf.Clamp(velocity, 0.0f, 1.0f);
        myAnimator.SetFloat(velocityHash, velocity);



        /*if (forwardKeyPress && !isWalking)
		{
            myAnimator.SetBool(isWalkingHash, true);
		}
        if(!forwardKeyPress && isWalking)
		{
            myAnimator.SetBool(isWalkingHash, false);
		}

        if((lshiftKeyPress && forwardKeyPress) && !isRunning)
		{
            myAnimator.SetBool(isRunningHash, true);
		}
        if((!lshiftKeyPress || !forwardKeyPress) && isRunning)
		{
            myAnimator.SetBool(isRunningHash, false);
		}*/
    }
}
