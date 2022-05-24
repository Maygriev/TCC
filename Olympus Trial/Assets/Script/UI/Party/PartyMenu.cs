using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMenu : MonoBehaviour
{
    public GameObject partyMenu;
    public GameObject heroPrefab;
    public GameObject playerParty;
    private List<UnitStats> units;

    public void ShowMenu() {
        if(playerParty == null) {
            FindParty();
        }
        if(playerParty != null) {
            partyMenu.SetActive(true);
            MakeMenuItems();
        }
    }

    public void CloseMenu() {
        DestroyAllMenuItems();
        partyMenu.SetActive(false);
    }

    public bool FindParty() {
        playerParty = GameObject.Find("PlayerParty");
        if(playerParty != null) {
            return true;
        } else {
            return false;
        }
    }

    public void GetUnits() {
        units = new List<UnitStats>();
        foreach(Transform unit in playerParty.transform) {
            units.Add(unit.GetComponent<UnitStats>());
        }
    }

    void DestroyAllMenuItems() {
        int i = 0;
        foreach(Transform child in partyMenu.transform) {
            if(i < 2) {
                i++;
            } else {
                Destroy(child.gameObject);
            }
        }
    }

    public void MakeMenuItems() {
        GetUnits();

        foreach (UnitStats unit in units) {
            GameObject hero = Instantiate(heroPrefab, partyMenu.transform);

            hero.GetComponent<HeroPartyMenuInfo>().MakeInfo(unit, hero);
        }
    }
}
