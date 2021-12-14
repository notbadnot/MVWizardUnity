using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] AudioClip clickClip;

    InGameUIManagerSubscriber subscriber;
    InGameManagerPauseWorker pauseWorker;
    InGameUIManagerSpellCreatingWorker spellCreatingWorker;
    InGameUIManagerStatsWorker statsWorker;

    GameMainFrameView mainFrameView;

    HandlerOfPlayerModels playerModelsHandler;
    SoundManager soundManager;

    [Inject]
    private void Construct(HandlerOfPlayerModels _playerModelsHandler, GameMainFrameView _mainFrameView, SoundManager _soundManager)
    {
        playerModelsHandler = _playerModelsHandler;
        mainFrameView = _mainFrameView;
        soundManager = _soundManager;
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
        pauseWorker.pauseView.gameObject.SetActive(false);
    }

    private void InitializeSpellCreatingWorker()
    {
        spellCreatingWorker.subscriber = subscriber;
        spellCreatingWorker.choosingView = mainFrameView.spellCreatingView.creatingChoosingView;
        spellCreatingWorker.spellCreatingView = mainFrameView.spellCreatingView;
        spellCreatingWorker.editingView = mainFrameView.spellCreatingView.creatingEditingView;
        spellCreatingWorker.spellModel = playerModelsHandler.spellModel;

        spellCreatingWorker.choosingView.gameObject.SetActive(false);
        spellCreatingWorker.editingView.gameObject.SetActive(false);
        spellCreatingWorker.spellCreatingView.gameObject.SetActive(false);
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
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
        pauseWorker.PressedResume();
    }

    public void Pause_MainMenu()
    {
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
        pauseWorker.PressedMainMenu();
    }

    //----pause part
    //----spell choosing part

    public void ChoosingSpell_ChooseSpell(int numberOfSpell)
    {
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
        spellCreatingWorker.StartEditingSpell(numberOfSpell);
    }

    public void ChoosingSpell_OK()
    {
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
        spellCreatingWorker.QuitSpellEditor();
    }

    //---spell chosing part
    //---spell editing part

    public void EditingSpell_Accept()
    {
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
        spellCreatingWorker.AcceptEditedSpell();
    }
    
    public void EditingSpell_Decline()
    {
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
        spellCreatingWorker.DeclineEditedSpell();
    }

    //---spell editing part
    //---stats part


    public void Stats_ChangeSpellNumber(int newSpellNumber)
    {
        soundManager.SpawnSoundObject().Play(clickClip, mainFrameView.gameObject.transform.position, true, false);
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

    public void CloseSpellEditingMenu()
    {
        spellCreatingWorker.QuitSpellEditor();
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
