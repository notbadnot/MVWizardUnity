using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManagerSubscriber // Probably will make easiyer to read, but not sure
{
    public GamePauseView pauseView;
    public GameSpellCreatingChoosingView choosingView;
    public GameSpellCreatingEditingView editingView;
    public GameStatsView statsView;

    public InGameUIManager uIManager; 


    //============================Pause Zone ============================
    public void SubscribeToPauseEvents()
    {
        pauseView.ResumeClickedEvent += PauseView_ResumeClickedEvent;
        pauseView.MainMenuClickedEvent += PauseView_MainMenuClickedEvent;
    }

    private void PauseView_ResumeClickedEvent()
    {
        uIManager.Pause_resume();
    }

    private void PauseView_MainMenuClickedEvent()
    {
        uIManager.Pause_MainMenu();
    }

    public void UnSubscribeToPauseEvents()
    {
        pauseView.ResumeClickedEvent += PauseView_ResumeClickedEvent;
        pauseView.MainMenuClickedEvent += PauseView_MainMenuClickedEvent;
    }

    //=======================Pause Zone ==========================
    //=======================SpellChooseing Zone ==========================

    public void SubscribeToSpellChoosingEvents()
    {
        choosingView.Spell4ChoosedEvent += ChoosingView_Spell4ChoosedEvent;
        choosingView.Spell3ChoosedEvent += ChoosingView_Spell3ChoosedEvent;
        choosingView.Spell2ChoosedEvent += ChoosingView_Spell2ChoosedEvent;
        choosingView.Spell1ChoosedEvent += ChoosingView_Spell1ChoosedEvent;
        choosingView.PressedOKEvent += ChoosingView_PressedOKEvent;
    }

    private void ChoosingView_PressedOKEvent()
    {
        uIManager.ChoosingSpell_OK();
    }

    private void ChoosingView_Spell1ChoosedEvent()
    {
        uIManager.ChoosingSpell_ChooseSpell(0);
    }

    private void ChoosingView_Spell2ChoosedEvent()
    {
        uIManager.ChoosingSpell_ChooseSpell(1);
    }

    private void ChoosingView_Spell3ChoosedEvent()
    {
        uIManager.ChoosingSpell_ChooseSpell(2);
    }

    private void ChoosingView_Spell4ChoosedEvent()
    {
        uIManager.ChoosingSpell_ChooseSpell(3);
    }

    public void UnSubscribeToSpellChoosingEvents()
    {
        choosingView.Spell4ChoosedEvent -= ChoosingView_Spell4ChoosedEvent;
        choosingView.Spell3ChoosedEvent -= ChoosingView_Spell3ChoosedEvent;
        choosingView.Spell2ChoosedEvent -= ChoosingView_Spell2ChoosedEvent;
        choosingView.Spell1ChoosedEvent -= ChoosingView_Spell1ChoosedEvent;
        choosingView.PressedOKEvent -= ChoosingView_PressedOKEvent;
    }

    //=====================Spell Choosing=================================================
    //=======================Spell Editing ==============================================
    
    public void SubscribeToSpellEditingEvents()
    {
        editingView.PressedAcceptEvent += EditingView_PressedAcceptEvent;
        editingView.PressedDenyedEvent += EditingView_PressedDenyedEvent;
    }


    private void EditingView_PressedAcceptEvent(int arg1, int arg2)
    {
        uIManager.EditingSpell_Accept();
    }

    private void EditingView_PressedDenyedEvent()
    {
        uIManager.EditingSpell_Decline();
    }

    public void UnSubscribeToSpellEditingEvents()
    {
        editingView.PressedAcceptEvent -= EditingView_PressedAcceptEvent;
        editingView.PressedDenyedEvent -= EditingView_PressedDenyedEvent;
    }

    //=================Spell Editing============================
    //===================Stats==================================

    public void SubscribeToStatsEvents()
    {
        statsView.Spell4ClickedEvent += StatsView_Spell4ClickedEvent;
        statsView.Spell3ClickedEvent += StatsView_Spell3ClickedEvent;
        statsView.Spell2ClickedEvent += StatsView_Spell2ClickedEvent;
        statsView.Spell1ClickedEvent += StatsView_Spell1ClickedEvent;
    }

    private void StatsView_Spell1ClickedEvent()
    {
        uIManager.Stats_ChangeSpellNumber(0);
    }

    private void StatsView_Spell2ClickedEvent()
    {
        uIManager.Stats_ChangeSpellNumber(1);
    }

    private void StatsView_Spell3ClickedEvent()
    {
        uIManager.Stats_ChangeSpellNumber(2);
    }

    private void StatsView_Spell4ClickedEvent()
    {
        uIManager.Stats_ChangeSpellNumber(3);
    }

    public void UnSubscribeToStatsEvents()
    {
        statsView.Spell4ClickedEvent -= StatsView_Spell4ClickedEvent;
        statsView.Spell3ClickedEvent -= StatsView_Spell3ClickedEvent;
        statsView.Spell2ClickedEvent -= StatsView_Spell2ClickedEvent;
        statsView.Spell1ClickedEvent -= StatsView_Spell1ClickedEvent;
    }

    //================Stats=============================

}
