﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
            return;
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString("d1");
        string seconds = ((int)(t % 60)).ToString("d2");
        if (SceneManager.GetActiveScene().name == "HUB")
            if(System.DateTime.Now.Hour > 12)
                timerText.text = ((System.DateTime.Now.Hour) - 12).ToString() + ":" + (System.DateTime.Now.Minute).ToString("d2") +" PM";
            else
                timerText.text = (System.DateTime.Now.Hour).ToString() + ":" + (System.DateTime.Now.Minute).ToString("d2") + " AM";
        else
            timerText.text = minutes + ":" + seconds;
    }
    public void FinishTimer()
    {
        finished = true;
        timerText.color = Color.yellow;
    }
}