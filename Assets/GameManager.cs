using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuScreen;

    public void PlayGame()
    {
        menuScreen.SetActive(false);
    }
}