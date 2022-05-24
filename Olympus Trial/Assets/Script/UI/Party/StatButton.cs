using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIENUMS;

public class StatButton : MonoBehaviour
{
    [SerializeField] private HeroClass hero;
    [SerializeField] private StatName stat;
    [SerializeField] private GameObject unitObject;
    private HeroPartyMenuInfo unitInfo;
    [SerializeField] private bool startActive;

    public void SetHero(HeroClass heroClass, GameObject unitObj) {
        hero = heroClass;
        unitObject = unitObj;
        unitInfo = unitObject.GetComponent<HeroPartyMenuInfo>();
        Start();
    }

    void Start() {
        if(hero != null) {
            
            UpdateButton();
        } else {
            gameObject.SetActive(false);
        }
    }

    public void IncreaseStat() {
        if(hero.SpendStatPoints(stat)) {
            unitInfo.spentPoints++;
            unitInfo.UpdateStats();
        }
    }

    public void DecreaseStat() {
        if(unitInfo.spentPoints > 0) {
            hero.RegainStatPoints(stat);
            unitInfo.spentPoints--;
            unitInfo.UpdateStats();
        }
    }

    public void UpdateButton() {
        if(startActive) {
            gameObject.SetActive(hero.StatPoints > 0);
        } else {
            gameObject.SetActive(unitInfo.spentPoints > 0);
        }
    }
}
