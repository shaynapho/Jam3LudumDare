
using UnityEngine;

public class StormPlayer : MonoBehaviour
{
    public float windMultiplier = 0.3f;
    public float speedMultiplier = 0.5f;

    private Rigidbody rb;
    private StormArea currentStorm;
    private FirstPersonMovement movement;

    private float normalSpeed;
    private float normalRunSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<FirstPersonMovement>();

        normalSpeed = movement.speed;
        normalRunSpeed = movement.runSpeed;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.TryGetComponent<StormArea>(out StormArea storm))
        {
            currentStorm = storm;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.TryGetComponent<StormArea>(out StormArea storm)
            && currentStorm == storm)
        {
            currentStorm = null;
        }
    }

    void FixedUpdate()
    {
        bool inActiveStorm = currentStorm != null && currentStorm.stormActive;

        // reduce movement speed during the storm
        movement.speed = inActiveStorm ? normalSpeed * speedMultiplier : normalSpeed;

        movement.runSpeed = inActiveStorm ? normalRunSpeed * speedMultiplier : normalRunSpeed;

        // push player in the wind direction
        if (inActiveStorm)
        {
            Vector3 wind = currentStorm.direction.normalized;
            wind.y = 0;

            rb.MovePosition(rb.position + wind * currentStorm.strength * windMultiplier * Time.fixedDeltaTime);
        }
    }

    void OnDisable()
    {
        // restore movement speeds
        if (movement != null)
        {
            movement.speed = normalSpeed;
            movement.runSpeed = normalRunSpeed;
        }
    }
}
