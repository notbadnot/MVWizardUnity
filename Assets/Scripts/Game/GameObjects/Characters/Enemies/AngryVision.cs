using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryVision : MonoBehaviour
{
    public Character myTarget = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var noticedCharacter = collision.GetComponent<Character>();
        if (noticedCharacter != null)
        {
            myTarget = noticedCharacter;
            
        }
    }

}
