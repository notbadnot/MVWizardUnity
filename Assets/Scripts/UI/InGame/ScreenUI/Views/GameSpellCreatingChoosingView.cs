using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSpellCreatingChoosingView : MonoBehaviour
{
    public event Action Spell1ChoosedEvent;
    public event Action Spell2ChoosedEvent;
    public event Action Spell3ChoosedEvent;
    public event Action Spell4ChoosedEvent;

    public event Action PressedOKEvent;

    public void OnChoosedSpell1()
    {
        Spell1ChoosedEvent?.Invoke();
    }

    public void OnChoosedSpell2()
    {
        Spell2ChoosedEvent?.Invoke();
    }

    public void OnChoosedSpell3()
    {
        Spell3ChoosedEvent?.Invoke();
    }

    public void OnChoosedSpell4()
    {
        Spell4ChoosedEvent?.Invoke();
    }

    public void OnPressedOK()
    {
        PressedOKEvent?.Invoke();
    }


}
