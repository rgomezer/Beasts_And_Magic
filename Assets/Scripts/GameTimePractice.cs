using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameTimePractice : MonoBehaviour {

    public int iTime;
    public int timeLimited;
    int s;
    int hight;
    int width;
    Text timeText;
    bool isStart;
    

    DateTime now;
   

    // Use this for initialization
    void Start () {
        iTime = 0;
        timeText = GameObject.Find("GameTimeText").GetComponent<Text>();
        isStart = true;
        timeText.text = "00";
        iTime = 0;
        timeLimited = 90;
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            now = DateTime.Now;
            isStart = false;
        }
        TimeSpan t = DateTime.Now.Subtract(now);

        iTime = timeLimited - (int)t.TotalSeconds;

        if (iTime < 0)
        {
            
            
            isStart = true;
            Application.Quit();
        }

    }
    private void OnGUI()
    {

        //hud.offsetX =(int) ( -100)/2 ;

        string s = iTime.ToString("D2");
        timeText.text = s;

    }
}
