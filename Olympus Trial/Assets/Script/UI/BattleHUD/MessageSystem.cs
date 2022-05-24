using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageSystem : MonoBehaviour{
    public GameObject messageField;

    public void ChangeMessage(string message) {
        messageField.GetComponent<TextMeshProUGUI>().text = message;
    }
}