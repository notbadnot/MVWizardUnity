using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WizardSpellCaster : MonoBehaviour
{
    [SerializeField] GameObject fireSpell;
    [SerializeField] GameObject lightningSpell;
    [SerializeField] GameObject airSpell;

    [SerializeField] AudioClip fireClip;
    [SerializeField] AudioClip lightningClip;
    public ManaComponent manaComponent;


    private GameObject castingSpell;
    private bool castingNow = false;
    private float manaUse = 0;

    public float coolDown;


    private PrefabFactory prefabFactory;
    private SoundManager soundManager;
    [Inject]
    private void Construct (PrefabFactory _prefabFactory, SoundManager _soundManager)
    {
        prefabFactory = _prefabFactory;
        soundManager = _soundManager;
    }

    
    public void StopCasting()
    {
        if (!castingNow)
        {
            return;
        }
        else
        {
            Destroy(castingSpell);
            castingNow = false;
            manaUse = 0;
        }
    }
    public void CastSpell(SpellModel.ElementType element, SpellModel.SpellType spellType)
    {

        if (!castingNow)
        {
            GameObject localSpell = fireSpell;
            AudioClip localClip = fireClip;
            if (element == SpellModel.ElementType.fire)
            {
                localSpell = fireSpell;
                localClip = fireClip;
            }else if (element == SpellModel.ElementType.lightning)
            {
                localSpell = lightningSpell;
                localClip = lightningClip;
            }else if (element == SpellModel.ElementType.air)
            {
                localSpell = airSpell;
            }

            if (spellType == SpellModel.SpellType.ball)
            {

                if (coolDown > 0)
                {
                    return;
                }
                if (manaComponent.mana < 15)
                {
                    return;
                }
                manaComponent.ChangeMana(-15);
                castingSpell = prefabFactory.Spawn(localSpell, gameObject.transform.position, gameObject.transform.rotation);
                castingSpell.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                castingSpell.layer = 9;
                Spell spellOfCastingSpell = castingSpell.GetComponent<Spell>();
                spellOfCastingSpell.element = element;
                spellOfCastingSpell.spellType = spellType;

                var ball = castingSpell.AddComponent<Ball>();
                ball.mySpell = spellOfCastingSpell;

                var sound = soundManager.SpawnSoundObject();
                sound.Play(localClip, castingSpell.transform.position, false, true);
                sound.transform.SetParent(castingSpell.transform);

                coolDown += 5;

            }
            else
            {


                castingSpell = prefabFactory.Spawn(localSpell, gameObject.transform);
                castingSpell.gameObject.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                castingSpell.layer = 8;
                Spell spellOfCastingSpell = castingSpell.GetComponent<Spell>();
                spellOfCastingSpell.element = element;
                spellOfCastingSpell.spellType = spellType;
                castingNow = true;

                var sound = soundManager.SpawnSoundObject();
                sound.Play(localClip, castingSpell.transform.position, false, true);
                sound.transform.SetParent(castingSpell.transform);

                if (spellType == SpellModel.SpellType.wave)
                {
                    var wave = castingSpell.AddComponent<Wave>();
                    wave.mySpell = spellOfCastingSpell;
                    manaUse = 18;
                } else if (spellType == SpellModel.SpellType.beam)
                {
                    var beam = castingSpell.AddComponent<Beam>();
                    beam.mySpell = spellOfCastingSpell;
                    manaUse = 25;
                }

            }
        }
    }

    void Update()
    {
        coolDown = Mathf.Max(0, coolDown - 10f * Time.deltaTime);
        manaComponent.ChangeMana(-manaUse * Time.deltaTime);
    }
}
