using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellModel.ElementType element;
    public SpellModel.SpellType spellType;

    public void CastSpellOnTarget (SpellAceptor spellAceptor)
    {
        spellAceptor.AcceptSpell(this);
    }
}
