using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI highScoreUI;

    public float currentTime;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    private void Start()
    {
        //highScoreUI.text = PlayerPrefs.GetFloat("HighScore").ToString();
        

        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethDecimal, "0.00");

        highScoreUI.text = hasFormat ? "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString(timeFormats[format]) : "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString();

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;


        SetTimerText();
    }

    private void SetTimerText()
    {
        timerUI.text = hasFormat ? "Time: " + currentTime.ToString(timeFormats[format]) : "Time: " + currentTime.ToString();
    }

    //Sets Best Score if larger than previous
    public void CheckHighScore()
    {
        if (currentTime < PlayerPrefs.GetFloat("HighScore", 10000))
        {
            PlayerPrefs.SetFloat("HighScore", currentTime);

        }
    }

    public void UpdateHighScoreText()
    {
        //highScoreUI.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 1000)}";
        highScoreUI.text = hasFormat ? "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString(timeFormats[format]) : "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethDecimal
}
