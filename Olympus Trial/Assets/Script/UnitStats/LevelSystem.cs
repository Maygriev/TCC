using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem {
    private int level;
    private int experience;
    private int expToNextLvl;

    public LevelSystem() {
        level = 1;
        experience = 0;
        expToNextLvl = 100;
    }

    public void AddExperience(int xp) {
        experience += xp;
        while(experience >= expToNextLvl) {
            level++;
            experience -= expToNextLvl;
            expToNextLvl = (expToNextLvl * 3)/2;
        }
    }

    public int GetLevel() {
        return level;
    }

    public int GetCurrentXP() {
        return experience;
    }

    public int GetXPtoNextLvl() {
        return expToNextLvl;
    }
}