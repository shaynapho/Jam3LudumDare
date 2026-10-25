using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuAGain : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;

            }
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
