using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionTypes;

public class ExtraEffect : MonoBehaviour
{
    public GameObject owner;

    [SerializeField] private ActionTarget actionTarget;
    [SerializeField] private ActionType actionType;
    [SerializeField] private ActionAOE actionAOE;
    [SerializeField] private AttackType attackType;
    [SerializeField] private StatScaling statScaling;
    [SerializeField] private float statMultiplier;
    [SerializeField] private List<TempEffect> tempEffects;

    public void Hit(GameObject target, GameObject curOwner) {
        owner = curOwner;
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

        CheckActionType(actionPower, enemyDefense, targetStats);
    }

    public void HitMultiple(List<GameObject> target, GameObject curOwner) {
        owner = curOwner;
        UnitStats ownerStats = owner.GetComponent<UnitStats>();
        List<UnitStats> targetStats = new List<UnitStats>();

        if(actionTarget == ActionTarget.SELF) {
            targetStats.Add(ownerStats);
        } else {
            if(target != null) {
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
    }

    public int CheckAttackType(UnitStats targetStats) {
        int enemyDefense = 0;
        switch(attackType){
            case AttackType.PHYSICAL:{
                enemyDefense = targetStats.GetArmor();
                break;
            }
            case AttackType.MAGICAL:{
                enemyDefense = targetStats.GetRes();
                break;
            }
        }
        return enemyDefense;
    }

    public int CheckStatScaling(UnitStats ownerStats) {
        int actionPower = 1;
        switch(statScaling){
            case StatScaling.HEALTH:{
                actionPower = Mathf.FloorToInt(statMultiplier * ownerStats.GetHealth());
                break;
            }
            case StatScaling.WILL:{
                actionPower = Mathf.FloorToInt(statMultiplier * ownerStats.GetWill());
                break;
            }
            case StatScaling.MIGHT:{
                actionPower = Mathf.FloorToInt(statMultiplier * ownerStats.GetMight());
                break;
            }
            case StatScaling.SKILL:{
                actionPower = Mathf.FloorToInt(statMultiplier * ownerStats.GetSkill());
                break;
            }
            case StatScaling.FAITH:{
                actionPower = Mathf.FloorToInt(statMultiplier * ownerStats.GetFaith());
                break;
            }
        }
        return actionPower;
    }

    public void CheckActionType(int actionPower, int enemyDefense, UnitStats targetStats) {
        switch(actionType){
            case ActionType.ATTACK:{
                if(!targetStats.IsDead()) {
                    actionPower = Mathf.Max(1, actionPower - enemyDefense);
                    targetStats.ReceiveDamage(actionPower);
                    break;
                }
                break;
            }
            case ActionType.HEAL:{
                if(!targetStats.IsDead()) {
                    targetStats.RegainLP(actionPower);
                    break;
                }
                break;
            }
            case ActionType.SHIELD:{
                if(!targetStats.IsDead()) {
                    targetStats.RechargeShield(actionPower);
                    break;
                }
                break;
            }
            case ActionType.BUFF:{
                if(!targetStats.IsDead()) {
                    foreach(TempEffect effect in tempEffects) {
                        Dictionary<string, int> nuEffect = effect.GetEffect();
                        List<string> keys = new List<string>(nuEffect.Keys);
                        int value;
                        foreach(string key in keys) {
                            value = System.Math.Abs(nuEffect[key]);
                            if(value != 0) {
                                nuEffect[key] = value * actionPower * -1;
                            }
                        }
                        TempEffect clone = Instantiate(effect);
                        clone.SetEffect(nuEffect);
                        targetStats.AddEffect(clone);
                        targetStats.UpdateTempEffects();
                    }
                    break;
                }
                break;
            }
            case ActionType.DEBUFF:{
                if(!targetStats.IsDead()) {
                    foreach(TempEffect effect in tempEffects) {
                        Dictionary<string, int> nuEffect = effect.GetEffect();
                        List<string> keys = new List<string>(nuEffect.Keys);
                        int value;
                        foreach(string key in keys) {
                            value = System.Math.Abs(nuEffect[key]);
                            if(value != 0) {
                                nuEffect[key] = value * actionPower * -1;
                            }
                        }
                        TempEffect clone = Instantiate(effect);
                        clone.SetEffect(nuEffect);
                        targetStats.AddEffect(clone);
                        targetStats.UpdateTempEffects();
                    }
                    break;
                }
                break;
            }
        }
    }

    public ActionAOE GetActionAOE() {
        return actionAOE;
    }
}
