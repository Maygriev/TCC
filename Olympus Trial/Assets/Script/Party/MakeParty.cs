using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeParty : MonoBehaviour
{
    public GameObject party;
    public GameObject heroAPrefab;
    public GameObject heroBPrefab;
    public GameObject heroCPrefab;
    public void CreateParty() {
        GameObject heroA = Instantiate(heroAPrefab, party.transform);
        GameObject heroB = Instantiate(heroBPrefab, party.transform);
        GameObject heroC = Instantiate(heroCPrefab, party.transform);

        GameObject uiStats = GameObject.Find("Hero A");
        UpdateHeroStats(heroA,uiStats);
        heroA.transform.position = new Vector2(3f,1.5f);
        heroA.name = "Hero A";
        uiStats = GameObject.Find("Hero B");
        UpdateHeroStats(heroB,uiStats);
        heroB.name = "Hero B";
        heroB.transform.position = new Vector2(1.5f,0f);
        uiStats = GameObject.Find("Hero C");
        UpdateHeroStats(heroC,uiStats);
        heroC.name = "Hero C";
        heroC.transform.position = new Vector2(3f,-1.5f);

    }

    public void UpdateHeroStats(GameObject hero, GameObject uiStats) {
        string newName = uiStats.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text;
        hero.GetComponent<UnitStats>().SetUnitName(newName);
    }
}
