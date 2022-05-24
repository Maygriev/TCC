using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIENUMS;
using static ActionTypes;

public class CallAction : MonoBehaviour
{
    private GameObject battleSystem;
    private GameObject selection;
    private SelectionType type;

    void Start()
    {
        battleSystem = GameObject.Find("BattleSystem");
    }

    public void Fill(GameObject newSelection, SelectionType newType) {
        selection = newSelection;
        type = newType;
    }

    public void Select() {
        if(type == SelectionType.ACTION) {
            battleSystem.GetComponent<SelectUnit>().SelectAction(selection, ActionOption.BASICACTION);
            DestroyAllChilds();
            if(selection.GetComponent<BasicAction>().GetActionTarget() == ActionTarget.ENEMY) {
                battleSystem.GetComponent<ShowMenu>().ShowBasicEnemyTarget();
            } else if(selection.GetComponent<BasicAction>().GetActionTarget() == ActionTarget.ALLY) {
                battleSystem.GetComponent<ShowMenu>().ShowBasicAllyTarget();
            } else {
                battleSystem.GetComponent<SelectUnit>().SelectTarget(selection);
                battleSystem.GetComponent<ShowMenu>().HideMenu();
                battleSystem.GetComponent<TurnSystem>().WaitThenNextTurn();
            }
        } else if(type == SelectionType.TARGET) {
            battleSystem.GetComponent<SelectUnit>().SelectTarget(selection);
            DestroyAllChilds();
            battleSystem.GetComponent<ShowMenu>().HideMenu();
            battleSystem.GetComponent<TurnSystem>().WaitThenNextTurn();
        }        
    }

    public void SelectSpecial() {
        if(type == SelectionType.ACTION) {
            battleSystem.GetComponent<SelectUnit>().SelectAction(selection, ActionOption.SPECIALACTION);
            DestroyAllChilds();
            if(selection.GetComponent<SpecialAction>().GetActionTarget() == ActionTarget.ENEMY) {
                if(selection.GetComponent<SpecialAction>().CanCast()) {
                    battleSystem.GetComponent<ShowMenu>().ShowBasicEnemyTarget();
                } else {
                    battleSystem.GetComponent<MessageSystem>().ChangeMessage("Precisa de mais Carga Heróica!");
                    battleSystem.GetComponent<ShowMenu>().ShowBasicActions();
                }
            } else if(selection.GetComponent<SpecialAction>().GetActionTarget() == ActionTarget.ALLY) {
                if(selection.GetComponent<SpecialAction>().CanCast()) {
                    battleSystem.GetComponent<ShowMenu>().ShowBasicAllyTarget();
                } else {
                    battleSystem.GetComponent<MessageSystem>().ChangeMessage("Precisa de mais Carga Heróica!");
                    battleSystem.GetComponent<ShowMenu>().ShowBasicActions();
                }
            } else {
                battleSystem.GetComponent<SelectUnit>().SelectTarget(selection);
                battleSystem.GetComponent<ShowMenu>().HideMenu();
                battleSystem.GetComponent<TurnSystem>().WaitThenNextTurn();
            }
        } else if(type == SelectionType.TARGET) {
            battleSystem.GetComponent<SelectUnit>().SelectTarget(selection);
            DestroyAllChilds();
            battleSystem.GetComponent<ShowMenu>().HideMenu();
            battleSystem.GetComponent<TurnSystem>().WaitThenNextTurn();
        }
    }

    public void SelectSkip() {
        battleSystem.GetComponent<SelectUnit>().SelectSkip();
        battleSystem.GetComponent<TurnSystem>().WaitThenNextTurn();
    }

    void DestroyAllChilds() {
        GameObject obj = gameObject.transform.parent.gameObject;
        foreach(Transform child in obj.transform) {
            Destroy(child.gameObject);
        }
    }
}
