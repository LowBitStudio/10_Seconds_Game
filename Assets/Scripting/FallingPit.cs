using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Timer_Countdown.instance.ReducingTimePoint(Timer_Countdown.instance.Timer_Counter);
        }
    }
}
