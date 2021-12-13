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

    public bool alive = true;

    public SpellModel spellModel;

    private int spellNumber = 1;

    private HandlerOfPlayerModels handlerOfPlayerModels;
    private InGameUIManager uIManager;



    [Inject]
    private void Construct(HandlerOfPlayerModels _handlerOfPlayerModels)
    {
        handlerOfPlayerModels = _handlerOfPlayerModels;
        
    }

    void Start()
    {
        movingComponent = GetComponent<MovingComponent>(); //fix then
        animationScript = GetComponent<WizardAnimationScript>(); // fix then
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
        throw new System.NotImplementedException();
    }

    private void HealthComponent_HealthChangedEvent()
    {
        throw new System.NotImplementedException();
    }

    private void ManaComponent_NoManaEvent()
    {
        StopAttacking();
    }

    private void HealthComponent_ImDeadEvent()
    {
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
        Debug.Log(handlerOfPlayerModels.spellModel);
        var whichspell = handlerOfPlayerModels.spellModel.SpellBook[spellNumber];
        Debug.Log(whichspell);
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
        spellNumber = number;
    }




    // Update is called once per frame
    void Update()
    {
        
    }

}
