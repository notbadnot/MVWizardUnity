using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStatsView : MonoBehaviour
{
    [SerializeField] public RectTransform healthBar;
    [SerializeField] public RectTransform manaBar;
    [SerializeField] public Button spell1;
    [SerializeField] public Button spell2;
    [SerializeField] public Button spell3;
    [SerializeField] public Button spell4;

    public event Action Spell1ClickedEvent;
    public event Action Spell2ClickedEvent;
    public event Action Spell3ClickedEvent;
    public event Action Spell4ClickedEvent;

    public void OnSpell1Clicked()
    {
        Spell1ClickedEvent?.Invoke();
    }
    public void OnSpell2Clicked()
    {
        Spell2ClickedEvent?.Invoke();
    }
    public void OnSpell3Clicked()
    {
        Spell3ClickedEvent?.Invoke();
    }
    public void OnSpell4Clicked()
    {
        Spell4ClickedEvent?.Invoke();
    }

}
