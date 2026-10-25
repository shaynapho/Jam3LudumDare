using UnityEngine;
using TMPro;

public class GameProgressManager : MonoBehaviour
{
    // allows other scripts to access the game manager
    public static GameProgressManager instance;

    // tracks deliveries and collected cogwheels
    public int deliveriesCompleted = 0;
    public int cogwheels = 0;
    public int requiredCogwheels = 3;
    public int totalDeliveries = 3;

    // text displaying delivery progress
    public TMP_Text deliveryCounter;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateDeliveryCounter();
    }

    // called whenever the player completes a delivery
    public void CompleteDelivery()
    {
        deliveriesCompleted++;
        cogwheels++;

        // update the delivery counter
        UpdateDeliveryCounter();

        Debug.Log("delivery completed");
        Debug.Log("cogwheels: " + cogwheels);

        // check if the player has enough cogwheels
        if (cogwheels >= requiredCogwheels)
        {
            Debug.Log("SuperStructure unlocked");
        }
    }

    // updates the text shown on screen
    void UpdateDeliveryCounter()
    {
        deliveryCounter.text = "Deliveries: " + deliveriesCompleted + "/" + totalDeliveries;
    }
}
