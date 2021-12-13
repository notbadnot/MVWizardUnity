using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Spell mySpell;


    void Start()
    {
        //======================Collider__Zone======================================
        var damagezone = gameObject.AddComponent<CircleCollider2D>();

        var zoneSize = damagezone.radius;
        damagezone.radius = 2f;

        //======================RigidBody__Zone=============================
        var rbody = gameObject.AddComponent<Rigidbody2D>();
        rbody.velocity = (rbody.GetRelativeVector(new Vector2(0,1))) * 15;
        StartCoroutine(DestroyingTimer());

        //===============================================================

        //===================Particle_System_Zone=================================
        var spellPS = gameObject.GetComponent<ParticleSystem>();

        var spellMain = spellPS.main;

        spellMain.simulationSpeed = 3;


        var spellEmision = spellPS.emission;
        spellEmision.rateOverTime = 200;


        var SpellVOL = spellPS.velocityOverLifetime;
        SpellVOL.enabled = true;



    }

    IEnumerator DestroyingTimer()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
        yield return null;
    }







    private void OnCollisionEnter2D(Collision2D collision)
    {
        var spellAceptor = collision.gameObject.GetComponent<SpellAceptor>();

        if (spellAceptor != null)
        {
            mySpell.CastSpellOnTarget(spellAceptor);
            
        }
        Destroy(gameObject);

    }

}
