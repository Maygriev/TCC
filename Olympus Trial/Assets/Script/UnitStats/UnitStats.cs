using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class UnitStats : MonoBehaviour, IComparable
{
    [SerializeField] public Animator animator;

    [SerializeField] private Transform effectInfoPopUpPrefab;
    private float popUpTime = 1.5f;

    [SerializeField] private Sprite menuItemSprite;

    //Atributos Base
    [SerializeField] private int baseHealth, baseWill, baseMight, baseSkill, baseFaith;
    //LP = Life Points, HC = Heroic Charge
    private int modHealth, modWill, modMight, modSkill, modFaith, modLP, modHC, modArmor, modRes, modSpeed,
        tempHealth, tempWill, tempMight, tempSkill, tempFaith, tempArmor, tempRes;
    //SC = Shield Charge, o dano eh inflingido no escudo antes dos pontos de vida.
    [SerializeField] private int finalHealth, finalWill, finalMight, finalSkill, finalFaith, finalArmor, finalRes, finalSpeed, maxLP, maxHC;
    
    [SerializeField] private int curLP, curHC, curSC;
    //Booleana simples pra definir se esta vivo ou morto.
    private bool dead = false;
    [SerializeField]private List<TempEffect> tempEffects;
    //Variavel simples pra armazenar o resultado da iniciativa
    public int turnOrder, turnNumber;

    public string unitName;

    private bool hasLeveledUp;

    void Awake() {
        //Determina os valores iniciais dos atributos.
        modHealth = modWill = modMight = modSkill = modFaith = modLP = modHC = modArmor = modRes = modSpeed = 0;
        tempHealth = tempWill = tempMight = tempSkill = tempFaith = tempArmor = tempRes = 0;
        finalHealth = baseHealth + modHealth;
        finalWill = baseWill + modWill;
        finalMight = baseMight + modMight;
        finalSkill = baseSkill + modSkill;
        finalFaith = baseFaith + modFaith;
        finalArmor = (finalHealth/4) +  modArmor;
        finalRes = (finalWill/4) + modRes;
        finalSpeed = (finalSkill/4) + modSpeed;
        curLP = maxLP = (finalHealth * 4) + modLP;
        curHC = maxHC = (finalWill * 4) + modHC;
        curSC = 0;
        turnNumber = 0;
        hasLeveledUp = false;
        tempEffects = new List<TempEffect>();
    }

    public void UpdateTempEffects() {
        tempHealth = tempWill = tempMight = tempSkill = tempFaith = tempArmor = tempRes = 0;
        Dictionary<string, int> stats = new Dictionary<string, int>();
        for(int i = 0; i < tempEffects.Count; i++) {
            stats = tempEffects[i].GetEffect();
            if(tempEffects[i].IsOver()) {
                GameObject cleanup = tempEffects[i].gameObject;
                tempEffects.RemoveAt(i);
                i -= 1;
                Destroy(cleanup);
            } else {
                tempHealth += stats["Health"];
                tempWill += stats["Will"];
                tempMight += stats["Might"];
                tempSkill += stats["Skill"];
                tempFaith += stats["Faith"];
                tempArmor += stats["Armor"];
                tempRes += stats["Res"];
            }
        }
    }

    public void AddEffect(TempEffect effect) {
        tempEffects.Add(effect);
    }

    public void UpdateDuration() {
        foreach(TempEffect effect in tempEffects) {
            effect.UpdateDuration();
        }
    }

    public void EndEffects() {
        Dictionary<string, int> stats = new Dictionary<string, int>();
        List<TempEffect> finished = new List<TempEffect>();
        foreach(TempEffect effect in tempEffects) {
            stats = effect.GetEffect();
            tempHealth -= stats["Health"];
            tempWill -= stats["Will"];
            tempMight -= stats["Might"];
            tempSkill -= stats["Skill"];
            tempFaith -= stats["Faith"];
            tempArmor -= stats["Armor"];
            tempRes -= stats["Res"];
            finished.Add(effect);
        }
        foreach(TempEffect effect in finished)
        {
            tempEffects.Remove(effect);
            Destroy(effect.gameObject);
        }
    }

    public void UpdateStats() {
        finalHealth = baseHealth + modHealth;
        finalWill = baseWill + modWill;
        finalMight = baseMight + modMight;
        finalSkill = baseSkill + modSkill;
        finalFaith = baseFaith + modFaith;
        finalArmor = (finalHealth/4) +  modArmor;
        finalRes = (finalWill/4) + modRes;
        finalSpeed = (finalSkill/4) + modSpeed;
        curLP = maxLP = (finalHealth * 4) + modLP;
        curHC = maxHC = (finalWill * 4) + modHC;
        curSC = 0;
    }

    public void LevelUp(int health, int will, int might, int skill, int faith) {
        IncreaseHealth(health);
        IncreaseWill(will);
        IncreaseMight(might);
        IncreaseSkill(skill);
        IncreaseFaith(faith);
        UpdateStats();
        hasLeveledUp = true;
    }

    public void IncreaseHealth(int value) {
        baseHealth += value;
        UpdateStats();
    }

    public void IncreaseWill(int value) {
        baseWill += value;
        UpdateStats();
    }

    public void IncreaseMight(int value) {
        baseMight += value;
        UpdateStats();
    }

    public void IncreaseSkill(int value) {
        baseSkill += value;
        UpdateStats();
    }

    public void IncreaseFaith(int value) {
        baseFaith += value;
        UpdateStats();
    }

    public bool HasLeveledUp() {
        return hasLeveledUp;
    }

    public void SetLeveledUp() {
        hasLeveledUp = false;
    }

    public void BattleEnd() {
        curSC = 0;
        turnNumber = 0;
        EndEffects();
    }

    public void Rest() {
        curLP = maxLP;
        curHC = maxHC;
        dead = false;
        gameObject.tag = "PlayerUnit";
    }

    //Metodo simples pra calcular a iniciativa, quanto menor o resultado, melhor.
    public void CalculateTurnOrder(){
        turnOrder = Mathf.FloorToInt (UnityEngine.Random.Range(1,100) - finalSpeed);
    }

    public bool IsDead() {
        return dead;
    }

    public int CompareTo(object otherStats) {
        return turnOrder.CompareTo(((UnitStats)otherStats).turnOrder);
    }

    public void ReceiveDamage(int damage) {
        if(curSC > 0) {
            //Se tiver escudo, o dano eh infligido no escudo primeiro
            curSC -= damage;
            if(curSC < 0) {
                //Caso o dano seja superior a carga de escudo, o dano excedente vai para os pontos de vida, e o escudo se torna 0
                curLP += curSC;
                curSC = 0;
            }
        } else {
            //Caso nao tenha escudo, o dano vai direto nos pontos de vida.
            curLP -= damage;
        }
        
        //TODO: Chamar uma animation de dano recebido
        animator.Play("Hit");

        //Cria um popup de dano usando um prefab.
        DamagePopUp damageText = DamagePopUp.Create(effectInfoPopUpPrefab, Mathf.FloorToInt(damage), gameObject.transform.position, new Color32(231, 76, 60, 255));
        
        //Destroi o popup dps de um tempo.
        Destroy(damageText.gameObject, popUpTime);

        //Caso os pontos de vida sejam zerados ou negativos, torna o personagem em uma unidade morta.
        if(curLP <= 0) {
            curLP = 0;
            dead = true;
            //gameObject.tag = "DeadUnit";
        }
    }

    //Metodos Getters e Setters
    public int GetLP() {
        return curLP;
    }
    public int GetHC() {
        return curHC;
    }

    public int GetMaxLP() {
        return maxLP;
    }

    public int GetMaxHC() {
        return maxHC;
    }

    public int GetShield() {
        return curSC;
    }

    public int GetHealth() {
        return finalHealth + tempHealth;
    }

    public int GetWill() {
        return finalWill + tempWill;
    }

    public int GetMight() {
        return finalMight + tempMight;
    }

    public int GetSkill() {
        return finalSkill + tempSkill;
    }

    public int GetFaith() {
        return finalFaith + tempFaith;
    }

    public int GetArmor() {
        return finalArmor + tempArmor;
    }

    public int GetRes() {
        return finalRes + tempRes;
    }

    public int GetSpeed() {
        return finalSpeed;
    }

    public string GetUnitName() {
        return unitName;
    }

    public void SetUnitName(string newName) {
        unitName = newName;
    }

    public Sprite GetSprite() {
        return menuItemSprite;
    }

    public void SetSprite(Sprite sprite) {
        menuItemSprite = sprite;
    }

    public bool SpendHC(int hcCost) {
        if(curHC >= hcCost){
            curHC -= hcCost;
            return true;
        } else {
            return false;
        }
    }

    public bool CanSpendHC(int hcCost) {
        if(curHC >= hcCost){
            return true;
        } else {
            return false;
        }
    }

    public bool RegainHC(int hcRegained) {
        if(curHC <= maxHC){
            curHC += hcRegained;
            if(curHC >= maxHC) {
                curHC = maxHC;
            }
            return true;
        } else {
            return false;
        }
    }

    public bool RegainLP(int lpRegained) {
        if(curLP <= maxLP){
            curLP += lpRegained;
            if(curLP >= maxLP) {
                curLP = maxLP;
            }

            //Cria um popup de cura usando um prefab.
            DamagePopUp healText = DamagePopUp.Create(effectInfoPopUpPrefab, Mathf.FloorToInt(lpRegained), gameObject.transform.position, new Color32(46, 204, 113, 255));
            
            //Destroi o popup dps de um tempo.
            Destroy(healText.gameObject, popUpTime);

            return true;
        } else {
            return false;
        }
    }

    public bool RechargeShield(int scGained) {
        if(curSC <= maxLP){
            curSC += scGained;
            if(curSC >= maxLP) {
                curSC = maxLP;
            }

            //Cria um popup de escudo usando um prefab.
            DamagePopUp shieldText = DamagePopUp.Create(effectInfoPopUpPrefab, Mathf.FloorToInt(scGained), gameObject.transform.position, new Color32(52, 152, 219, 255));
            
            //Destroi o popup dps de um tempo.
            Destroy(shieldText.gameObject, popUpTime);

            return true;
        } else {
            return false;
        }
    }
}
