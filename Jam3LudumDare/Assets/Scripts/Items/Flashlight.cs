using SimpleFPSController;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.lightEnabled = true;
            Destroy(this.gameObject);
        }

    }
}