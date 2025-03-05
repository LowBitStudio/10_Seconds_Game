using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BlinkingEffect : MonoBehaviour
{
    //Var
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D PlayerCollider;
    [SerializeField] private float BlinkingInterval;
    [SerializeField] private float BlinkingDuration;
    private Color OriginalColor;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 7, false);    
    }

    void Start()
    {
        OriginalColor = sr.color;
    }

    public void BlinkingEffectActive()
    {
        StartCoroutine(BlinkingEffect());
    }

    private IEnumerator BlinkingEffect()
    {
        float elapsedTime = 0f;
        Physics2D.IgnoreLayerCollision(8, 7, true); //This will ignore player layers and obstacle

        while(elapsedTime < BlinkingDuration) //Do it when Elapsed time is below the set duration
        {
            Color newColor = sr.color;
            newColor.a = newColor.a == 1f ? 0f : 1f; //Toggle between sprite color alpha
            sr.color = newColor;

            yield return new WaitForSeconds(BlinkingInterval); //Interval between each blinks
            elapsedTime += BlinkingInterval;
        }
        sr.color = OriginalColor;
        Physics2D.IgnoreLayerCollision(8, 7, false);        
    }
}
