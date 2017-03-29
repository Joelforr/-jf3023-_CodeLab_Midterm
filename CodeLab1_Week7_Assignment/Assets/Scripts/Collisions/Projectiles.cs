using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {

    public string targetTag = "Player";

    void OnTriggerEnter2D(Collider2D target) {
        if(target.gameObject.tag == targetTag) {
            OnHit(target.gameObject);
            OnDestroy();
        }
    }
	
    protected virtual void OnHit(GameObject target) {

    }

    protected virtual void OnDestroy() {
        Destroy(gameObject);
    }
}
