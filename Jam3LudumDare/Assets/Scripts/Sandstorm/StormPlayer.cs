using UnityEngine;

public class StormPlayer : MonoBehaviour
{
    public float windMultiplier = 0.3f;
    public float speedMultiplier = 0.5f;
    public float shelterDistance = 8f;

    Rigidbody rb;
    StormArea storm;
    FirstPersonMovement movement;

    float normalSpeed;
    float normalRunSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<FirstPersonMovement>();

        normalSpeed = movement.speed;
        normalRunSpeed = movement.runSpeed;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.TryGetComponent(out StormArea area))
            storm = area;
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.TryGetComponent(out StormArea area) && storm == area)
            storm = null;
    }

    void FixedUpdate()
    {
        bool exposed = storm != null && storm.stormActive && !IsSheltered();

        // slow player down during the storm
        movement.speed = exposed ? normalSpeed * speedMultiplier : normalSpeed;
        movement.runSpeed = exposed ? normalRunSpeed * speedMultiplier : normalRunSpeed;

        // push player with the wind
        if (exposed)
        {
            Vector3 wind = storm.direction.normalized;
            wind.y = 0;

            rb.MovePosition(rb.position + wind * storm.strength *
                windMultiplier * Time.fixedDeltaTime);
        }
    }

    bool IsSheltered()
    {
        Vector3 wind = storm.direction.normalized;
        wind.y = 0;

        return Physics.Raycast(
            transform.position + Vector3.up,
            -wind,
            out RaycastHit hit,
            shelterDistance,
            ~0,
            QueryTriggerInteraction.Ignore
        ) && hit.collider.CompareTag("stormShelter");
    }

    void OnDisable()
    {
        if (movement != null)
        {
            movement.speed = normalSpeed;
            movement.runSpeed = normalRunSpeed;
        }
    }
}
