using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleSwitch : MonoBehaviour
{


    // Start is called before the first frame update
    public void TimeScaleOne()
    {
        Time.timeScale = 1;
    }
    public void TimeScaleZero() 
    {
        Time.timeScale = 0;
    }
}
