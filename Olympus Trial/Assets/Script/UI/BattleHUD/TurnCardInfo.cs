using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCardInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject portrait, turnOrder;

    [SerializeField]
    private UnitStats unitStat;

    public void MakeCard(Sprite face, int order, UnitStats unit) {
        portrait.GetComponent<Image>().sprite = face;
        turnOrder.GetComponent<Text>().text = "" + order;
        unitStat = unit;
    }

    public void UpdateCard() {
        if(unitStat.IsDead()) {
            Destroy(gameObject);
        } else {
            turnOrder.GetComponent<Text>().text = "" + unitStat.turnOrder;
        }
    }
}
