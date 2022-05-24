using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemyMenuItems : MonoBehaviour
{
    [SerializeField]
    private GameObject targetEnemyUnitPrefab;

    GameObject targetEnemyUnit;
    UnitStats unit;

    void Awake() {
        //Encontra o menu onde ficam os inimigos
        GameObject enemyUnitsMenu = GameObject.Find("EnemyInfo");

        unit = gameObject.GetComponent<UnitStats>();

        //Instancia os itens
        targetEnemyUnit = Instantiate(targetEnemyUnitPrefab, enemyUnitsMenu.transform);
        targetEnemyUnit.GetComponent<EnemyInfoStats>().MakeInfo(unit.GetSprite(), unit.GetLP(), unit.GetMaxLP(), unit.GetHC(), unit.GetMaxHC(), unit.GetShield());
        targetEnemyUnit.name = gameObject.GetComponent<UnitStats>().unitName;
    }

    void Start() {
        targetEnemyUnit.GetComponent<EnemyInfoStats>().UpdateTurn(unit.turnOrder);
    }

    void Update() {
        targetEnemyUnit.GetComponent<EnemyInfoStats>().UpdateInfo(unit.turnOrder, unit.GetLP(), unit.GetMaxLP(), unit.GetHC(), unit.GetMaxHC(), unit.GetShield());
        if(unit.GetComponent<UnitStats>().IsDead()) {
            KillInfo();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void KillInfo() {
        Destroy(targetEnemyUnit);
    }
}
