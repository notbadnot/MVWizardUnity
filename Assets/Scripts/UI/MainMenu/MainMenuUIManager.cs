using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    //private
    private MMenuStartView startView;
    private MMenuView menuView;
    [Inject]
    private void Construct(MMenuView _menuView)
    {
        menuView = _menuView;
    }
    void Start()
    {
        startView = menuView.startMenu;
        SubscribeForStartEvents();
    }


    //========================================================
    private void SubscribeForStartEvents()
    {
        startView.PressStartEvent += StartView_PressStartEvent;
        startView.PressQuitEvent += StartView_PressQuitEvent;
    }



    private void StartView_PressStartEvent()
    {
        SceneManager.LoadScene("TrainingScene");
        UnSubscribeForStartEvents();
    }
    private void StartView_PressQuitEvent()
    {
        UnSubscribeForStartEvents();
        QuitHelper.Exit();
    }
    private void UnSubscribeForStartEvents()
    {
        startView.PressStartEvent -= StartView_PressStartEvent;
        startView.PressQuitEvent -= StartView_PressQuitEvent;
    }
    //===============================================================
}
