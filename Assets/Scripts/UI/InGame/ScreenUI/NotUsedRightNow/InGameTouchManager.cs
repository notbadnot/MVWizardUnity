using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InGameTouchManager : MonoBehaviour //hard to enter UI manager system
{/*
    [SerializeField] Wizard wizard;
    public bool leftStickTouched = false;
    public bool rightStickTouched = false;
    int leftStickTouchNumber;
    int rightStickTouchNumber;

    InGameUIManager uIManager;
    GameMainFrameView mainFrameView;
    TouchInputView touchView;
    [Inject]
    private void Construct(InGameUIManager _uIManager, GameMainFrameView _mainFrameView)
    {
        uIManager = _uIManager;
        mainFrameView = _mainFrameView;
    }
    void Start()
    {
        touchView = mainFrameView.touchView;

        touchView.InteractButtonTappedEvent += TouchView_InteractButtonTappedEvent;
        touchView.PauseButtonTappedEvent += TouchView_PauseButtonTappedEvent;
    }

    private void TouchView_PauseButtonTappedEvent()
    {
        uIManager.PressedPauseButton();
    }

    private void TouchView_InteractButtonTappedEvent()
    {
        uIManager.OpenSpellEditingMenu();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            //touchView.gameObject.SetActive(false);
        }else if (Input.touchCount > 0)
        {
            touchView.gameObject.SetActive(false);
        }
        if ( /*Input.touchCount > 0 *\/Input.GetMouseButton(0))
        {
            //if (Input.mousePosition)
            Debug.Log((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log((Vector2)touchView.leftStick.position);
            if (!leftStickTouched || )
        }
    }*/
}
