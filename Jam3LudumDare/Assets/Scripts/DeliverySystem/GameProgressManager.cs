using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    // allows other scripts to access the game manager
    public static GameProgressManager instance;

    // tracks deliveries and collected cogwheels
    public int deliveriesCompleted = 0;
    public int cogwheels = 0;
    public int requiredCogwheels = 5;

    void Awake()
    {
        instance = this;
    }

    // called whenever the player completes a delivery
    public void CompleteDelivery()
    {
        deliveriesCompleted++;
        cogwheels++;

        Debug.Log("delivery completed");
        Debug.Log("cogwheels: " + cogwheels);

        // check if the player has enough cogwheels
        if (cogwheels >= requiredCogwheels)
        {
            Debug.Log("SuperStructure unlocked");
        }
    }
}
