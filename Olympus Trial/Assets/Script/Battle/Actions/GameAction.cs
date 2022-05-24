using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionTypes;

public class GameAction : MonoBehaviour
{
    public GameObject owner;

    public string actionName;
    public string actionDescription;

    [SerializeField] protected int levelRequired;
    [SerializeField] protected string actionAnimation;
    [SerializeField] protected ActionTarget actionTarget;
    [SerializeField] protected ActionType actionType;
    [SerializeField] protected ActionAOE actionAOE;
    [SerializeField] protected AttackType attackType;
    [SerializeField] protected StatScaling statScaling;
    [SerializeField] protected float statMultiplier;
    [SerializeField] protected int duration;
    [SerializeField] protected int heroicCharge;
    [SerializeField] protected List<ExtraEffect> extraEffects;
    [SerializeField] protected List<TempEffect> tempEffects;
    
    public virtual void Hit(GameObject target) {
    }

    public virtual void HitMultiple(List<GameObject> target) {
    }

    public ActionAOE GetActionAOE() {
        return actionAOE;
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
                                nuEffect[key] = value * actionPower * 1;
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

    public int GetRequiredLevel() {
        return levelRequired;
    }

    public void WaitThenExtra(ExtraEffect extra, GameObject target, float delay) {
        StartCoroutine(WaitThenExtraRoutine(extra, target, delay));
    }
    public void WaitThenExtraMultiple(ExtraEffect extra, List<GameObject> target, float delay) {
        StartCoroutine(WaitThenExtraMultipleRoutine(extra, target, delay));
    }

    private IEnumerator WaitThenExtraRoutine(ExtraEffect extra, GameObject target, float delay) {
        yield return new WaitForSeconds(delay);
        extra.Hit(target, owner);
    }

    private IEnumerator WaitThenExtraMultipleRoutine(ExtraEffect extra, List<GameObject> target, float delay) {
        yield return new WaitForSeconds(delay);
        extra.HitMultiple(target, owner);
    }

    public ActionTarget GetActionTarget() {
        return actionTarget;
    }
}
