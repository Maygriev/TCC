using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionTypes;

public class BasicAction : GameAction
{
    public override void Hit(GameObject target) {
        UnitStats ownerStats = owner.GetComponent<UnitStats>();
        UnitStats targetStats;

        if(actionTarget == ActionTarget.SELF) {
            targetStats = ownerStats;
        } else {
            if(target != null) {
                targetStats = target.GetComponent<UnitStats>();
            } else {
                return;
            }
        }

        int actionPower = 1;
        int enemyDefense = 0;

        enemyDefense = CheckAttackType(targetStats);

        enemyDefense = Random.Range((enemyDefense/2), enemyDefense);

        actionPower = CheckStatScaling(ownerStats);

        owner.GetComponent<Animator>().Play(actionAnimation);

        CheckActionType(actionPower, enemyDefense, targetStats);

        ownerStats.RegainHC(heroicCharge);
        
        float delay = 0.5f;

        foreach(ExtraEffect extra in extraEffects) {
            WaitThenExtra(extra, target, delay);
            delay += 0.5f;
        }
    }

    public override void HitMultiple(List<GameObject> target) {
        UnitStats ownerStats = owner.GetComponent<UnitStats>();
        List<UnitStats> targetStats = new List<UnitStats>();

        if(actionTarget == ActionTarget.SELF) {
            targetStats.Add(ownerStats);
        } else {
            if(target.Count > 0) {
                foreach(GameObject unit in target) {
                    targetStats.Add(unit.GetComponent<UnitStats>());
                }
            } else {
                return;
            }
        }

        int actionPower = 1;
        int enemyDefense = 0;

        foreach(UnitStats unit in targetStats) {
            enemyDefense = CheckAttackType(unit);

            enemyDefense = Random.Range((enemyDefense/2), enemyDefense);

            actionPower = CheckStatScaling(ownerStats);

            CheckActionType(actionPower, enemyDefense, unit);
        }

        owner.GetComponent<Animator>().Play(actionAnimation);

        ownerStats.RegainHC(heroicCharge);

        float delay = 0.5f;
        
        foreach(ExtraEffect extra in extraEffects) {
            if(extra.GetActionAOE() == ActionAOE.ALL) {
                WaitThenExtraMultiple(extra, target, delay);
                delay += 0.5f;
            } else {
                WaitThenExtra(extra, target[0], delay);
                delay += 0.5f;
            }
        }
    }
}
