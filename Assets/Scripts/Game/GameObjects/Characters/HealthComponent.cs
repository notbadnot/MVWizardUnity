using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    public event Action ImDeadEvent;
    public event Action HealthChangedEvent;
    [SerializeField] public float maxHealth = 100; 
    public float health;

    private void Start()
    {
        health = maxHealth;
    }



    public void ChangeHealth( float amout)
    {
        health = Mathf.Max(0, health + amout);
        HealthChangedEvent?.Invoke();
        if (health <= 0)
        {
            Debug.Log("Get Dead");
            ImDeadEvent?.Invoke();
        }
    }

    public void SetHealth( float _health)
    {
        health = _health;
        HealthChangedEvent?.Invoke();
    }

}
