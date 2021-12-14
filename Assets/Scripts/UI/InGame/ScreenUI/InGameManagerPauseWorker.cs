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
        Time.timeScale = 0;
    }
    public void PressedMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        subscriber.UnSubscribeToPauseEvents();
    }

    public void PressedResume()
    {
        pauseView.gameObject.SetActive(false);
        subscriber.UnSubscribeToPauseEvents();
        Time.timeScale = 1f;
    }
}
