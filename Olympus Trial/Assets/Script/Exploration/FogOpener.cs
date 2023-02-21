using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOpener : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 destination;
    
    [SerializeField]
    private bool active = false;

    public void SetDestination(Vector3 target) {
        destination = target;
        active = true;
    }

    public void Update() {
        if(active){
            if(gameObject.transform.position == destination) {
                Expand();
                Destroy(gameObject, 2);
            } else {
                Vector3 heading = (destination - gameObject.transform.position).normalized;

                transform.position += heading * speed * Time.deltaTime;
            }
        }
    }

    public void Expand() {
        gameObject.transform.localScale = new Vector3(6f,6f,1f);
    }
}
