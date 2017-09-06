using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Animator BikeAnimator;
    public Animator CameraAnimator;

    [Range(0.0001f,0.05f)]
    public float cameraTime;

    private void Update()
    {
        BikeAnimation();
        CameraAnimation();
    }

    void BikeAnimation()
    {
        float input = GetComponent<BikeMotor>().GetInput();
        if (GetComponent<Rigidbody>().velocity.x > 2f && input < 1 && input > -1)
        {
            input = 1;
        } else if (GetComponent<Rigidbody>().velocity.x < -2f && input < 1 && input > -1)
        {
            input = -1;
        }
        if (BikeAnimator.GetInteger("Direction") != input && input <= 1)
        {
            BikeAnimator.SetInteger("Direction", (int)input);
            
        }
        BikeAnimator.speed = Mathf.Abs(GetComponent<Rigidbody>().velocity.x / 30);
    }

    void CameraAnimation()
    {
        if (!GetComponent<PlayerManager>().GetLifeStatus() && !CameraAnimator.GetBool("Dead"))
        {
            CameraAnimator.SetBool("Dead", true);
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            //Camera.main.GetComponentInParent<Transform>().parent = null;
        }
        if (CameraAnimator.GetBool("Dead") && !CameraAnimator.GetBool("Restart"))
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(51, 0, 0), cameraTime);
        }
    }

    public void RestartCamera()
    {
        CameraAnimator.SetBool("Restart", true);
    }
}
