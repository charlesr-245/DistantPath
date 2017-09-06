using UnityEngine;

public class BikeLeaning : MonoBehaviour {

    public float leanAmount;

    private int input;
    private bool disabled = false;

	void FixedUpdate () {
        if (!GetComponent<BikeMotor>().OnGround() && !disabled)
        {
            InputHandler();
            Lean();
        }
	}

    void InputHandler()
    {
        input = (int)Input.GetAxisRaw("Lean");
    }

    void Lean()
    {
        if (input > 0)
            transform.Rotate(0,0, -leanAmount);
        else if (input < 0)
            transform.Rotate(0, 0, leanAmount);
    }

    public void DisableLeaning()
    {
        transform.eulerAngles = Vector3.zero;
        disabled = true;
    }
}
