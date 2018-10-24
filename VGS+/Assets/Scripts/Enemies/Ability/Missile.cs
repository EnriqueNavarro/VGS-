using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField] private float range;
    [SerializeField] private Elements dmgType;
    [SerializeField] private int damage;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject target=null;
    [SerializeField] private Vector3 pos;
    [SerializeField] private float speed;
    [SerializeField] private bool defuse;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    private Vector3 scale = new Vector3(1, 1, 1);
    private bool once;
    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public Elements DmgType
    {
        get
        {
            return dmgType;
        }

        set
        {
            dmgType = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public Collider Col
    {
        get
        {
            return col;
        }

        set
        {
            col = value;
        }
    }

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Use this for initialization
    void Start () {
        Col.transform.localScale = new Vector3(Range, 2, Range);
        once = true;
        transform.localScale = scale;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Target!=null) {
            if(once) {
                pos = Target.transform.position;
            } else {
                transform.position = Vector3.MoveTowards(transform.position, pos, Speed);
                defuse = (Vector3.Distance(transform.position, pos) < 1);
            }
            once = false;
        }
	}
    public void DealDamage()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !defuse) {
            other.GetComponent<Stats>().damage(Damage, DmgType);
            Destroy(this.gameObject);
        }
    }
    
}
