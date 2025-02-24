using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_StaminaBar : MonoBehaviour
{
    //Stamina Var
    [SerializeField] private float CurrentStamina, MaxStamina;
    [SerializeField] private float SprintCost;
    [SerializeField] private Image StaminaBar;

    void Awake()
    {
        CurrentStamina = MaxStamina;
    }

    void Update()
    {
        //If the player had stamina that are above sufficient recharge point
        //The player are able to run
        if(CurrentStamina >= 1f)
            Player_Variable.CanRun = true;
        else
            Player_Variable.CanRun = false;
        
        //If the player were running and the stamina is above zero
        //Then it will decrease over time
        if(Player_Variable.isRunning)
        {
            CurrentStamina -= SprintCost * Time.deltaTime;
        }
        //Once the player is running out of stamina then it will not able to run
        //until it is filled up overtime 

        //This will fill up the stamina overtime if the player didn't run
        if(!Player_Variable.isRunning && CurrentStamina >= 0)
        {
            CurrentStamina += 1f * Time.deltaTime;
        }

        //Stop the stamina if it's maxed out
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, MaxStamina);

        //Stamina Bar updates
        StaminaBar.fillAmount = CurrentStamina / MaxStamina;
    }
}
