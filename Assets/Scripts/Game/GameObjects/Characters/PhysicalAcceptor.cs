using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PhysicalAcceptor : MonoBehaviour
{
    [SerializeField] HealthComponent health;

    [SerializeField] AudioClip hitClip;
    SoundManager soundManager;

    private bool canApplyDamage = true;



    [Inject]
    private void Construct(SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }


    private IEnumerator CanApplyDamageRestore()
    {
        yield return new WaitForSeconds(0.2f);
        canApplyDamage = true;
        yield return null;
    }
    public void AcceptDamage(float damage)
    {
        if (canApplyDamage)
        {
            canApplyDamage = false;
            health.ChangeHealth(-damage);
            soundManager.SpawnSoundObject().Play(hitClip, gameObject.transform.position);
            StartCoroutine(CanApplyDamageRestore());
        }
    }
}
