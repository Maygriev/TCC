using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionTypes;

public class EnemyUnitAI : MonoBehaviour
{
    [SerializeField]
    private string targetsTag;

    private EnemyAction enemyAction;
    private List<GameObject> basicActions;

    private GameObject target;
    private GameObject action;

    public MessageSystem messageSystem;

    void Awake(){
        enemyAction = gameObject.GetComponent<EnemyAction>();
        basicActions = enemyAction.BasicActions;
        messageSystem = GameObject.Find("BattleSystem").GetComponent<MessageSystem>();
        foreach(GameObject bAction in basicActions) {
            bAction.GetComponent<BasicAction>().owner = gameObject;
        }
    }

    private GameObject FindRandomTarget() {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetsTag);
        List<GameObject> aliveTargets = new List<GameObject>();

        foreach(GameObject target in possibleTargets) {
            if(!target.GetComponent<UnitStats>().IsDead()) {
                aliveTargets.Add(target);
            }
        }

        if(aliveTargets.Count > 0) {
            return aliveTargets[Random.Range(0, aliveTargets.Count)];
        } else {
            return null;
        }
    }

    private GameObject ChooseRandomAction(List<GameObject> actions) {
        if(actions.Count > 0) {
            return actions[Random.Range(0, actions.Count)];
        } else {
            return null;
        }
    }

    public void Act() {
        target = FindRandomTarget();
        action = ChooseRandomAction(basicActions);
        messageSystem.ChangeMessage(gameObject.GetComponent<UnitStats>().GetUnitName() + " usou " + action.GetComponent<BasicAction>().actionName);
        if(action.GetComponent<BasicAction>().GetActionAOE() == ActionAOE.SINGLE){
            action.GetComponent<BasicAction>().Hit(target);
        } else {
            List<GameObject> tgt = new List<GameObject>();
            foreach(Transform child in target.transform.parent.transform) {
                tgt.Add(child.gameObject);
            }
            action.GetComponent<BasicAction>().HitMultiple(tgt);
        }
    }
}
