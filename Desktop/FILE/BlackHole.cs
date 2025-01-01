using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float blackHoleMass = 1.989e30f; // Mass of the black hole in kilograms
    public float eventHorizonRadius = 33.0f; // Radius of the event horizon
    private const float gravitationalConstant = 6.67430e-11f; // Gravitational constant

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate the direction to the black hole's center
            Vector3 direction = (transform.position - other.transform.position).normalized;

            // Calculate the distance to the black hole
            float distance = Vector3.Distance(transform.position, other.transform.position);

            // Clamp the distance to avoid singularities and extreme values
            float minDistance = eventHorizonRadius * 1.1f; // Minimum distance to prevent singularity
            distance = Mathf.Max(distance, minDistance);

            // Prevent unrealistic distances
            distance = Mathf.Clamp(distance, eventHorizonRadius * 1.1f, 1000f);

            // Stop objects inside the event horizon
            if (distance <= eventHorizonRadius)
            {
                rb.velocity = Vector3.zero; // Stop movement
                rb.angularVelocity = Vector3.zero; // Stop rotation
                Debug.Log($"{other.name} has crossed the event horizon and cannot escape.");
                return;
            }

            // Calculate gravitational force using Newton's law of gravitation
            float gravitationalForceMagnitude = gravitationalConstant * blackHoleMass * rb.mass / (distance * distance);
            Vector3 gravitationalForce = direction * gravitationalForceMagnitude;

            // Apply gravitational force
            rb.AddForce(gravitationalForce, ForceMode.Force);

            // Clamp velocity to avoid instability
            float maxVelocity = 100f; // Maximum allowable velocity
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
                Debug.LogWarning($"{other.name}'s velocity was clamped to prevent instability.");
            }

            // Debug log the gravitational force
            Debug.Log($"[BlackHole] Applying force to {other.name}: {gravitationalForce}. Distance: {distance}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} is approaching the black hole!");
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);

            // Prevent exit if within event horizon
            if (distance <= eventHorizonRadius)
            {
                Debug.LogWarning($"{other.name} tried to escape the event horizon but was stopped.");
                return;
            }

            Debug.Log($"{other.name} escaped the black hole's gravitational pull!");
        }
    }
}
