using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField] AudioClip clickClip;
    private MMenuStartView startView;
    private MMenuView menuView;
    private SoundManager soundManager;
    [Inject]
    private void Construct(MMenuView _menuView, SoundManager _soundManager)
    {
        menuView = _menuView;
        soundManager = _soundManager;
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
        UnSubscribeForStartEvents();
        soundManager.SpawnSoundObject().Play(clickClip, Vector3.zero, true, false);
        SceneManager.LoadScene("DemoScene");
    }
    private void StartView_PressQuitEvent()
    {
        soundManager.SpawnSoundObject().Play(clickClip, Vector3.zero, true, false);
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
