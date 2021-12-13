using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MMenuStartView : MonoBehaviour
{
    public event Action PressStartEvent;
    public event Action PressQuitEvent;

    public void OnPressStart()
    {
        PressStartEvent?.Invoke();
    }
    public void OnPressQuit()
    {
        PressQuitEvent?.Invoke();
    }
}
