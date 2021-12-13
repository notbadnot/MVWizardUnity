using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellModel 
{

    public enum ElementType
    {
        fire,
        lightning
            
    }

    public enum SpellType
    {
        wave,
        ball,
        //beam
    }


    public struct SpellInBook
    {
        public ElementType elementType;
        public SpellType spellType;
        public SpellInBook(ElementType element, SpellType spell)
        {
            this.elementType = element;
            this.spellType = spell;
        }
    }

    public SpellInBook[] SpellBook = new SpellInBook[4];

    public void SetStandartSpellsInSpellBook()
    {
        SpellBook = new SpellInBook[4] 
        { 
            new SpellInBook(ElementType.fire, SpellType.wave),
            new SpellInBook(ElementType.lightning, SpellType.wave),
            new SpellInBook(ElementType.fire, SpellType.ball),
            new SpellInBook(ElementType.lightning, SpellType.ball)  
        };
    }

}
