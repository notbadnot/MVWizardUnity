using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Wizard : Character
{
    [SerializeField]MovingComponent movingComponent;
    [SerializeField]WizardAnimationScript animationScript;
    [SerializeField] WizardSpellCaster spellCaster;
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] ManaComponent manaComponent;

    [SerializeField] AudioClip deathClip;

    public bool alive = true;

    public SpellModel spellModel;

    private HandlerOfPlayerModels handlerOfPlayerModels;
    private InGameUIManager uIManager;

    private SoundManager soundManager;

    [Inject]
    private void Construct(HandlerOfPlayerModels _handlerOfPlayerModels, InGameUIManager _uIManager, SoundManager _soundManager)
    {
        handlerOfPlayerModels = _handlerOfPlayerModels;
        uIManager = _uIManager;
        soundManager = _soundManager;

    }

    void Start()
    {
        healthComponent.ImDeadEvent += HealthComponent_ImDeadEvent;
        healthComponent.HealthChangedEvent += HealthComponent_HealthChangedEvent;
        spellCaster.manaComponent = manaComponent;
        manaComponent.NoManaEvent += ManaComponent_NoManaEvent;
        manaComponent.ManaChangedEvent += ManaComponent_ManaChangedEvent;
        healthComponent.health = handlerOfPlayerModels.statsModel.health;
        manaComponent.mana = handlerOfPlayerModels.statsModel.mana;
    }

    private void ManaComponent_ManaChangedEvent()
    {
        handlerOfPlayerModels.statsModel.mana = manaComponent.mana;
        uIManager.StatsModelManaChanged();
        
    }

    private void HealthComponent_HealthChangedEvent()
    {
        handlerOfPlayerModels.statsModel.health = healthComponent.health;
        uIManager.StatsModelHealthChanged();
    }

    private void ManaComponent_NoManaEvent()
    {
        StopAttacking();
    }

    private void HealthComponent_ImDeadEvent()
    {
        soundManager.SpawnSoundObject().Play(deathClip, gameObject.transform.position);
        alive = false;
        animationScript.PlayDeadAnimation();
    }

    public void GoForward()
    {
        movingComponent.Move(MovingComponent.Direction.Forward);
    }
    public void GoBack()
    {
        movingComponent.Move(MovingComponent.Direction.Back);
    }
    public void GoLeft()
    {
        movingComponent.Move(MovingComponent.Direction.Left);
    }
    public void GoRight()
    {
        movingComponent.Move(MovingComponent.Direction.Right);
    }
    public void Attack()
    {
        var whichspell = handlerOfPlayerModels.spellModel.SpellBook[handlerOfPlayerModels.statsModel.numberOfSpell];
        spellCaster.CastSpell(whichspell.elementType, whichspell.spellType);
        animationScript.StartAttackingAnimation();
    }
    public void StopAttacking()
    {
        spellCaster.StopCasting();
        animationScript.StopAttackingAnimation();
    }


    public void RotateToPoint(Vector2 point)
    {
        movingComponent.Rotate(point);
    }

    public void SetSpellNumber(int number)
    {
        handlerOfPlayerModels.statsModel.numberOfSpell = number;
        uIManager.StatsModelSpellNumberChanged();
    }


}
