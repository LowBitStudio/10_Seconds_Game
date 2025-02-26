using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Obstacle_ : MonoBehaviour
{
    [SerializeField] private int Time_Point;

    //Collision of this object with the player
    //Will cause the timer to decrease
    //Which lead to game over.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Timer_Countdown.instance.ReducingTimePoint(Time_Point);
            gameObject.SetActive(false);
        }
    }
}
