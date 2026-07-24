using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject ExitScreen;
    public Health player1Health;
    public Health player2Health;

    private void Start()
    {
        ExitScreen.SetActive(false);

        player1Health.HideHealthBar();
        player2Health.HideHealthBar();
    }

    public void PlayGame()
    {
        menuScreen.SetActive(false);
        ExitScreen.SetActive(false);

        player1Health.ShowHealthBar();
        player2Health.ShowHealthBar();

        player1Health.ResetHealth();
        player2Health.ResetHealth();
    }
    public void QuitGame()
    {
        Debug.Log("Application closing");
        Application.Quit();
    }
    public TMPro.TextMeshProUGUI endingText;

    public void ShowEndingScreen(string winner)
    {
        player1Health.HideHealthBar();
        player2Health.HideHealthBar();
        ExitScreen.SetActive(true);
        endingText.text = winner + " Wins!";
    }
    public void ReturnToMenu()
    {
        ExitScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

}

