using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer_Countdown : MonoBehaviour
{
    public static Timer_Countdown instance;
    [SerializeField] private int Timer_Counter;
    [SerializeField] private TextMeshProUGUI Timer_Text;

    //The timer will start counting down to ten
    //If the timer reaches 10
    //The game will be over
    //This will act some sort of health for the player

    void Start()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        //Countdown will start at 10 in default
        Timer_Counter = 10;
        //Begin to countdown
        StartCoroutine(Countdown());  
    }

    public void AddingTimePoint(int point)
    {
        Timer_Counter += point;
        Debug.Log($"Extra time added by {point}");
    }
    
    public void ReducingTimePoint(int point)
    {
        Timer_Counter -= point;
        Debug.Log($"Extra time reduced by {point}");
    }

    private void GameOver()
    {
        //This function activated the game over screen
        Debug.Log($"Game Over!");
    }

    IEnumerator Countdown()
    {
        while(Timer_Counter >= 0)
        {
            Timer_Text.text = Timer_Counter.ToString();
            
            yield return new WaitForSeconds(1f);
            
            Timer_Counter--;
        }
        //Game Over Screen on display
        GameOver();
    }
}
