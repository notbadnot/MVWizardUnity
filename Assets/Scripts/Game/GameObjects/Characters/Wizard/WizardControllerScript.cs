using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WizardControllerScript : Controller
{
    [SerializeField] KeyCode pause = KeyCode.Escape;
    [SerializeField] KeyCode forward = KeyCode.W;
    [SerializeField] KeyCode back = KeyCode.S;
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode first = KeyCode.Alpha1;
    [SerializeField] KeyCode second = KeyCode.Alpha2;
    [SerializeField] KeyCode third = KeyCode.Alpha3;
    [SerializeField] KeyCode fourth = KeyCode.Alpha4;
    [SerializeField] KeyCode interact = KeyCode.E;

    [SerializeField] Wizard wizard;


    [SerializeField] Camera mainCamera;

    InGameUIManager uIManager;

    [Inject]
    private void Construct(InGameUIManager _uIManager)
    {
        uIManager = _uIManager;
    }

    void Start()
    {
        mainCamera = Camera.main;
        wizard = GetComponent<Wizard>();
    }

    void Update()
    {

        if (Input.GetKeyDown(pause))
        {
            uIManager.PressedPauseButton();
        }

        if (!wizard.alive || Time.timeScale ==0)
        {
            return;
        }

        wizard.RotateToPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetKey(forward))
        {
            wizard.GoForward();
        }else if (Input.GetKey(back))
        {
            wizard.GoBack();
        }
        if (Input.GetKey(left))
        {
            wizard.GoLeft();
        }else if (Input.GetKey(right))
        {
            wizard.GoRight();
        }

        if (Input.GetMouseButton(0))
        {
            wizard.Attack();
        }
        else
        {
            if (Time.timeScale > 0)
            {
                wizard.StopAttacking();
            }
        }

        if (Input.GetKeyDown(first))
        {
            wizard.SetSpellNumber(0);
        } else if (Input.GetKeyDown(second))
        {
            wizard.SetSpellNumber(1);
        } else if (Input.GetKeyDown(third))
        {
            wizard.SetSpellNumber(2);
        }else if (Input.GetKeyDown(fourth))
        {
            wizard.SetSpellNumber(3);
        }
        if (Input.GetKeyDown(interact))
        {
            wizard.Interract();
        }



        //Debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
            else Time.timeScale = 1;

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            uIManager.OpenSpellEditingMenu();
        }

    }
}
