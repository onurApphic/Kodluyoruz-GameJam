﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HappinessBarController : MonoBehaviour
{

    public Image happinessBarImage;
    public Image happinessBarOutImage;
    public Slider progressBar;
    public Image fillArea;
    
    public float happinessAmount; //Check To Do

    private float happinessTotal = 0;
    private float HappinessTotal 
    { 
        get 
        { 
            if(happinessTotal == 0) 
            {
                happinessTotal = LevelManager.Instance.CurrentLevel.happinessTotal;
            }
            return happinessTotal;
        } 
    }
    private float currentHappiness=20f;
    private float tweenDelay = 0.3f;

    private void OnEnable()
    {
        EventManager.OnOrderCompleted.AddListener(IncreaseHappiness);
        EventManager.OnOrderFailed.AddListener(DecreaseHappiness);
    }

    private void OnDisable()
    {
        EventManager.OnOrderCompleted.RemoveListener(IncreaseHappiness);
        EventManager.OnOrderFailed.RemoveListener(DecreaseHappiness);
    }

    private void Start()
    {
        UpdateHappinesBar();
    }

    public void IncreaseHappiness() 
    {
        currentHappiness += happinessAmount;
        UpdateHappinesBar();
        if (currentHappiness >= HappinessTotal)
        {
            EventManager.OnLevelSuccesed.Invoke();
        }        
    }

    public void DecreaseHappiness() 
    {
        Debug.Log("hi");
        currentHappiness -= happinessAmount;
        UpdateHappinesBar();
        if (currentHappiness <= 0)
        {
            EventManager.OnLevelFailed.Invoke();
        }       
    }
    /*
    private void UpdateHappinesBar()
    {        
        float ratio = currentHappiness / HappinessTotal;
        DOTween.To(() => happinessBarImage.fillAmount, (a) => happinessBarImage.fillAmount = a, ratio, tweenDelay).
            OnComplete(()=> {
                if (ratio >= 1) happinessBarImage.color = Color.green;               
                else if (ratio <= 0) happinessBarOutImage.color = Color.red;                
            });
    }
    */

    private void UpdateHappinesBar()
    {
        float ratio = currentHappiness / HappinessTotal;
        DOTween.To(() => progressBar.value, (a) => progressBar.value = a, ratio, tweenDelay).
            OnComplete(() => {                
                if (ratio >= 1) fillArea.color = Color.green;
                else if (ratio <= 0) fillArea.color = Color.red;                
            });
    }
}
