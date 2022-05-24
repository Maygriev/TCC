using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UIENUMS;

public abstract class HeroClass : MonoBehaviour
{
    [SerializeField] protected string className;
    [SerializeField] protected string classDescription;
    [SerializeField] protected LevelSystem classLevel;
    [SerializeField] protected int healthLvlUp;
    [SerializeField] protected int willLvlUp;
    [SerializeField] protected int mightLvlUp;
    [SerializeField] protected int skillLvlUp;
    [SerializeField] protected int faithLvlUp;
    [SerializeField] protected int statPoints;
    [SerializeField] protected int skillPoints;
    [SerializeField] protected UnitStats unit;

    [SerializeField] private List<GameObject> lockedActionsPrefabs;
    [SerializeField] private List<GameObject> basicActionsPrefabs;
    [SerializeField] private List<GameObject> specialActionsPrefabs;

    [SerializeField] public List<GameObject> basicActions;
    [SerializeField] public List<GameObject> specialActions;


    public string ClassName {
        get{
            return className;
        }
    }

    public string ClassDescription {
        get{
            return classDescription;
        }
    }

    public LevelSystem ClassLevel {
        get{
            return classLevel;
        }
    }

    public void AddExperience(int xp) {
        int oldLevel = classLevel.GetLevel();
        classLevel.AddExperience(xp);
        while(oldLevel < classLevel.GetLevel()) {
            oldLevel++;
            statPoints += 2;
            skillPoints += 2;
            LevelUp();
        }
    }

    public void LevelUp() {
        unit.LevelUp(healthLvlUp, willLvlUp, mightLvlUp, skillLvlUp, faithLvlUp);
        LearnSkill();
    }

    public int GetLevel() {
        return classLevel.GetLevel();
    }

    public int GetCurrentXP() {
        return classLevel.GetCurrentXP();
    }

    public int GetXPtoNextLvl() {
        return classLevel.GetXPtoNextLvl();
    }

    public int HealthLvlUp {
        get{
            return healthLvlUp;
        }
    }
    public int WillLvlUp {
        get{
            return willLvlUp;
        }
    }
    public int MightLvlUp {
        get{
            return mightLvlUp;
        }
    }
    public int SkillLvlUp {
        get{
            return skillLvlUp;
        }
    }
    public int FaithLvlUp {
        get{
            return faithLvlUp;
        }
    }

    public int StatPoints {
        get{
            return statPoints;
        }
    }

    public int SkillPoints {
        get{
            return skillPoints;
        }
    }
    
    public bool SpendStatPoints (StatName stat) {
        if(statPoints > 0) {
            statPoints--;
            switch(stat) {
                case StatName.HEALTH:
                    unit.IncreaseHealth(1);
                    break;
                case StatName.WILL:
                    unit.IncreaseWill(1);
                    break;
                case StatName.MIGHT:
                    unit.IncreaseMight(1);
                    break;
                case StatName.SKILL:
                    unit.IncreaseSkill(1);
                    break;
                case StatName.FAITH:
                    unit.IncreaseFaith(1);
                    break;
            }
            return true;
        } else {
            return false;
        }
    }

    public void RegainStatPoints(StatName stat) {
        statPoints++;
        switch(stat) {
            case StatName.HEALTH:
                unit.IncreaseHealth(-1);
                break;
            case StatName.WILL:
                unit.IncreaseWill(-1);
                break;
            case StatName.MIGHT:
                unit.IncreaseMight(-1);
                break;
            case StatName.SKILL:
                unit.IncreaseSkill(-1);
                break;
            case StatName.FAITH:
                unit.IncreaseFaith(-1);
                break;
        }
    }

    public void SpendSkillPoints() {
        skillPoints--;
    }

    public void LearnSkill() {
        List<GameObject> learntActions = new List<GameObject>();
        foreach(GameObject action in lockedActionsPrefabs) {
            if(action.GetComponent<BasicAction>() != null){
                if(action.GetComponent<BasicAction>().GetRequiredLevel() <= GetLevel()) {
                    GameObject obj = Instantiate(action, gameObject.transform);
                    obj.GetComponent<BasicAction>().owner = gameObject;
                    basicActions.Add(obj);
                    learntActions.Add(action);
                }
            }
            if(action.GetComponent<SpecialAction>() != null){
                if(action.GetComponent<SpecialAction>().GetRequiredLevel() <= GetLevel()) {
                    GameObject obj = Instantiate(action, gameObject.transform);
                    obj.GetComponent<SpecialAction>().owner = gameObject;
                    specialActions.Add(obj);
                    learntActions.Add(action);
                }
            }
        }
        foreach(GameObject action in learntActions) {
            lockedActionsPrefabs.Remove(action);
        }
    }

    public List<GameObject> BasicActions {
        get{
            return basicActions;
        }
    }

    public List<GameObject> SpecialActions {
        get{
            return specialActions;
        }
    }

    public virtual void Awake() {
        foreach(GameObject action in basicActionsPrefabs) {
            GameObject obj = Instantiate(action, gameObject.transform);
            obj.GetComponent<BasicAction>().owner = gameObject;
            basicActions.Add(obj);
        }
        foreach(GameObject action in specialActionsPrefabs) {
            GameObject obj = Instantiate(action, gameObject.transform);
            obj.GetComponent<SpecialAction>().owner = gameObject;
            specialActions.Add(obj);
        }
        classLevel = new LevelSystem();
    }
}