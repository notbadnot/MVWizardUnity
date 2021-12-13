using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WizardSpellCaster : MonoBehaviour
{
    [SerializeField] GameObject fireSpell;
    [SerializeField] GameObject lightningSpell;
    public ManaComponent manaComponent;


    private GameObject castingSpell;
    private bool castingNow = false;
    private float manaUse = 0;

    public float coolDown;


    private PrefabFactory prefabFactory;
    [Inject]
    private void Construct (PrefabFactory _prefabFactory)
    {
        prefabFactory = _prefabFactory;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (element == SpellModel.ElementType.fire)
            {
                localSpell = fireSpell;
            }else if (element == SpellModel.ElementType.lightning)
            {
                localSpell = lightningSpell;
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

                coolDown += 5;

            }
            else
            {

                //castingSpell = Instantiate(localSpell, gameObject.transform);

                castingSpell = prefabFactory.Spawn(localSpell, gameObject.transform);
                castingSpell.gameObject.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                castingSpell.layer = 8;
                Spell spellOfCastingSpell = castingSpell.GetComponent<Spell>();
                spellOfCastingSpell.element = element;
                spellOfCastingSpell.spellType = spellType;
                castingNow = true;

                if (spellType == SpellModel.SpellType.wave)
                {
                    var wave = castingSpell.AddComponent<Wave>();
                    wave.mySpell = spellOfCastingSpell;
                    manaUse = 18;
                }

            }




        }
    }

    // Update is called once per frame
    void Update()
    {
        coolDown = Mathf.Max(0, coolDown - 10f * Time.deltaTime);
        manaComponent.ChangeMana(-manaUse * Time.deltaTime);
    }
}
