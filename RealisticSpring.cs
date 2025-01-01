using UnityEngine;

public class RealisticSpring : MonoBehaviour
{
    [Header("Spring Properties")]
    public Transform anchorPoint;
    public float springConstant = 10.0f; // k in Hooke's Law
    public float damping = 0.1f; // To stabilize oscillation

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 displacement = transform.position - anchorPoint.position;
        Vector3 springForce = -springConstant * displacement;
        Vector3 dampingForce = -damping * rb.velocity;

        rb.AddForce(springForce + dampingForce);
    }
}
