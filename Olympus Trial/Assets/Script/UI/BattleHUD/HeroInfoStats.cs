using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HeroInfoStats : MonoBehaviour
{
    [SerializeField]
    private GameObject turnNumber, heroPortrait, lpBar, hcBar, scBar;

    private Text lpText, hcText;

    public void MakeInfo(Sprite portrait, int curLP, int maxLP, int curHC, int maxHC, int curSC) {
        lpText = lpBar.transform.GetChild(1).gameObject.GetComponent<Text>();
        hcText = hcBar.transform.GetChild(1).gameObject.GetComponent<Text>();
        heroPortrait.GetComponent<Image>().sprite = portrait;
        UpdateLP(curLP, maxLP);
        UpdateHC(curHC, maxHC);
        UpdateShield(curSC, maxLP);
    }

    public void UpdateInfo(int turn, int curLP, int maxLP, int curHC, int maxHC, int curSC) {        
        UpdateTurn(turn);
        UpdateLP(curLP, maxLP);
        UpdateHC(curHC, maxHC);
        UpdateShield(curSC, maxLP);
    }

    public void UpdateTurn(int turn) {
        turnNumber.GetComponent<Text>().text = "" + turn;
    }

    public void UpdateLP(int curLP, int maxLP) {
        lpBar.GetComponent<Slider>().value = curLP;
        lpBar.GetComponent<Slider>().maxValue = maxLP;
        lpText.text = curLP + "/" + maxLP;
    }

    public void UpdateHC(int curHC, int maxHC) {
        hcBar.GetComponent<Slider>().value = curHC;
        hcBar.GetComponent<Slider>().maxValue = maxHC;
        hcText.text = curHC + "/" + maxHC;
    }

    public void UpdateShield(int curSC, int maxLP) {
        scBar.GetComponent<Slider>().value = curSC;
        scBar.GetComponent<Slider>().maxValue = maxLP;
    }
}
