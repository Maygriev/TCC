using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionTypes;

public class SelectUnit : MonoBehaviour
{
    private GameObject currentUnit;
    [SerializeField] private GameObject playerActions;
    [SerializeField] private GameObject menuFrame;
    [SerializeField] private GameObject enemyUnitsMenu;

    public void SelectCurrentUnit(GameObject unit) {
        currentUnit = unit;
        playerActions.SetActive(true);
    }

    public GameObject GetCurrentUnit() {
        return currentUnit;
    }

    public void SelectAction(GameObject action, ActionOption actionOption) {
        currentUnit.GetComponent<HeroUnitAction>().chooseAction(action, actionOption);

        playerActions.SetActive(false);
        menuFrame.SetActive(false);
    }

    public void SelectTarget(GameObject target) {
        currentUnit.GetComponent<HeroUnitAction>().chooseTarget(target);

        ActionOption option = currentUnit.GetComponent<HeroUnitAction>().GetActionOption();
        
        switch (option) {
            case ActionOption.BASICACTION:{
                currentUnit.GetComponent<HeroUnitAction>().basicAction();
                break;
            }
            case ActionOption.SPECIALACTION:{
                currentUnit.GetComponent<HeroUnitAction>().specialAction();
                break;
            }
        }

        playerActions.SetActive(false);
    }

    public void SelectSkip() {
        playerActions.SetActive(false);
        menuFrame.SetActive(false);
    }
}
