using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpellEditingShelf : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject tooltip;
    private InGameUIManager uIManager;

    private bool interactingNow = false;
    private bool spellEditingNow = false;

    [Inject]
    private void Construct( InGameUIManager _uIManager)
    {
        uIManager = _uIManager;
    }

    public void InterractWith()
    {
        interactingNow = true;
        StartCoroutine(interactingCooldown());
    }
    IEnumerator interactingCooldown()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        interactingNow = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tooltip.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (interactingNow)
        {
            uIManager.OpenSpellEditingMenu();
            spellEditingNow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Wizard>() != null)
        {
            tooltip.SetActive(false);
            if (spellEditingNow)
            {
                uIManager.CloseSpellEditingMenu();
            }
        }
    }







}
