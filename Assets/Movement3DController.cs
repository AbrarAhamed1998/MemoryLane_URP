using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement3DController : MonoBehaviour
{
    public Animator animator;
    int VelocityXHash;
    int VelocityZHash;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    bool forwardPress, leftPress, rightPress,backPress, lshiftPress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");

        forwardPress = Input.GetKey(KeyCode.W);
        leftPress = Input.GetKey(KeyCode.A);
        rightPress = Input.GetKey(KeyCode.D);
        backPress = Input.GetKey(KeyCode.S);
        lshiftPress = Input.GetKey(KeyCode.LeftShift);
        float currentMaxVelocity = lshiftPress ? 2.0f : 0.5f;
        #region Forward acceleration and deceleration
        if (forwardPress && velocityZ <= currentMaxVelocity)
		{
            velocityZ += Time.deltaTime * acceleration;
		}
        if (!forwardPress && velocityZ >=0f)
		{
            velocityZ -= Time.deltaTime * deceleration;
		}
		#endregion

		#region Backward Acceleration and deceleration
		if (backPress && velocityZ >= -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (!backPress && velocityZ <= 0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
		#endregion

		#region Left acceleration and deceleration
		if (leftPress && velocityX >= -currentMaxVelocity)
		{
            velocityX -= Time.deltaTime * acceleration;
		}
        if(!leftPress && velocityX <= 0f)
		{
            velocityX += Time.deltaTime * deceleration;
        }
		#endregion

		#region Right Acceleration and Deceleration
		if (rightPress && velocityX <= currentMaxVelocity)
		{
            velocityX += Time.deltaTime * acceleration;
		}
        if(!rightPress && velocityX >= 0f)
		{
            velocityX -= Time.deltaTime * deceleration;
        }
		#endregion
		velocityX = Mathf.Clamp(velocityX, -2f, 2f);
        velocityZ = Mathf.Clamp(velocityZ, -2f, 2f);
        animator.SetFloat(VelocityXHash, velocityX);
        animator.SetFloat(VelocityZHash, velocityZ);
    }
}
