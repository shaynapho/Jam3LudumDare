using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StormArea : MonoBehaviour
{
    public float strength = 25f;
    public Vector3 direction = new Vector3(0,0,-1);
    public float calmDuration = 10f;
    public float stormDuration = 12f;
    public bool stormActive = false;

    private float timer;
    private ParticleSystemForceField forceField;

    void Start()
    {
        forceField = GetComponent<ParticleSystemForceField>();

        if (forceField == null)
        {
            forceField = gameObject.AddComponent<ParticleSystemForceField>();
        }

        forceField.shape = ParticleSystemForceFieldShape.Box;
        timer = calmDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            stormActive = !stormActive;

            if(stormActive)
            {
                timer = stormDuration;
                Debug.Log("sandstorm started");
            } else {
                timer = calmDuration;
                Debug.Log("sandstorm ended");
            }
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
