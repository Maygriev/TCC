using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterEvent : MapEvent
{
    [SerializeField]
    private SpawnEnemy enemySpawn;
    [SerializeField]
    private ChangeScene sceneManager;

    public override void Activate() {
        enemySpawn.Spawn();
        sceneManager.loadNextScene("Battle");
    }
}
