using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    [SerializeField] private string enemyName = "Slime";
    [SerializeField] private int enemyLevel = 1;
    [SerializeField] private int experience = 100;
    [SerializeField] private int drachma = 10;

    [SerializeField] private List<GameObject> basicActionsPrefabs;
    [SerializeField] private List<GameObject> specialActionsPrefabs;

    public List<GameObject> basicActions;
    public List<GameObject> specialActions;

    public string EnemyName {
        get{
            return enemyName;
        }
    }

    public int EnemyLevel {
        get{
            return enemyLevel;
        }
    }

    public int Experience {
        get{
            return experience;
        }
    }

    public int Drachma {
        get{
            return drachma;
        }
    }

    public List<GameObject> SpecialActions {
        get{
            return specialActions;
        }
    }

    public List<GameObject> BasicActions {
        get{
            return basicActions;
        }
    }

    public void Awake() {
        foreach(GameObject action in basicActionsPrefabs) {
            GameObject obj = Instantiate(action, gameObject.transform);
            basicActions.Add(obj);
        }
        foreach(GameObject action in specialActionsPrefabs) {
            GameObject obj = Instantiate(action, gameObject.transform);
            specialActions.Add(obj);
        }

        gameObject.GetComponent<UnitStats>().SetUnitName(gameObject.name);
    }
}
