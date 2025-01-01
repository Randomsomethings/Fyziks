using UnityEngine;

public class RealisticPhysicsObject : MonoBehaviour
{
    [Header("Physics Properties")]
    public float mass = 1.0f; // Mass in kilograms
    public bool useCustomGravity = true;
    public Vector3 velocity = Vector3.zero; // Initial velocity
    private Vector3 acceleration = Vector3.zero;

    [Header("Drag Properties")]
    public bool useAirDrag = true;
    public float dragCoefficient = 0.47f; // Sphere default
    public float crossSectionalArea = 1.0f; // In m²
    public float airDensity = 1.225f; // Air density at sea level (kg/m³)

    [Header("Friction Properties")]
    public bool useFriction = true;
    public float frictionCoefficient = 0.5f; // Static friction

    private Vector3 gravitationalForce = Vector3.zero;

    private void FixedUpdate()
    {
        // Calculate custom gravity
        if (useCustomGravity)
        {
            gravitationalForce = CalculateGravity();
        }

        // Calculate forces
        Vector3 dragForce = useAirDrag ? CalculateDrag(velocity) : Vector3.zero;
        Vector3 frictionForce = useFriction ? CalculateFriction() : Vector3.zero;

        Vector3 netForce = gravitationalForce - dragForce - frictionForce;

        // Update acceleration, velocity, and position
        acceleration = netForce / mass;
        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private Vector3 CalculateGravity()
    {
        Vector3 gravityDirection = Vector3.down;
        float gravityMagnitude = 9.81f * mass; // Earth's gravity
        return gravityDirection * gravityMagnitude;
    }

    private Vector3 CalculateDrag(Vector3 velocity)
    {
        float speed = velocity.magnitude;
        float drag = 0.5f * dragCoefficient * crossSectionalArea * airDensity * Mathf.Pow(speed, 2);
        return velocity.normalized * drag;
    }

    private Vector3 CalculateFriction()
    {
        return -velocity.normalized * (mass * Physics.gravity.magnitude * frictionCoefficient);
    }
}
