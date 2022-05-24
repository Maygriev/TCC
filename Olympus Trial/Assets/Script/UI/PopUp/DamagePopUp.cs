using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private Color32 textColor;
    private TextMeshPro textMesh;
    private float moveYSpeed = 1f;
    public static DamagePopUp Create(Transform dmgPopUpPrefab, int damageAmount, Vector2 position, Color32 newColor) {
            Transform damagePopUpTransform = Instantiate(dmgPopUpPrefab, position, Quaternion.identity);
            DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
            damagePopUp.Setup(damageAmount, newColor);

            return damagePopUp;
        }

    public void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, Color32 newColor) {
        textColor = newColor;
        textMesh.SetText(damageAmount.ToString());
        textMesh.color = textColor;
    }

    public void Update() {
        transform.position += new Vector3(0,moveYSpeed) * Time.deltaTime;
    }
}
