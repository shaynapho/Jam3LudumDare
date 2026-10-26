using UnityEngine;

public class SuperStructure : MonoBehaviour
{
    public GameObject interactionPrompt;
    public GameObject gameOverScreen;

    private bool playerNearby = false;
    private bool gameFinished = false;

    void Start()
    {
        interactionPrompt.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && !gameFinished)
        {
            bool hasAllCogwheels = GameProgressManager.instance.cogwheels >= GameProgressManager.instance.requiredCogwheels;

            if (hasAllCogwheels && Input.GetKeyDown(KeyCode.E))
            {
                gameFinished = true;

                interactionPrompt.SetActive(false);
                gameOverScreen.SetActive(true);

                // unlock cursor for end screen
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // stops gameplay
                Time.timeScale = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            playerNearby = true;

            if (GameProgressManager.instance.cogwheels >= GameProgressManager.instance.requiredCogwheels)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            playerNearby = false;
            interactionPrompt.SetActive(false);
        }
    }
}
