using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Respawn(coll.gameObject);
        }
    }

    private void Respawn(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = respawnPoint.position;
            rb.rotation = respawnPoint.rotation;
        } else {
            player.transform.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);
        }
    }
}
