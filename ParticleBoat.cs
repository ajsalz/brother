using UnityEngine;

public class BoatSplashController : MonoBehaviour
{
    [SerializeField] private ParticleSystem splashEffect;
    [SerializeField] private float directionThreshold = 0.1f; // small buffer to avoid flicker

    private Rigidbody2D rb;
    private Vector3 originalLocalPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalLocalPos = splashEffect.transform.localPosition;
    }

    void Update()
    {
        float velocityMovement = Vector2.Dot(rb.linearVelocity, transform.right); // movement along boat's facing direction

        if (velocityMovement > directionThreshold)
        {
            // Moving forward → place splash behind
            splashEffect.transform.localPosition = originalLocalPos;
            splashEffect.transform.localRotation = Quaternion.identity;
        }
        else if (velocityMovement < -directionThreshold)
        {
            // Moving backward → flip splash to front
            splashEffect.transform.localPosition = -originalLocalPos;
            splashEffect.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }

        // Optionally stop emission when nearly stationary
        var emission = splashEffect.emission;
        emission.enabled = Mathf.Abs(velocityMovement) > directionThreshold;
    }
}
