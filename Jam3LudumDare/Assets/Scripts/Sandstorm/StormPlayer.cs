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
        Debug.Log("StormPlayer started on: " + gameObject.name);

        rb = GetComponent<Rigidbody>();
        movement = GetComponent<FirstPersonMovement>();

        // save the player's original movement speeds
        normalSpeed = movement.speed;
        normalRunSpeed = movement.runSpeed;
    }

    // check if player enters or leaves the storm area
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("storm detected: " + coll.name);
        Debug.Log("Player entered trigger: " + coll.name);

        if (coll.TryGetComponent(out StormArea area))
        {
            storm = area;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.TryGetComponent(out StormArea area) && storm == area)
        {
            storm = null;
        }
    }

    void FixedUpdate()
    {
        // check if storm is active and player isnt behind cover
        bool exposed = storm != null && storm.stormActive && !IsSheltered();

        if (PlayerManager.gogglesCollected == false) ;
        {
            // slow player down during the storm
            movement.speed = exposed ? normalSpeed * speedMultiplier : normalSpeed;
            movement.runSpeed = exposed ? normalRunSpeed * speedMultiplier : normalRunSpeed;

            // push player with the wind
            if (exposed)
            {
                Vector3 wind = storm.direction.normalized;
                wind.y = 0;

                rb.MovePosition(rb.position + wind * storm.strength * windMultiplier * Time.fixedDeltaTime);
            }
        }
    }

    // check for a blockade between the player and the wind
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

    // reset movement speeds if script gets disabled
    void OnDisable()
    {
        if (movement != null)
        {
            movement.speed = normalSpeed;
            movement.runSpeed = normalRunSpeed;
        }
    }
}
