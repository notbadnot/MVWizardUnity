using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManaComponent : MonoBehaviour
{
    [SerializeField] public float maxmana = 100;
    public float mana;

    public event Action NoManaEvent;
    public event Action ManaChangedEvent;

    private void Start()
    {
        mana = maxmana;
    }


    public void ChangeMana(float amout)
    {
        mana = Mathf.Max(mana + amout, 0);
        mana = Mathf.Min(mana, maxmana);
        ManaChangedEvent?.Invoke();
        if (mana <= 0)
        {
            NoManaEvent?.Invoke();
        }
    }

    public void SetMana(float amout)
    {
        mana = amout;
        ManaChangedEvent?.Invoke();
    }

    private void Update()
    {
        ChangeMana(6 * Time.deltaTime);
    }



}
