using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject ExitScreen;

    public void PlayGame()
    {
        menuScreen.SetActive(false);
        ExitScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Application closing");
        Application.Quit();
    }
    public void ShowEndingScreen()
    {
        ExitScreen.SetActive(true);
    }
    public void ReturnToMenu()
    {
        ExitScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

}

