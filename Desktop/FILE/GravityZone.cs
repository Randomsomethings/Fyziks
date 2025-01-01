using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityZone : MonoBehaviour
{
    [Header("Gravity Properties")]
    public Vector3 gravityDirection = Vector3.down; // Default direction for gravity
    public float gravityStrength = 9.81f; // Default gravity strength (m/s²)
    public bool useInverseSquareLaw = false; // Toggle between constant and inverse-square gravity
    public Transform gravitySource; // For inverse-square gravity

    [Header("Drag Properties")]
    public bool useDrag = true; // Apply drag
    public float dragCoefficient = 0.47f; // Drag coefficient (sphere = 0.47)
    public float crossSectionalArea = 1.0f; // Cross-sectional area of the object
    public float airDensity = 1.225f; // Air density (kg/m³)

    [Header("Friction Properties")]
    public bool useFriction = true; // Apply friction
    public float frictionCoefficient = 0.5f; // Coefficient of friction (e.g., 0.5 for wood)

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log($"[GravityZone] {other.name} has entered the Gravity Zone.");
        }
        else
        {
            Debug.LogWarning($"[GravityZone] {other.name} entered the zone, but it has no Rigidbody.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply custom gravity
            if (useInverseSquareLaw && gravitySource != null)
            {
                Vector3 gravityForce = CalculateInverseSquareGravity(rb, other.transform.position);
                rb.AddForce(gravityForce, ForceMode.Acceleration);
                Debug.Log($"[GravityZone] Applying inverse-square gravity to {other.name}. Force: {gravityForce}");
            }
            else
            {
                Vector3 gravityForce = gravityDirection.normalized * gravityStrength * rb.mass;
                rb.AddForce(gravityForce, ForceMode.Acceleration);
                Debug.Log($"[GravityZone] Applying constant gravity to {other.name}. Force: {gravityForce}");
            }

            // Apply drag
            if (useDrag)
            {
                Vector3 dragForce = CalculateDrag(rb.velocity);
                rb.AddForce(dragForce, ForceMode.Acceleration);
                Debug.Log($"[GravityZone] Applying drag to {other.name}. Force: {dragForce}");
            }

            // Apply friction
            if (useFriction)
            {
                Vector3 normalForce = rb.mass * Physics.gravity; // Approximation for flat surfaces
                Vector3 frictionForce = CalculateFriction(normalForce);
                rb.AddForce(frictionForce, ForceMode.Acceleration);
                Debug.Log($"[GravityZone] Applying friction to {other.name}. Force: {frictionForce}");
            }
        }
        else
        {
            Debug.LogWarning($"[GravityZone] {other.name} is in the zone, but it has no Rigidbody.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log($"[GravityZone] {other.name} has exited the Gravity Zone.");
        }
        else
        {
            Debug.LogWarning($"[GravityZone] {other.name} exited the zone, but it has no Rigidbody.");
        }
    }

    // Calculate inverse-square gravity
    private Vector3 CalculateInverseSquareGravity(Rigidbody rb, Vector3 objectPosition)
    {
        if (gravitySource == null) return Vector3.zero;

        Vector3 direction = gravitySource.position - objectPosition;
        float distance = direction.magnitude;

        if (distance == 0) return Vector3.zero; // Prevent division by zero

        float gravityMagnitude = (6.67430e-11f * rb.mass * rb.mass) / (distance * distance); // G * m1 * m2 / r²
        return direction.normalized * gravityMagnitude;
    }

    // Calculate drag force
    private Vector3 CalculateDrag(Vector3 velocity)
    {
        float speed = velocity.magnitude;
        Vector3 dragForce = -0.5f * airDensity * speed * speed * dragCoefficient * crossSectionalArea * velocity.normalized;
        return dragForce;
    }

    // Calculate friction force
    private Vector3 CalculateFriction(Vector3 normalForce)
    {
        return -normalForce.normalized * frictionCoefficient * normalForce.magnitude;
    }
}
