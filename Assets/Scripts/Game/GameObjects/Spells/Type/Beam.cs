using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour // Works very strange at the moment
{
    public Spell mySpell;
    void Start()
    {

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

        SpVOLy.constantMin = 160;
        SpVOLy.constantMax = 140;

        SpVOLx.constantMax = 2;
        SpVOLx.constantMin = -2;

        SpellVOL.x = SpVOLx;
        SpellVOL.y = SpVOLy;

        var spellCollision = spellPS.collision;
        spellCollision.enabled = true;
        spellCollision.type = ParticleSystemCollisionType.World;
        spellCollision.mode = ParticleSystemCollisionMode.Collision2D;
        spellCollision.dampen = 0.99f;
        spellCollision.bounce = 2;
    }



    private void Update()
    {

        var hit2D = Physics2D.Raycast(gameObject.transform.position, (Vector2)(transform.TransformPoint(Vector3.up)), 100000, 64);

        if (hit2D.collider != null)
        {


            var spellAceptor = hit2D.collider.gameObject.GetComponent<SpellAceptor>();
            if (spellAceptor != null)
            {
                mySpell.CastSpellOnTarget(spellAceptor);
            }
        }
    
    }



}
