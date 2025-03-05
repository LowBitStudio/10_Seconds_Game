using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    //UI Screens
    [SerializeField] 
    private GameObject
        PauseUI,
        GameOverUI,
        WinUI;
    
    private bool isPaused;

    void Awake()
    {
        if(PauseUI != null) PauseUI.SetActive(false);
        if(GameOverUI != null) GameOverUI.SetActive(false);
        if(WinUI != null) WinUI.SetActive(false);

        isPaused = false;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        //This function activated the game over screen
        GameOverUI.SetActive(true);
        Audio_Effect.instance.PlayingSFX
        (
            Audio_Effect.instance.Sfx_clip[3]
        );
        Debug.Log($"Game Over!");
    }

    public void Winning()
    {
        WinUI.SetActive(true);
        Time.timeScale = 0;
        Audio_Effect.instance.PlayingSFX
        (
            Audio_Effect.instance.Sfx_clip[2]
        );
        Debug.Log($"Player Wins!");
    }

    public void PausingGame()
    {
        isPaused = !isPaused;
        
        Time.timeScale = isPaused ? 0 : 1;
        
        PauseUI.SetActive(isPaused ? true : false);

        Audio_Effect.instance.PlayingSFX
        (
            isPaused ? Audio_Effect.instance.Sfx_clip[1] : Audio_Effect.instance.Sfx_clip[0]
        );
        
        Debug.Log($"Pausing the game!");
    }

    void Update()
    {
        if(Timer_Countdown.instance.PlayerWins || Timer_Countdown.instance.PlayerLoses)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PausingGame();
        }
    }
}
