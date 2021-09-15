using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerLabel;
    private float time;
    private float interval = 0.01f;
    private WaitForSeconds timerInterval;
    private Coroutine timer;
    private bool timeTick = false;

    // Start is called before the first frame update
    void Start()
    {
        timerInterval = new WaitForSeconds(interval);
        timerLabel = GameObject.Find("TimerLabel").GetComponent<Text>();
    }

    private void Update()
    {
        if (timeTick)
        {
            time += Time.deltaTime;
            timerLabel.text = time.FloatToTimeString();
        }
    }

    public void StartTimer()
    {
        //timer = StartCoroutine(TimerTick());
        timeTick = true;
    }

    public void StopTimer()
    {
        //StopCoroutine(timer);
        timeTick = false;
    }

    private IEnumerator TimerTick()
    {
        while (true)
        {
            yield return timerInterval;
            time += interval;
            timerLabel.text = time.ToString();//.FloatToTimeString();
                                              //timer = StartCoroutine(TimerTick());
        }
    }

    public float GetTime()
    {
        return time;
    }

    public void Reset()
    {
        time = 0f;
    }
}
