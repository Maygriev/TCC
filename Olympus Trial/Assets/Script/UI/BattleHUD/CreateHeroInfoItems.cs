using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateHeroInfoItems : MonoBehaviour
{
    [SerializeField]
    private GameObject heroInfoPrefab;

    GameObject heroUnit;
    UnitStats unit;

    Scene currentScene;

    void Start() {  
        //Invoca o metodo OnSceneLoaded sempre que mudar de cena.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        currentScene = SceneManager.GetActiveScene();
        if(scene.name == "Battle") {
            //Encontra o menu onde ficam os inimigos
            GameObject heroUnitsMenu = GameObject.Find("PlayerInfo");

            unit = gameObject.GetComponent<UnitStats>();

            //Instancia os itens
            heroUnit = Instantiate(heroInfoPrefab, heroUnitsMenu.transform);
            heroUnit.GetComponent<HeroInfoStats>().MakeInfo(unit.GetSprite(), unit.GetLP(), unit.GetMaxLP(), unit.GetHC(), unit.GetMaxHC(), unit.GetShield());
            heroUnit.name = gameObject.GetComponent<UnitStats>().unitName;
            heroUnit.GetComponent<HeroInfoStats>().UpdateTurn(unit.turnOrder);
        }
    }

    void Update() {
        if(currentScene.name == "Battle") {
            heroUnit.GetComponent<HeroInfoStats>().UpdateInfo(unit.turnOrder, unit.GetLP(), unit.GetMaxLP(), unit.GetHC(), unit.GetMaxHC(), unit.GetShield());
            if(unit.IsDead()) {
                KillInfo();
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
    }

    void KillInfo() {
        Destroy(heroUnit);
    }
}
