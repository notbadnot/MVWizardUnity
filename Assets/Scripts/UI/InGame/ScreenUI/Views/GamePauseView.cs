using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePauseView : MonoBehaviour
{
    public event Action ResumeClickedEvent;
    public event Action MainMenuClickedEvent;

    public void OnResumeClicked()
    {
        ResumeClickedEvent?.Invoke();
    }
    public void OnMainMenuClicked()
    {
        MainMenuClickedEvent?.Invoke();
    }
}
