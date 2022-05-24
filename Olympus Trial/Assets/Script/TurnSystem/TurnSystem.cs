using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject playerActions, enemyUnitsMenu;

    public GameObject enemyEncounter;

    private List<UnitStats> unitsStats;

    [SerializeField] private GameObject currentUnit;

    private Queue<UnitStats> turnOrder;

    private MessageSystem messageSystem;

    [SerializeField] private GameObject turnCardPrefab, turnOrderDisplay;

    private int xpReward;

    // Start is called before the first frame update
    void Start()
    {
        unitsStats = new List<UnitStats>();
        turnOrder = new Queue<UnitStats>();
        messageSystem = gameObject.GetComponent<MessageSystem>();
        xpReward = 0;

        //Adicionar unidades do jogador a lista
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits)
        {
            UnitStats currentStats = playerUnit.GetComponent<UnitStats>();
            currentStats.CalculateTurnOrder();
            unitsStats.Add(currentStats);
        }

        //Adicionar unidades inimigas a lista
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject enemyUnit in enemyUnits)
        {
            UnitStats currentStats = enemyUnit.GetComponent<UnitStats>();
            currentStats.CalculateTurnOrder();
            unitsStats.Add(currentStats);
            xpReward += enemyUnit.GetComponent<EnemyAction>().Experience;
        }

        unitsStats.Sort();

        foreach(UnitStats unit in unitsStats) {
            turnOrder.Enqueue(unit);
        }

        UpdateTurnOrderL(unitsStats);
        MakeCards(unitsStats);
        
        playerActions.SetActive(false);

        NextTurn();
    }
   
    public void NextTurn() {
        //Verifica quantos inimigos permanecem vivos
        GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");

        //Verifica se já matou todos os inimigos, caso sim, distribui recompensa e muda a cena;
        if(remainingEnemyUnits.Length == 0) {
            String levelUpText = RewardXP();
            messageSystem.ChangeMessage("Vitória, seu grupo recebeu " + xpReward + " Pontos de Experiência..." + levelUpText);
            StartCoroutine(WinRoutine());
            return;
        }

        //Verifica se o Jogador perdeu
        GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if(remainingPlayerUnits.Length == 0) {
            SceneManager.LoadScene("Title");
            return;
        }

        UpdateTurnOrderQ(turnOrder);

        UnitStats currentStats = turnOrder.Dequeue();
        
        currentStats.UpdateDuration();
        currentStats.UpdateTempEffects();
        
        CheckCards();
        
        if(!currentStats.IsDead()) {
            UpdateCard();
            currentUnit = currentStats.gameObject;
            turnOrder.Enqueue(currentStats);

            messageSystem.ChangeMessage("Turno de " + currentUnit.GetComponent<UnitStats>().GetUnitName());
            
            if(currentUnit.tag == "PlayerUnit") {
                StartCoroutine(PlayerTurn());
            } else {
                StartCoroutine(EnemyTurn());
            }
        } else {
            UpdateTurnOrderQ(turnOrder);
            CheckCards();
            NextTurn();
        }
    }

    public void UpdateTurnOrderL(List<UnitStats> unitStats) {
        int counter = 1;        
        foreach (UnitStats unit in unitsStats) {
            unit.turnOrder = counter++;
        }
    }

    public void UpdateTurnOrderQ(Queue<UnitStats> unitStats) {
        int counter = 1;
        foreach (UnitStats unit in unitStats) {
            if(!unit.IsDead()){
                unit.turnOrder = counter++;
            }
        }
    }

    public void MakeCards(List<UnitStats> unitStats) {
        GameObject obj = null;
        foreach(UnitStats unit in unitsStats) {
            obj = Instantiate(turnCardPrefab, turnOrderDisplay.transform);
            obj.GetComponent<TurnCardInfo>().MakeCard(unit.GetSprite(), unit.turnOrder, unit);
        }
        if(obj != null) {
            obj.transform.SetAsFirstSibling();
        }
    }

    public void CheckCards() {
        foreach(Transform card in turnOrderDisplay.transform) {
            card.gameObject.GetComponent<TurnCardInfo>().UpdateCard();
        }
    }

    public void UpdateCard() {
        GameObject card = turnOrderDisplay.transform.GetChild(0).gameObject;
        card.transform.SetAsLastSibling();
    }

    public String RewardXP() {
        GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        xpReward = xpReward / remainingPlayerUnits.Length;
        String levelUpText = "";
        foreach(GameObject player in remainingPlayerUnits) {
            player.GetComponent<HeroClass>().AddExperience(xpReward);
            if(player.GetComponent<UnitStats>().HasLeveledUp()) {
                levelUpText += "\n" + player.GetComponent<UnitStats>().unitName;
                levelUpText += " subiu para o nível " + player.GetComponent<HeroClass>().GetLevel() + ".";
                player.GetComponent<UnitStats>().SetLeveledUp();
            }
            player.GetComponent<UnitStats>().BattleEnd();
        }
        return levelUpText;
    }

    public IEnumerator WinRoutine() {
        List<KeyCode> keys = new List<KeyCode>();
        keys.Add(KeyCode.Space);
        keys.Add(KeyCode.Mouse0);
        yield return WaitForKeyPressRoutine(keys);
        SceneManager.LoadScene("Polis");
    }

    public IEnumerator PlayerTurn() {
        gameObject.GetComponent<SelectUnit>().SelectCurrentUnit(currentUnit.gameObject);
        yield return new WaitForSeconds(2.0f);
    }

    public IEnumerator EnemyTurn() {
        yield return new WaitForSeconds(2.0f);
        currentUnit.GetComponent<EnemyUnitAI>().Act();
        WaitThenNextTurn();
    }

    public void WaitThenNextTurn() {
       StartCoroutine(WaitThenNextTurnRoutine());
    }

    private IEnumerator WaitThenNextTurnRoutine() {
       yield return new WaitForSeconds(2.0f);
       NextTurn();
    }

    public IEnumerator WaitForKeyPress() {
        List<KeyCode> keys = new List<KeyCode>();
        keys.Add(KeyCode.Space);
        keys.Add(KeyCode.Mouse0);
        yield return WaitForKeyPressRoutine(keys);
    }

    private IEnumerator WaitForKeyPressRoutine(List<KeyCode> keys) {
       bool waiting = true;

        while(waiting) {
            foreach(KeyCode key in keys) {
                if(Input.GetKeyDown(key)) {
                    waiting = false;
                }

            }
            yield return null;
        }
    }
}
