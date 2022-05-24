using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestParty : MonoBehaviour
{
    public void Rest() {
        GameObject party = GameObject.Find("PlayerParty");
        foreach(Transform unit in party.transform) {
            unit.gameObject.GetComponent<UnitStats>().Rest();
            unit.gameObject.SetActive(true);
        }
    }
}
