using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        var interactObj = collision.gameObject.GetComponent<IInteractable>();
        if (interactObj != null)
        {
            interactObj.InterractWith();
        }
    }

}
