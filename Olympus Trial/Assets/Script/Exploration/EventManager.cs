using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour, IClickable
{
    [SerializeField]
    private List<MapEvent> eventList;

    public void TriggerEvents() {
        foreach(MapEvent var in eventList) {
            var.Activate();
        }
    }

    public void Click() {
        TriggerEvents();
    }
}
