using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManagerStatsWorker
{
    public InGameUIManagerSubscriber subscriber;
    public GameStatsView statsView;

    public HandlerOfPlayerModels playerModelsHandler;


    public void InitializeFinnaly()
    {
        ChangeHealthBar();
        ChangeManaBar();
        HighlightSpellButton();
    }

    public void ChangeHealthBar()
    {
        var healthPart = playerModelsHandler.statsModel.health / 100;
        statsView.healthBar.localScale = new Vector3(healthPart, 1, 1);

    }
    public void ChangeManaBar()
    {
        var manaPart = playerModelsHandler.statsModel.mana / 100;
        statsView.manaBar.localScale = new Vector3(manaPart, 1, 1);
    }


    private void MakeButtonsOfOneColor()
    {
        var spell1colors = statsView.spell1.colors;
        spell1colors.normalColor = Color.white;
        statsView.spell1.colors = spell1colors;

        var spell2colors = statsView.spell2.colors;
        spell2colors.normalColor = Color.white;
        statsView.spell2.colors = spell2colors;

        var spell3colors = statsView.spell3.colors;
        spell3colors.normalColor = Color.white;
        statsView.spell3.colors = spell3colors;

        var spell4colors = statsView.spell4.colors;
        spell4colors.normalColor = Color.white;
        statsView.spell4.colors = spell4colors;
    }

    public void HighlightSpellButton()
    {
        MakeButtonsOfOneColor();
        if (playerModelsHandler.statsModel.numberOfSpell == 0)
        {
            var spell1colors = statsView.spell1.colors;
            spell1colors.normalColor = Color.green;
            statsView.spell1.colors = spell1colors;
        }else if (playerModelsHandler.statsModel.numberOfSpell == 1)
        {
            var spell2colors = statsView.spell2.colors;
            spell2colors.normalColor = Color.green;
            statsView.spell2.colors = spell2colors;
        }else if (playerModelsHandler.statsModel.numberOfSpell == 2)
        {
            var spell3colors = statsView.spell3.colors;
            spell3colors.normalColor = Color.green;
            statsView.spell3.colors = spell3colors;
        }else if (playerModelsHandler.statsModel.numberOfSpell == 3)
        {
            var spell4colors = statsView.spell4.colors;
            spell4colors.normalColor = Color.white;
            statsView.spell4.colors = spell4colors;
        }
    }

    public void ChangeSpellNumber(int newSpellNumber)
    {
        playerModelsHandler.statsModel.numberOfSpell = newSpellNumber;
        HighlightSpellButton();
    }

}
