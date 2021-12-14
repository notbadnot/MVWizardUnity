using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameSpellCreatingEditingView : MonoBehaviour
{
    [SerializeField] public Dropdown element;
    [SerializeField] public Dropdown type;

    public event Action<int, int> PressedAcceptEvent;
    public event Action PressedDenyedEvent;

    public void OnPressedAccept()
    {
        PressedAcceptEvent?.Invoke(element.value, type.value);
    }
    public void OnPressedDeny()
    {
        PressedDenyedEvent?.Invoke();
    }

}
