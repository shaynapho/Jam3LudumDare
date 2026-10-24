using UnityEngine;
public class LightTrigger : MonoBehaviour
{
    [SerializeField] public Light flashlight;
    private void Start()
    {
        flashlight.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if (PlayerManager.lightEnabled == true)
            {
                Debug.Log("should turn on");
                flashlight.enabled = true;
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (PlayerManager.lightEnabled == true)
            {
                Debug.Log("should turn off");
                flashlight.enabled = false;
            }
        }

    }
}
