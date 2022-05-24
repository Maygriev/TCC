using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTypes : MonoBehaviour
{
    public enum ActionTarget{
        ENEMY,
        ALLY,
        SELF,
        BOTH
    }

    public enum ActionType{
        ATTACK,
        HEAL,
        SHIELD,
        BUFF,
        DEBUFF
    }

    public enum ActionAOE{
        SINGLE,
        ALL
    }

    public enum AttackType{
        PHYSICAL,
        MAGICAL
    }

    public enum StatScaling{
        HEALTH,
        WILL,
        MIGHT,
        SKILL,
        FAITH
    }

    public enum ActionOption {
        BASICACTION,
        SPECIALACTION,
        NOACTION
    }
}