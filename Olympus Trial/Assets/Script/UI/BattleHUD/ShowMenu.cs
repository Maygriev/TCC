using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UIENUMS;

public class ShowMenu : MonoBehaviour
{
    public GameObject menuFrame;
    public GameObject buttonList;
    public GameObject battleSystem;
    public GameObject buttonPrefab;

    void Start() {
        menuFrame.SetActive(false);
    }

    public void ShowBasicActions() {
        GameObject currentHero = battleSystem.GetComponent<SelectUnit>().GetCurrentUnit();
        List<GameObject> actions = currentHero.GetComponent<HeroClass>().BasicActions;
        foreach(GameObject action in actions) {
            GameObject button = Instantiate(buttonPrefab, buttonList.transform);
            button.GetComponent<CallAction>().Fill(action, SelectionType.ACTION);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = action.GetComponent<BasicAction>().actionName;
        }
        menuFrame.SetActive(true);
    }

    public void ShowSpecialActions() {
        GameObject currentHero = battleSystem.GetComponent<SelectUnit>().GetCurrentUnit();
        List<GameObject> actions = currentHero.GetComponent<HeroClass>().SpecialActions;
        foreach(GameObject action in actions) {
            GameObject button = Instantiate(buttonPrefab, buttonList.transform);
            button.GetComponent<CallAction>().Fill(action, SelectionType.ACTION);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = action.GetComponent<SpecialAction>().actionName +
                " HC - " + action.GetComponent<SpecialAction>().GetHC();
        }
        menuFrame.SetActive(true);
    }

    public void ShowBasicEnemyTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach(GameObject enemy in enemies) {
            GameObject button = Instantiate(buttonPrefab, buttonList.transform);
            button.GetComponent<CallAction>().Fill(enemy, SelectionType.TARGET);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemy.GetComponent<UnitStats>().unitName;
        }
        menuFrame.SetActive(true);
    }

    public void ShowBasicAllyTarget() {
        GameObject[] allies = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach(GameObject ally in allies) {
            GameObject button = Instantiate(buttonPrefab, buttonList.transform);
            button.GetComponent<CallAction>().Fill(ally, SelectionType.TARGET);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ally.GetComponent<UnitStats>().unitName;
        }
        menuFrame.SetActive(true);
    }

    public void HideMenu() {
        menuFrame.SetActive(false);
    }
}
