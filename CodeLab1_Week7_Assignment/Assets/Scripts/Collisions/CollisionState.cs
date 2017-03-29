using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour {

    public LayerMask staticEnvCollisionMask;
    public bool standing;
    public Vector2 bottomPosition = Vector2.down;
    public float collisionRadius = 10f;
    public Color debugCollisionColor = Color.red;

    private LayerMask _collisionMask;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _collisionMask = staticEnvCollisionMask;
	}

    void FixedUpdate() {

        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle(pos, collisionRadius, _collisionMask);
    }

    void OnDrawGizmos() {
        Gizmos.color = debugCollisionColor;

        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;
  
        Gizmos.DrawWireSphere(pos, collisionRadius);

    }
}
