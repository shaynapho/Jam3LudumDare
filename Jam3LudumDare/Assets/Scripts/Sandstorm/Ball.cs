using UnityEngine;

public class Ball : MonoBehaviour
{
    public float lifeTime = 10f;
    public bool inStormZone = false;
    public GameObject stormZone;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destruction();
            }
        }

        if (this.transform.position.y <= -20)
        {
            Destruction();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("ball entered trigger: " + coll.name);

        if (coll.gameObject.tag == "stormArea")
        {
            stormZone = coll.gameObject;
            inStormZone = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "stormArea")
        {
            inStormZone = false;
        }
    }

    void FixedUpdate()
    {
        if (inStormZone && stormZone != null)
        {
            StormArea storm = stormZone.GetComponent<StormArea>();

            if (storm.stormActive)
            {
                rb.AddForce(storm.direction.normalized * storm.strength);
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "destroyer")
        {
            Destruction();
        }
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
