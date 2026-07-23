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
    public TMPro.TextMeshProUGUI endingText;

    public void ShowEndingScreen(string winner)
    {
        ExitScreen.SetActive(true);
        endingText.text = winner + " Wins!";
    }
    public void ReturnToMenu()
    {
        ExitScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

}

