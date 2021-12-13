using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModel
{
    public int numberOfSpell;
    public float health;
    public float mana;

    public void SetStandartStats()
    {
        numberOfSpell = 0;
        health = 100;
        mana = 100;
    }
}
