using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI : MonoBehaviour
{
    [SerializeField] private GameObject StartPage;
    [SerializeField] private GameObject LevelSelect;


    void Awake()
    {
        StartPage.SetActive(true);
        LevelSelect.SetActive(false);    
    }

    public void PlayingSFX()
    {
        Audio_Effect.instance.PlayingSFX(Audio_Effect.instance.Sfx_clip[0]);
    }

    public void QuitGame()
    {
        Audio_Effect.instance.PlayingSFX(Audio_Effect.instance.Sfx_clip[1]);
        StartCoroutine
        (
            QuitSceneAfterEffect(Audio_Effect.instance.Sfx_clip[1].length)
        );
    }

    private IEnumerator QuitSceneAfterEffect(float sfx_length)
    {
        yield return new WaitForSeconds(sfx_length);
        Application.Quit(); Debug.Log($"Quitting the Game");
    }
}
