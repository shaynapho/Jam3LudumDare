using UnityEngine;

public class StormArea : MonoBehaviour
{
    public float strength = 25f;
    public Vector3 direction = new Vector3(0, 0, -1);
    public float calmDuration = 10f;
    public float stormDuration = 12f;
    public bool stormActive = false;
    public ParticleSystem sandDust;
    public AudioSource calmWind;
    public AudioSource strongWind;

    private float timer;
    private ParticleSystemForceField forceField;
    private BoxCollider stormCollider;
    private Transform player;

    void Start()
    {
        forceField = GetComponent<ParticleSystemForceField>();

        if (forceField == null)
        {
            forceField = gameObject.AddComponent<ParticleSystemForceField>();
        }

        forceField.shape = ParticleSystemForceFieldShape.Box;

        stormCollider = GetComponent<BoxCollider>();

        // find the player using their movement script
        FirstPersonMovement movement =
            FindFirstObjectByType<FirstPersonMovement>();

        if (movement != null)
        {
            player = movement.transform;
        }

        timer = calmDuration;

        if (sandDust != null)
        {
            sandDust.Stop();
        }

        // start the calm wind audio
        if (calmWind != null)
        {
            calmWind.Play();
        }

        if (strongWind != null)
        {
            strongWind.Stop();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            stormActive = !stormActive;

            if (stormActive)
            {
                timer = stormDuration;
                Debug.Log("sandstorm started");
            }
            else
            {
                timer = calmDuration;
                Debug.Log("sandstorm ended");
            }

            if (sandDust != null)
            {
                if (stormActive)
                {
                    sandDust.Play();
                }
                else
                {
                    sandDust.Stop();
                }
            }

            // switches between calm and strong wind
            if (calmWind != null && strongWind != null)
            {
                if (stormActive)
                {
                    calmWind.Stop();
                    strongWind.Play();
                }
                else
                {
                    strongWind.Stop();
                    calmWind.Play();
                }
            }
        }

        // checks whether the player is inside the storm collider
        bool playerInside = false;

        if (player != null && stormCollider != null)
        {
            Vector3 localPosition =
                transform.InverseTransformPoint(player.position);

            Vector3 offset = localPosition - stormCollider.center;
            Vector3 halfSize = stormCollider.size * 0.5f;

            playerInside =
                Mathf.Abs(offset.x) <= halfSize.x &&
                Mathf.Abs(offset.y) <= halfSize.y &&
                Mathf.Abs(offset.z) <= halfSize.z;
        }

        // mutes both sounds outside the storm zone
        if (calmWind != null)
        {
            calmWind.mute = !playerInside;
        }

        if (strongWind != null)
        {
            strongWind.mute = !playerInside;
        }

        Vector3 finalForce = Vector3.zero;

        if (stormActive)
        {
            finalForce = direction.normalized * strength;
        }

        forceField.directionX = finalForce.x;
        forceField.directionY = finalForce.y;
        forceField.directionZ = finalForce.z;
    }
}
