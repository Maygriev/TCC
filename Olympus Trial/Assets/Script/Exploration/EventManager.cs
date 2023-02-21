using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour, IClickable
{
    [SerializeField]
    private List<MapEvent> eventList;

    [SerializeField]
    private bool explored;
    [SerializeField]
    private bool active;

    [SerializeField]
    private GameObject fogOpener;
    
    [SerializeField]
    private List<GameObject> connections;

    public void Explore() {
        foreach(GameObject obj in connections) {
            GameObject dot = Instantiate(fogOpener, gameObject.transform.position, Quaternion.identity);
            dot.GetComponent<FogOpener>().SetDestination(obj.transform.position);
            EventManager map = obj.GetComponent<EventManager>();
            ShowMapMarker(map);
        }
    }

    public void ShowMapMarker(EventManager map) {
        map.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        map.SetActive();
    }

    public bool IsExplored() {
        return explored;
    }

    public bool IsActive() {
        return active;
    }

    public void SetActive() {
        active = true;
    }

    public void TriggerEvents() {
        foreach(MapEvent var in eventList) {
            var.Activate();
        }
    }

    public void Click() {
        
        if(!IsExplored() && IsActive()) {
            Explore();
            TriggerEvents();
            explored = true;
            active = false;
        }
    }
}
