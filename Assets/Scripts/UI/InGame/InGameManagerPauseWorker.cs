using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManagerPauseWorker
{
    public InGameUIManagerSubscriber subscriber;
    public GamePauseView pauseView;


    public void PressedEscapeButton()
    {
        PressedResume();
    }
    public void OpenPauseMenu()
    {
        pauseView.gameObject.SetActive(true);
        subscriber.SubscribeToPauseEvents();
    }
    public void PressedMainMenu()
    {
        SceneManager.LoadScene(1);
        subscriber.UnSubscribeToPauseEvents();
    }

    public void PressedResume()
    {
        pauseView.gameObject.SetActive(false);
        subscriber.UnSubscribeToPauseEvents();
    }
}
