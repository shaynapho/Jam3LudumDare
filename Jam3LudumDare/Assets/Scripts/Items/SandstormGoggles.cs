using UnityEngine;

public class SandstormGoggles : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("colliding");
            PlayerManager.gogglesCollected = true;
            Destroy(this.gameObject);
        }

    }
}