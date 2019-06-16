using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : MonoBehaviour
{
    public static TimeMgr Instance;
    private void Awake()
    {
        Instance = this;
    }

    
    public void TimerOnce(float delay, Action callback)
    {
        StartCoroutine(Delay(delay, callback));
    }

    IEnumerator Delay(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);

        callback();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
