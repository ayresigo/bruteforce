 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    /*Option #2: All-in-One Spells
     *
     *Instead of making one class that does damage, 
     *and another that stuns, and a third that pushes, 
     *make a single class that does everything. Every single 
     *spell in the whole game has a field for how much 
     *damage it does, and a separate field for how long it stuns,
     *and a separate field for how far it pushes the target. 
     *Most of those fields may be "zero" on most spells, 
     *but they're always there if you need them.
     *
     *Obviously, this could get unwieldy if you have a large number of specialized effects. 
     *But there are many computer games where this could be totally practical. 
     *And a few wacky special cases that are only used by one spell (or one family of spells) 
     *can still be handled by separate classes as long as they don't need to be combined with 
     *all the other effects in the game.     */

    public string id = System.Guid.NewGuid().ToString();
    public SkillName uniqueName;
    public string name;
    public Sprite icon;
    [Multiline] public string description;
    public Character.Rarity rarity;
    public SkillCategory category;
    public SkillType type;
    public TargetType targetType;
    public Triggers triggeredBy;
    public TriggerChance[] triggerChance;
    public AttModifier[] attModifier;    

    #region enums
    public enum SkillType
    {
        None,
        BasicAttack,
        Active,
        Passive,
        Trigger
    }
    public enum Triggers
    {
        None,
        BattleStart,
        BattleEnd,
        Updating,
        PickingTeams,
        Critical
    }
    public enum SkillName
    {
        None,
        BasicMeleeAttack,
        BasicRangedAttack
    }
    public enum SkillCategory
    {
        None,
        Ofensive,
        Defensive,
        Support
    }
    public enum TargetType
    {
        /*
         * Alterar no getTarget() caso haja alguma alteração!
         */

        None,
        Self,
        Caster,
        RandomAlly, 
        FirstAlly,
        SecondAlly,
        ThirdAlly,
        RandomAllies,
        FirstTwoAllies,
        LastTwoAllies,
        EdgeAllies,
        AllAllies,
        LowestHealthAlly,
        HighestHealthAlly,
        LowestAttackAlly,
        HighestAttackAlly,
        LowestDefenseAlly,
        HighestDefenseAlly,
        RandomEnemy,
        FirstEnemy,
        SecondEnemy,
        ThirdEnemy,
        RandomEnemies,
        FirstTwoEnemies,
        LastTwoEnemies,
        EdgeEnemies,
        AllEnemies,
        MirrorSideEnemy,
        LowestHealthEnemy,
        HighestHealthEnemy,       
        LowestAttackEnemy,
        HighestAttackEnemy,
        LowestDefenseEnemy,
        HighestDefenseEnemy
    }
    public enum Attributes
    {
        None,
        Health,
        Attack,
        Defense,
        Magic,
        Accuracy,
        Evade,
        CritChance,
        CritDamage
    }
    #endregion

    [System.Serializable]
    public class AttModifier
    {
        public Attributes attribute;
        public TargetType target;
        [Min(0)] public int intModifier = 0;
        [Min(0)] public float floatModifier = 0;
        public bool integerModifier = true;
        public bool positiveModifier = true;
    }

    [System.Serializable]
    public class TriggerChance
    {
        public TargetType target;
        public Triggers applyEffect;
        [Range(0f, 1f)]
        public float chance;
    }

    public abstract void Cast();
}
