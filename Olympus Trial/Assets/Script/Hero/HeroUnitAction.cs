using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionTypes;

public class HeroUnitAction : MonoBehaviour
{
    private GameObject currentAction;
    private GameObject currentTarget;

    private ActionOption currentActionOption;

    public void chooseAction(GameObject action, ActionOption actionOption) {
        currentAction = action;
        currentActionOption = actionOption;
    }

    public void chooseTarget(GameObject target) {
        currentTarget = target;
    }

    public void basicAction() {
        if(currentAction.GetComponent<BasicAction>().GetActionAOE() == ActionAOE.SINGLE) {
            currentAction.GetComponent<BasicAction>().Hit(currentTarget);
        } else {
            List<GameObject> tgt = new List<GameObject>();
            foreach(Transform child in currentTarget.transform.parent.transform) {
                tgt.Add(child.gameObject);
            }
            currentAction.GetComponent<BasicAction>().HitMultiple(tgt);
        }
        chooseAction(null, ActionOption.NOACTION);
        chooseTarget(null);
    }

    public void specialAction() {
        if(currentAction.GetComponent<SpecialAction>().GetActionAOE() == ActionAOE.SINGLE) {
            currentAction.GetComponent<SpecialAction>().Hit(currentTarget);
        } else {
            List<GameObject> tgt = new List<GameObject>();
            foreach(Transform child in currentTarget.transform.parent.transform) {
                tgt.Add(child.gameObject);
            }
            currentAction.GetComponent<SpecialAction>().HitMultiple(tgt);
        }
        chooseAction(null, ActionOption.NOACTION);
        chooseTarget(null);
    }

    public ActionOption GetActionOption() {
        return currentActionOption;
    }
}
