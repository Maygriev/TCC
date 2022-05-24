using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIENUMS : MonoBehaviour 
{
    public enum SelectionType{
        ACTION,
        TARGET
    }
    
    public enum StatName {
        HEALTH,
        WILL,
        MIGHT,
        SKILL,
        FAITH,
        NONE
    }
}