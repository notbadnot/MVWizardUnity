using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Spell mySpell;
    void Start()
    {
        //======================Collider__Zone======================================
        var damagezone = gameObject.AddComponent<BoxCollider2D>();
        damagezone.isTrigger = true;

        var zoneSize = damagezone.size;
        zoneSize.x = 3.5f;
        //zoneSize.y = 25;
        zoneSize.y = 0.1f;
        damagezone.size = zoneSize;

        StartCoroutine(ZoneGrow(damagezone));

        var zoneOffset = damagezone.offset;
        //zoneOffset.y = 13;
        zoneOffset.y = 0;
        damagezone.offset= zoneOffset;
        //===============================================================

        //===================Particle_System_Zone=================================
        var spellPS = gameObject.GetComponent<ParticleSystem>();

        var spellMain = spellPS.main;
        spellMain.startLifetime = 2f;


        var spellEmision = spellPS.emission;
        spellEmision.rateOverTime = 200;
        

        var SpellVOL = spellPS.velocityOverLifetime;
        SpellVOL.enabled = true;
        
        var SpVOLy = SpellVOL.y;
        var SpVOLx = SpellVOL.x;

        SpVOLy.constantMin = 9;
        SpVOLy.constantMax = 10;

        SpVOLx.constantMax = 10;
        SpVOLx.constantMin = -10;
        
        SpellVOL.x = SpVOLx;
        SpellVOL.y = SpVOLy;



    }


    private IEnumerator ZoneGrow (BoxCollider2D boxCollider)
    {
        var zoneSize = boxCollider.size;
        var zoneOffset = boxCollider.offset;
        for (int i=0; i < 100; i++)
        {
            zoneSize.y = boxCollider.size.y + 0.025f;
            boxCollider.size = zoneSize;

            zoneOffset.y = boxCollider.offset.y + 0.013f;
            boxCollider.offset = zoneOffset;
            yield return new WaitForFixedUpdate();
        }


        yield return null;

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        var spellAceptor = collision.gameObject.GetComponent<SpellAceptor>();
        if (spellAceptor != null)
        {
            mySpell.CastSpellOnTarget(spellAceptor);

        }
    }


}
