using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyEncounterPrefab;

    public void Spawn()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.name == "Battle") {
            Instantiate(enemyEncounterPrefab);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
