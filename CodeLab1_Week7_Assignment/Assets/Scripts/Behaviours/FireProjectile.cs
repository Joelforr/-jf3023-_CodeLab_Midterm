using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : AbstractBehaviour {

    public float shootDelay = .5f;
    public GameObject projectilePrefab;
    public Vector2 firePosition = Vector2.zero;
    public Color debugColor = Color.yellow;
    public float debugRadius = 3f;
    public Vector2 fireKnockback = new Vector2(20f, 5f);
    private float timeElapsed = 0f;

    private PlayerManager myPM;



    // Use this for initialization
    void Start () {

        myPM = GetComponent<PlayerManager>();
        shootDelay = myPM.attackspeedDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(projectilePrefab != null) {
            var canFire = inputState.GetButtonValue(inputButtons[0]);

            if(canFire && timeElapsed > shootDelay) {
                CreateProjectile(CalculateFirePosition());
                rb2D.AddRelativeForce(fireKnockback * -(float)inputState.direction, ForceMode2D.Impulse);
                timeElapsed = 0f;
            }

            timeElapsed += Time.deltaTime;
        }
	}

    Vector2 CalculateFirePosition() {
        var pos = firePosition;
        pos.x *= (float)inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        return pos;
    }

    public void CreateProjectile (Vector2 pos) {
        var clone = Instantiate(projectilePrefab, pos, Quaternion.identity) as GameObject;
        clone.transform.localScale = transform.localScale;
        EnergyBlast cloneEnergyBlast = clone.GetComponent<EnergyBlast>();
        cloneEnergyBlast.myPlayerManager = myPM;
    }

    void OnDrawGizmos() {
        var pos = firePosition;

        if(inputState != null)
            pos.x *= (float)inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Gizmos.DrawWireSphere(pos, debugRadius);

    }
}
