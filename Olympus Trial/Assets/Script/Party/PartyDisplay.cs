using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {  
        //Evita que o Objeto seja destruido ao mudar de cena.
        DontDestroyOnLoad(gameObject);

        //Invoca o metodo OnSceneLoaded sempre que mudar de cena.
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Deixa o estado inicial do objeto como inativo.
        gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Verifica qual a cena atual.
        if(scene.name == "Title") {
            //Caso seja a carregada a cena Title, destroi essa instancia do objeto, pois uma nova ja foi criada.
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        } else {
            //Caso esteja na cena Battle, o objeto se torna ativo, caso seja outra, se torna inativo.
            gameObject.SetActive(scene.name == "Battle" || scene.name == "Polis");
        }
    }
}
