using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffect : MonoBehaviour {
    [SerializeField] private int duration, tempHealth, tempWill, tempMight, tempSkill, tempFaith, tempArmor, tempRes;

    public bool IsOver() {
        if (duration <= 0) {
            return true;
        } else {
            return false;
        }
    }

    public void UpdateDuration() {
        duration--;
    }

    public Dictionary<string, int> GetEffect() {
        Dictionary<string, int> res = new Dictionary<string, int>();
        res["Health"] = tempHealth;
        res["Will"] = tempWill;
        res["Might"] = tempMight;
        res["Skill"] = tempSkill;
        res["Faith"] = tempFaith;
        res["Armor"] = tempArmor;
        res["Res"] = tempRes;
        return res;
    }

    public void SetEffect(Dictionary<string, int> effect) {
        tempHealth = effect["Health"];
        tempWill = effect["Will"];
        tempMight = effect["Might"];
        tempSkill = effect["Skill"];
        tempFaith = effect["Faith"];
        tempArmor = effect["Armor"];
        tempRes = effect["Res"];
    }

    public TempEffect(Dictionary<string, int> effect) {
        tempHealth = effect["Health"];
        tempWill = effect["Will"];
        tempMight = effect["Might"];
        tempSkill = effect["Skill"];
        tempFaith = effect["Faith"];
        tempArmor = effect["Armor"];
        tempRes = effect["Res"];
    }
}