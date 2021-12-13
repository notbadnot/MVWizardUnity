using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerOfPlayerModels : MonoBehaviour //Content models that connected to the player (wizard)
{

    public SpellModel spellModel;
    public StatsModel statsModel;

    void Start()
    {
        spellModel = new SpellModel(); 
        spellModel.SetStandartSpellsInSpellBook();
        statsModel = new StatsModel();
        statsModel.SetStandartStats();
    }


}
