using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManagerSpellCreatingWorker
{
    public GameSpellCreatingView spellCreatingView;
    public GameSpellCreatingChoosingView choosingView;
    public GameSpellCreatingEditingView editingView;
    public InGameUIManagerSubscriber subscriber;

    public SpellModel spellModel;

    private int numberOfEditingSpell = 0;


    public void PressedEscapeButton()
    {
        if (editingView.gameObject.activeInHierarchy)
        {
            DeclineEditedSpell();
        } else if (choosingView.gameObject.activeInHierarchy)
        {
            QuitSpellEditor();
        }
    }

    public void OpenSpellEditor()
    {
        spellCreatingView.gameObject.SetActive(true);
        choosingView.gameObject.SetActive(true);
        subscriber.SubscribeToSpellChoosingEvents();
    }
    public void StartEditingSpell(int numberOfSpell)
    {
        numberOfEditingSpell = numberOfSpell;
        choosingView.gameObject.SetActive(false);
        subscriber.UnSubscribeToSpellChoosingEvents();
        editingView.gameObject.SetActive(true);
        subscriber.SubscribeToSpellEditingEvents();
        editingView.element.value = (int)spellModel.SpellBook[numberOfSpell].elementType;
        editingView.type.value = (int)spellModel.SpellBook[numberOfSpell].spellType;
    }
    public void QuitSpellEditor()
    {
        subscriber.UnSubscribeToSpellChoosingEvents();
        if (editingView.gameObject.activeInHierarchy)
        {
            editingView.gameObject.SetActive(false);
            subscriber.UnSubscribeToSpellEditingEvents();
        }
        choosingView.gameObject.SetActive(false);
        spellCreatingView.gameObject.SetActive(false);
    }


    public void DeclineEditedSpell()
    {
        editingView.gameObject.SetActive(false);
        subscriber.UnSubscribeToSpellEditingEvents();
        choosingView.gameObject.SetActive(true);
        subscriber.SubscribeToSpellChoosingEvents();
    }
    public void AcceptEditedSpell()
    {
        SpellModel.ElementType newElement = (SpellModel.ElementType)editingView.element.value; //fire = 0, lightning = 1
        SpellModel.SpellType newType = (SpellModel.SpellType)editingView.type.value;
        spellModel.SpellBook[numberOfEditingSpell] = new SpellModel.SpellInBook(newElement, newType);

        editingView.gameObject.SetActive(false);
        subscriber.UnSubscribeToSpellEditingEvents();
        choosingView.gameObject.SetActive(true);
        subscriber.SubscribeToSpellChoosingEvents();
    }

}
