using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMist : MonoBehaviour {
    [SerializeField] private GameObject mistT1;
    [SerializeField] private GameObject mistT2;
    [SerializeField] private GameObject mistT3;
    [SerializeField] private GameObject[] sliders= new GameObject[2];//0->Izquierda,1->Derecha
    [SerializeField] private GameObject filler;
    [SerializeField] private GameObject centerFill;
    [SerializeField] private GameObject megaCollider;
    [SerializeField] private int phase;
    public bool test;
    [SerializeField] private float freq;
    [SerializeField] private float increase;
    [SerializeField] private Elements dmgType;
    [SerializeField] private float tick;
    [SerializeField] private int damage;
    [SerializeField] private float maxScale;
    [SerializeField] private bool maxed;
    [SerializeField] private GameObject epicentre;
    [SerializeField] private int[] repetitions;
    [SerializeField] private float[,] timers;
    private GameObject[] ps;
    public List<GameObject> players;
    [SerializeField] float force;

    public bool Maxed
    {
        get
        {
            return maxed;
        }

        set
        {
            maxed = value;
        }
    }

    // Use this for initialization
    void Start () {
        mistT1.SetActive(false);
        mistT2.SetActive(false);
        mistT3.SetActive(false);
        filler.SetActive(false);
        centerFill.SetActive(false);
        megaCollider.SetActive(false);
        ps = GameObject.FindGameObjectsWithTag("Player");
        repetitions = new int[ps.Length];
        timers = new float[ps.Length,2];
    }
	public void ChangePhase(int t) {
        switch(t) {
            case 2:
                mistT1.SetActive(true);
                break;
            case 3:
                mistT1.SetActive(false);
                mistT2.SetActive(true);
                break;
            case 4:
                mistT2.SetActive(false);
                mistT3.SetActive(true);
                break;
            case 5:
                InvokeRepeating("GrowMist", freq, freq);
                filler.SetActive(true);
                Vector3 aux2 = sliders[0].GetComponent<Collider>().transform.localScale;
                sliders[0].GetComponent<Collider>().transform.localScale = new Vector3(aux2.x, aux2.y, aux2.z * 0.7f);
                aux2 = sliders[1].GetComponent<Collider>().transform.localScale;
                sliders[1].GetComponent<Collider>().transform.localScale = new Vector3(aux2.x, aux2.y, aux2.z * 0.7f);
                break;
        }
        phase = t;
    }
    private void GrowMist() {
        if (!Maxed)
        {
            Vector3 aux = sliders[0].transform.localScale;
            sliders[0].transform.localScale = new Vector3(aux.x, aux.y, aux.z + increase);
            aux = sliders[0].transform.position;
            sliders[0].transform.position = new Vector3(aux.x + (increase * 10), aux.y, aux.z);
            aux = sliders[1].transform.localScale;
            sliders[1].transform.localScale = new Vector3(aux.x, aux.y, aux.z + increase);
            Maxed = (aux.z > maxScale);
            aux = sliders[1].transform.position;
            sliders[1].transform.position = new Vector3(aux.x - (increase * 10), aux.y, aux.z);
            
            
        } else {
            centerFill.SetActive(true);
            megaCollider.SetActive(true);
        }
    }
    private void Update()
    {
        if (test)
        {
            ChangePhase(phase);
            test = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") addEnemy(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") removeEnemy(other);
    }
    public void removeEnemy(Collider other)
    {
        int j = 0;
        for (int i = 0; i < ps.Length; i++)
        {
            if (other.gameObject == ps[i])
            {
                repetitions[i]--;
                j = i;
            }
        }
        if(repetitions[j]<1) {
            repetitions[j] = 0;
            players.Remove(other.gameObject);
            other.gameObject.GetComponent<PlayerMovement>().Cc = false;
            int index = 0;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i] == other.gameObject) index = i;
            }
            timers[index, 0] = 0;
            timers[index, 1] = 0;
        }
        
        if(players.Count==0) {
            CancelInvoke();
            Debug.Log("canceling");
        }
    }
    public void addEnemy(Collider other)
    {
        int before = players.Count;
        if(!players.Contains(other.gameObject)) {
            players.Add(other.gameObject);
            other.gameObject.GetComponent<PlayerMovement>().Cc = true;
            
        }
        for(int i=0;i<ps.Length;i++) {
            if(other.gameObject==ps[i]) {
                repetitions[i]++;
            }
        }       
        if(before==0 && players.Count>0) {
            InvokeRepeating("DealDamage", tick, tick);
            Debug.Log("starting");
        }
    }
    public void DealDamage()
    {
        for (int i = 0; i < players.Count; i++)
        {
            int index = 0;
            for (int j = 0; j < ps.Length; j++)
            {
                if (ps[j] == players[i]) index = j;
            }
            if (timers[index, 0] == 0)
            {//0 es starting time, 1 es active time
                timers[index, 0] = Time.fixedTime;
            }
            else
            {
                timers[index, 1] = Time.fixedTime - timers[index, 0];
            }
            //Debug.Log("Starting time:" + timers[index, 0] + " Active time:" + timers[index, 1]);

        }
        foreach (GameObject enemy in players)
        {
            int index = 0;
            for (int j = 0; j < ps.Length; j++)
            {
                if (ps[j] == enemy) index = j;
            }
            enemy.GetComponent<Stats>().damage(damage, dmgType);
            Vector3 forceDirection = epicentre.transform.position - enemy.transform.position;
            Vector3 totalForce = forceDirection.normalized * force * timers[index, 1];
            enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
            enemy.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            enemy.GetComponent<Rigidbody>().AddForce(totalForce);
            Debug.Log("Force:" + totalForce);
        }
        
    }

}
