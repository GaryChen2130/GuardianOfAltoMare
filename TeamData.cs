using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamData : MonoBehaviour {

    public int hp;
    public int atk;
    public int type;
    public int member_num;
    public Text atk_num;

	// Use this for initialization
	void Start () {

        // Set color of Text for attacking
        if (type == 0) atk_num.color = Color.red;
        else if (type == 1) atk_num.color = Color.blue;
        else if (type == 2) atk_num.color = Color.green;
        else if (type == 3) atk_num.color = Color.yellow;
        else if (type == 4) atk_num.color = Color.white;
        else if (type == 5) atk_num.color = Color.red;
        Attack(0);

        // Set member's data
        /*hp = GameControl.member_hp[member_num];
        atk = GameControl.member_atk[member_num];
        type = GameControl.member_type[member_num];
        transform.GetComponent<Image>().sprite = GameControl.member_img[member_num];*/

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Attack(int n) {
        if (n == 0) atk_num.text = "";
        else atk_num.text = (n * atk).ToString();
        return n * atk;
    }

}
