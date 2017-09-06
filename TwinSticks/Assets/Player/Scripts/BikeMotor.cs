using UnityEngine;

public class BikeMotor : MonoBehaviour {

    [Header("Forces")]
    public float forwardsForce;
    public float backwardsForce;
    public float boostMultiplier;

    [Header("Maximum Forces")]
    public float maxBoostMultiplier;
    public float maxForwardsVelocity;
    public float maxBackwardsVelocity;

    [Header("Other")]
    public GameObject Wheel;
    public float groundDistance;
    public ForceMode BikeForceMode;
    public bool debug;

    private Rigidbody rb;
    private float input;
    private float boostInput;
    private bool disabled = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        BikeMotion();
        if (debug)
            DebugMessages();
        //Debug.Log("safdsfdsfs");
    }

    void BikeMotion()
    {
        if (OnGround() && InputHandler() && !disabled)
            ApplyBikeForce();
        ClampBikeForce();
    }

    public bool OnGround()
    {
        Ray ray = new Ray(Wheel.transform.position, -transform.up);

        RaycastHit[] results = Physics.RaycastAll(ray, groundDistance);
        
        foreach (RaycastHit r in results)
        {
            if (r.transform.gameObject.layer == LayerMask.NameToLayer("TraversableAll") || (r.transform.gameObject.layer == LayerMask.NameToLayer("Traversable1") && gameObject.layer == LayerMask.NameToLayer("Player1")) || (r.transform.gameObject.layer == LayerMask.NameToLayer("Traversable2") && gameObject.layer == LayerMask.NameToLayer("Player2")))
            {
                //Debug.Log("true");
                return true;
            }
        }
        //Debug.Log("false");
        return false;
    }

    bool InputHandler()
    {
        input = (int)Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Boost"))
        {
            if (input > 0)
            {
                input += Input.GetAxis("Boost");
                boostInput = Input.GetAxis("Boost");
            }
            //Debug.Log(input);
        }

        if (input != 0)
        {
            return true;
        }
        return false;
    }

    void ApplyBikeForce()
    {
        Vector3 force = new Vector3();
        if (input > 0f)
        {
            if (input > 1f)
                force = (forwardsForce + (forwardsForce*boostMultiplier*boostInput)) * transform.right;
            else
                force = forwardsForce * transform.right;
        }

        else if (input < 0f)
            force = backwardsForce * transform.right;
        rb.AddForce(force, BikeForceMode);
    }

    void ClampBikeForce()
    {
        //float clampedForce;
        Vector3 clampedForce = rb.velocity;
        if (input == 1f)
            clampedForce = Vector3.ClampMagnitude(rb.velocity, maxForwardsVelocity);
        else if (input > 1f)
            clampedForce = Vector3.ClampMagnitude(rb.velocity, maxForwardsVelocity * maxBoostMultiplier*2);
        else if (input == -1)
            clampedForce = Vector3.ClampMagnitude(rb.velocity, -maxBackwardsVelocity);
        rb.velocity = clampedForce;
    }

    public void DisableMovement()
    {
        rb.velocity = Vector3.zero;
        disabled = true;
    }

    public float GetInput()
    {
        return input;
    }

    void DebugMessages()
    {
        Debug.DrawRay(transform.position, transform.right*5, Color.red);
        Debug.DrawRay(Wheel.transform.position, -transform.up*groundDistance, Color.blue);
    }
}
