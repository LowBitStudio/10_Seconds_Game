using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_ClockItem : MonoBehaviour
{
    //Var
    [SerializeField] private int Time_Point = 3;

    //Collider trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //If the player hits this item
            //Then it will put an extra time to the timer
            Timer_Countdown.instance.AddingTimePoint(Time_Point);
            //After that the item will disappear
            gameObject.SetActive(false);
        }
    }
}
