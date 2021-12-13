using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InGameUIManager : MonoBehaviour
{
    InGameUIManagerSubscriber subscriber;
    InGameManagerPauseWorker pauseWorker;
    InGameUIManagerSpellCreatingWorker spellCreatingWorker;
    InGameUIManagerStatsWorker statsWorker;

    GameMainFrameView mainFrameView;

    HandlerOfPlayerModels playerModelsHandler;

    [Inject]
    private void Construct(HandlerOfPlayerModels _playerModelsHandler, GameMainFrameView _mainFrameView)
    {
        playerModelsHandler = _playerModelsHandler;
        mainFrameView = _mainFrameView;
    }



    void Start()
    {
        subscriber = new InGameUIManagerSubscriber();
        pauseWorker = new InGameManagerPauseWorker() ;
        spellCreatingWorker = new InGameUIManagerSpellCreatingWorker();
        statsWorker = new InGameUIManagerStatsWorker();
        InitializeSubscriber();
        InitializePauseWorker();
        InitializeSpellCreatingWorker();
        InitializeStatsWorker();

        mainFrameView.pauseView.gameObject.SetActive(false);
        mainFrameView.spellCreatingView.gameObject.SetActive(false);
    }


    //=====================================Initialize Part ================================
    private void InitializeSubscriber()
    {
        subscriber.uIManager = this;
        subscriber.pauseView = mainFrameView.pauseView;
        subscriber.statsView = mainFrameView.statsView;
        subscriber.choosingView = mainFrameView.spellCreatingView.creatingChoosingView;
        subscriber.editingView = mainFrameView.spellCreatingView.creatingEditingView;
        subscriber.SubscribeToStatsEvents();
    }
    private void InitializePauseWorker()
    {
        pauseWorker.subscriber = subscriber;
        pauseWorker.pauseView = mainFrameView.pauseView;
    }

    private void InitializeSpellCreatingWorker()
    {
        spellCreatingWorker.subscriber = subscriber;
        spellCreatingWorker.choosingView = mainFrameView.spellCreatingView.creatingChoosingView;
        spellCreatingWorker.spellCreatingView = mainFrameView.spellCreatingView;
        spellCreatingWorker.editingView = mainFrameView.spellCreatingView.creatingEditingView;
        spellCreatingWorker.spellModel = playerModelsHandler.spellModel;
    }

    private void InitializeStatsWorker()
    {
        statsWorker.subscriber = subscriber;
        statsWorker.statsView = mainFrameView.statsView;
        statsWorker.playerModelsHandler = playerModelsHandler;
        statsWorker.InitializeFinnaly();
    }


    //=======================================Initialize Part ===============================
    //=======================================Subscrber Signal Reciving Part===========================


    //------pause part
    public void Pause_resume()
    {
        pauseWorker.PressedResume();
    }

    public void Pause_MainMenu()
    {
        pauseWorker.PressedMainMenu();
    }

    //----pause part
    //----spell choosing part

    public void ChoosingSpell_ChooseSpell(int numberOfSpell)
    {
        spellCreatingWorker.StartEditingSpell(numberOfSpell);
    }

    public void ChoosingSpell_OK()
    {
        spellCreatingWorker.QuitSpellEditor();
    }

    //---spell chosing part
    //---spell editing part

    public void EditingSpell_Accept()
    {
        spellCreatingWorker.AcceptEditedSpell();
    }
    
    public void EditingSpell_Decline()
    {
        spellCreatingWorker.DeclineEditedSpell();
    }

    //---spell editing part
    //---stats part


    public void Stats_ChangeSpellNumber(int newSpellNumber)
    {
        statsWorker.ChangeSpellNumber(newSpellNumber);
    }





    //==========================================Subscriber Signal Reciving Part ===============================
    //==========================================Game Signal Reciving======================================

    public void PressedPauseButton()
    {
        if (spellCreatingWorker.spellCreatingView.gameObject.activeInHierarchy)
        {
            spellCreatingWorker.PressedEscapeButton();
        } else if (pauseWorker.pauseView.gameObject.activeInHierarchy)
        {
            pauseWorker.PressedResume();
        }
        else
        {
            pauseWorker.OpenPauseMenu();
        }
    }

    public void OpenSpellEditingMenu()
    {
        spellCreatingWorker.OpenSpellEditor();
    }


    //==========Think more later=========
    public void StatsModelHealthChanged()
    {
        statsWorker.ChangeHealthBar();
    }
    public void StatsModelManaChanged()
    {
        statsWorker.ChangeManaBar();
    }
    public void StatsModelSpellNumberChanged()
    {
        statsWorker.HighlightSpellButton();
    }



}
