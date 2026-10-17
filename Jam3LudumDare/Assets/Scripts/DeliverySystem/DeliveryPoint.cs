using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    // tracks whether this delivery has been completed
    private bool delivered = false;
    // checks if the player is close enough
    private bool playerNearby = false;

    void Update()
    {
        // press E to deliver water
        if (playerNearby && !delivered && Input.GetKeyDown(KeyCode.E))
        {
            delivered = true;

            // update the game's delivery progress
            GameProgressManager.instance.CompleteDelivery();

            Debug.Log("water delivered");
        }
    }

    // player enters the delivery area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("press E to deliver water");
        }
    }

    // player leaves the delivery area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
