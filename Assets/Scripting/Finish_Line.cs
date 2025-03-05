using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Line : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Timer_Countdown.instance.PlayerWins = true;
            Debug.Log($"This {collision.gameObject.name} touches the finish line");
        }
    }
}
