using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpellAceptor : SpellAceptor
{
    [SerializeField] Skeleton skeleton;
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] GameObject fireEffectPrefab;
    [SerializeField] GameObject lightningEffectPrefab;
    [SerializeField] GameObject airEffectPrefab;

    private Coroutine firecoroutine;
    private GameObject fireEffect;

    private Coroutine lightningCoroutine;
    private GameObject lightningEffect;

    private Coroutine airCoroutine;
    private GameObject airEffect;

    public override void AcceptSpell(Spell spell)
    {
        if (spell.element == SpellModel.ElementType.fire)
        {
            AcceptFireSpell(spell);
        }else if (spell.element == SpellModel.ElementType.lightning)
        {
            AcceptLightningSpell(spell);
        }else if (spell.element == SpellModel.ElementType.air)
        {
            AcceptAirSpell(spell);
        }
        
    }
    private void AcceptFireSpell(Spell spell)
    {
        float[] modifiers = new float[2];
        modifiers = GetModifiers(spell.spellType);
        healthComponent.ChangeHealth(-30*modifiers[0]);
        if (modifiers[1] > Random.value)
        {
            StartFireEffect();
        }

    }
    private void StartFireEffect()
    {
        if (firecoroutine == null)
        {
            fireEffect = Instantiate(fireEffectPrefab, gameObject.transform);
            firecoroutine = StartCoroutine(FireEffect());
            
        }
    }
    IEnumerator FireEffect()
    {
        for (int i=0; i < 10; i++)
        {
            healthComponent.ChangeHealth(-3);
            yield return new WaitForSeconds(0.2f);
            
        }
        Destroy(fireEffect);
        firecoroutine = null;
        yield return null;

    }
    private void StopFireEffect()
    {
        if (firecoroutine != null)
        {
            StopCoroutine(firecoroutine);
            firecoroutine = null;
            Destroy(fireEffect);
        }
    }
    private void AcceptLightningSpell(Spell spell)
    {
        float[] modifiers = GetModifiers(spell.spellType);
        healthComponent.ChangeHealth(-15 * modifiers[0]);
        if (modifiers[1] > Random.value)
        {
            StartLightningEffect();
        }
    }
    private void StartLightningEffect()
    {
        if (lightningCoroutine == null)
        {
            lightningEffect = Instantiate(lightningEffectPrefab, gameObject.transform);
            lightningEffect.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            lightningCoroutine = StartCoroutine(LightningEffect());
        }
    }
    IEnumerator LightningEffect()
    {
        
        for (int i = 0; i < 7; i++)
        {
            Vector2 pushVector = new Vector2(Random.Range(-1,1), Random.Range(-1,1));
            rigidbody.AddForce(pushVector.normalized * 1300);

            yield return new WaitForSeconds(0.5f);

        }
        Destroy(lightningEffect);
        lightningCoroutine = null;
        yield return null;
    }
    private void StopLightningEffect()
    {
        if (lightningCoroutine != null)
        {
            StopCoroutine(lightningCoroutine);
            lightningCoroutine = null;
            Destroy(lightningEffect);
        }
    }


    private void AcceptAirSpell(Spell spell)
    {
        float[] modifiers = GetModifiers(spell.spellType);
        StopFireEffect();
        rigidbody.AddForce(((Vector2)(gameObject.transform.position - spell.gameObject.transform.position)).normalized * 7000 * modifiers[0]);
        rigidbody.AddTorque(Random.Range(-1440, 1440) * modifiers[0]);
        if (modifiers[1] > Random.value)
        {
            StartAirEffect();
        }
    }

    private void StartAirEffect()
    {
        if (airCoroutine == null)
        {
            airEffect = Instantiate(airEffectPrefab, gameObject.transform);
            airEffect.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            airCoroutine = StartCoroutine(AirEffect());
        }
    }

    IEnumerator AirEffect()
    {
        for (int i = 0; i < 35; i++)
        {
            Vector2 pushVector = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            rigidbody.AddTorque(Random.Range(-720, 720));
            rigidbody.AddForce(pushVector.normalized * 700);

            yield return new WaitForSeconds(0.1f);

        }
        Destroy(airEffect);
        airCoroutine = null;
        yield return null;
    }
    private void StopAirEffect()
    {
        if (airCoroutine != null)
        {
            StopCoroutine(airCoroutine);
            airCoroutine = null;
            Destroy(airEffect);
        }
    }
    private float[] GetModifiers(SpellModel.SpellType spelltype)
    {
        //0 - damage 
        //1 - EffectChanse
        float[] modifiers = new float[2];
        if (spelltype == SpellModel.SpellType.wave )
        {
            modifiers[0] = 0.05f;
            modifiers[1] = 0.001f;
        }else if (spelltype == SpellModel.SpellType.ball)
        {
            modifiers[0] = 1f;
            modifiers[1] = 0.4f;
        }else if (spelltype == SpellModel.SpellType.beam)
        {
            modifiers[0] = 0.2f;
            modifiers[1] = 0.01f;
        }


        return modifiers;
    }

}
