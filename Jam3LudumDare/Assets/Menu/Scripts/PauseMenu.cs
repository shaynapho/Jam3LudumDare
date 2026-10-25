using Unity.VectorGraphics;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //public GameObject firstpersoncamera;
    public GameObject container;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

}

