using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GreenLightState
{
    RED = 1, // 闪红灯，两边绿灯亮
    GREEN = 2,    // 闪绿灯，两边绿灯亮
    RED_WITH_CHAOS = 3, // 闪红灯，两边绿灯不亮
    GREEN_WITH_CHAOS = 4, // 闪绿灯，两边绿灯不亮
}

public class RedGreenLightCtrl : MonoBehaviour
{
    public GameObject GreenLightMask;
    public GameObject RedLightMask;

    public GreenLightState GreenLightState = GreenLightState.GREEN;


    public void SetState(GreenLightState state)
    {
        switch (state)
        {
            case GreenLightState.RED:
                GetComponent<Image>().enabled = true;
                GreenLightMask.SetActive(true);
                RedLightMask.SetActive(false);
                break;
            case GreenLightState.GREEN:
                GetComponent<Image>().enabled = true;
                GreenLightMask.SetActive(false);
                RedLightMask.SetActive(true);
                break;
            case GreenLightState.RED_WITH_CHAOS:
                GetComponent<Image>().enabled = false;
                GreenLightMask.SetActive(true);
                RedLightMask.SetActive(false);
                break;
            case GreenLightState.GREEN_WITH_CHAOS:
                GetComponent<Image>().enabled = false;
                GreenLightMask.SetActive(true);
                RedLightMask.SetActive(false);
                break;
            default:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetState(GreenLightState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
