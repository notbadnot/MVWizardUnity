using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAcceptor : MonoBehaviour
{
    [SerializeField] HealthComponent health;
    private bool canApplyDamage = true;


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
            StartCoroutine(CanApplyDamageRestore());
        }
    }
}
