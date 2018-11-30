using UnityEngine;
using UnityEngine.UI;

public class PotionSystem : MonoBehaviour {

    public Button[] pots;

    private int index;
    private Stats stat;

    private void Start()
    {
        index = 0;
        stat = gameObject.GetComponent<Stats>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {

            stat.Potion();
            pots[index].interactable = false;
            index++;
        }
	}
}
