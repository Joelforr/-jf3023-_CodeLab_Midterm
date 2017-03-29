using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : Projectiles {

    public Vector2 intialVelocity = new Vector2(100,0);
    public float lifeTime = 2f;
    public Vector2 knockbackForce;

    private Rigidbody2D rb2D;
    public PlayerManager myPlayerManager;

    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {

        lifeTime = myPlayerManager.attackRange / 100;

        var startVelX = intialVelocity.x * transform.localScale.x;

        rb2D.velocity = new Vector2(startVelX, intialVelocity.y);
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            OnDestroy();
        }
	}

    protected override void OnHit(GameObject target) {

        var normalizdProjectileVel = new Vector2(rb2D.velocity.x, 0).normalized;
        var targetRB2D = target.gameObject.GetComponent<Rigidbody2D>();
        var targetManager = target.gameObject.GetComponent<PlayerManager>();
        targetRB2D.AddRelativeForce(knockbackForce * normalizdProjectileVel.x, ForceMode2D.Impulse);
 
        targetManager.health = targetManager.health - (int)((float)myPlayerManager.attackDamage * (float)(100f / (targetManager.defense + 100f)));
    }
}
