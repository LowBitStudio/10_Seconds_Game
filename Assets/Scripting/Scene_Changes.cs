using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Changes : MonoBehaviour
{
    public void ChangeScene(string SceneName)
    {
        Audio_Effect.instance.PlayingSFX(Audio_Effect.instance.Sfx_clip[0]);
        SceneManager.LoadScene(SceneName);
    }

    public void RestarttheGame()
    {
        Audio_Effect.instance.PlayingSFX(Audio_Effect.instance.Sfx_clip[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
