using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPartyMenuInfo : MonoBehaviour
{
    public UnitStats owner;
    public HeroClass ownerClass;
    public GameObject heroPortrait;
    public GameObject heroName;
    public GameObject heroLP;
    public GameObject heroHC;
    public GameObject heroClass;
    public GameObject heroLevel;
    public GameObject heroXP;
    public GameObject heroNextXP;
    public GameObject heroStatPoints;
    public GameObject heroHealth;
    public GameObject heroHealthIncrease;
    public GameObject heroHealthDecrease;
    public GameObject heroWill;
    public GameObject heroWillIncrease;
    public GameObject heroWillDecrease;
    public GameObject heroMight;
    public GameObject heroMightIncrease;
    public GameObject heroMightDecrease;
    public GameObject heroSkill;
    public GameObject heroSkillIncrease;
    public GameObject heroSkillDecrease;
    public GameObject heroFaith;
    public GameObject heroFaithIncrease;
    public GameObject heroFaithDecrease;
    public GameObject heroSkillTreeButton;
    public GameObject heroSkillTree;
    public GameObject heroSkillPoints;

    public int spentPoints;

    public void MakeInfo(UnitStats unit, GameObject hero) {
        owner = unit;
        ownerClass = owner.gameObject.GetComponent<HeroClass>();
        
        heroPortrait.GetComponent<Image>().sprite = owner.GetSprite();
        heroName.GetComponent<TextMeshProUGUI>().text = owner.GetUnitName();
        heroClass.GetComponent<TextMeshProUGUI>().text = ownerClass.ClassName;
        heroLevel.GetComponent<TextMeshProUGUI>().text = "Level " + ownerClass.GetLevel();
        heroXP.GetComponent<TextMeshProUGUI>().text =  " " + ownerClass.GetCurrentXP();
        heroNextXP.GetComponent<TextMeshProUGUI>().text = " " + ownerClass.GetXPtoNextLvl();
        
        heroHealthIncrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroHealthDecrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroWillIncrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroWillDecrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroMightIncrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroMightDecrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroSkillIncrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroSkillDecrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroFaithIncrease.GetComponent<StatButton>().SetHero(ownerClass, hero);
        heroFaithDecrease.GetComponent<StatButton>().SetHero(ownerClass, hero);

        UpdateStats();
    }

    public void UpdateStats() {
        heroLP.GetComponent<TextMeshProUGUI>().text = "PV: " + owner.GetLP() + "/" + owner.GetMaxLP();
        heroHC.GetComponent<TextMeshProUGUI>().text = "CH: " + owner.GetHC() + "/" + owner.GetMaxHC();

        heroStatPoints.GetComponent<TextMeshProUGUI>().text = " " + ownerClass.StatPoints;
        heroHealth.GetComponent<TextMeshProUGUI>().text = " " + owner.GetHealth();
        heroWill.GetComponent<TextMeshProUGUI>().text = " " + owner.GetWill();
        heroMight.GetComponent<TextMeshProUGUI>().text = " " + owner.GetMight();
        heroSkill.GetComponent<TextMeshProUGUI>().text = " " + owner.GetSkill();
        heroFaith.GetComponent<TextMeshProUGUI>().text = " " + owner.GetFaith();

        heroSkillPoints.GetComponent<TextMeshProUGUI>().text = "Pontos Disponíveis:  " + ownerClass.SkillPoints;

        UpdateButtons();
    }

    public void UpdateButtons() {
        heroHealthIncrease.GetComponent<StatButton>().UpdateButton();
        heroHealthDecrease.GetComponent<StatButton>().UpdateButton();
        heroWillIncrease.GetComponent<StatButton>().UpdateButton();
        heroWillDecrease.GetComponent<StatButton>().UpdateButton();
        heroMightIncrease.GetComponent<StatButton>().UpdateButton();
        heroMightDecrease.GetComponent<StatButton>().UpdateButton();
        heroSkillIncrease.GetComponent<StatButton>().UpdateButton();
        heroSkillDecrease.GetComponent<StatButton>().UpdateButton();
        heroFaithIncrease.GetComponent<StatButton>().UpdateButton();
        heroFaithDecrease.GetComponent<StatButton>().UpdateButton();
    }
}
