using UnityEngine;

public class FyziksObject : MonoBehaviour
{
    public float mass = 1.0f;
    public bool isAffectedByCustomGravity = true;
    private Vector3 velocity;

    void FixedUpdate()
    {
        if (isAffectedByCustomGravity)
        {
            ApplyCustomGravity();
        }
    }

    private void ApplyCustomGravity()
    {
        Vector3 customGravity = new Vector3(0, -9.8f, 0);
        velocity += customGravity * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
