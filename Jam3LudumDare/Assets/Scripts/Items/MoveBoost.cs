using SimpleFPSController;
using UnityEngine;

public class MoveBoost : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("colliding");
            FirstPersonMovement fpsMovement = other.GetComponent<FirstPersonMovement>();
            Jump jumpScript = other.GetComponent<Jump>();
            fpsMovement.speed = 18;
            jumpScript.jumpStrength = 4;
            Destroy(this.gameObject);
        }

    }
}
