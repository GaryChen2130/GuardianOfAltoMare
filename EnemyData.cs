using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour {

    public int hp;
    public int atk;
    public int cd;
    public Image hp_bar;
    public Text show_cd;
    float ex_hp;
    int ex_cd;
    int cnt;
    bool down;

    private static float timer_f;
    private static int timer_i;

    // Use this for initialization
    void Start () {

        hp = GameControl.enemy_hp[0];
        atk = GameControl.enemy_atk[0];
        cd = GameControl.enemy_cd[0];
        ex_hp = (float)hp;
        ex_cd = cd;
        down = false;
        show_cd.text = "cd:" + ex_cd.ToString();
        transform.GetComponent<Image>().sprite = GameControl.enemy_img[0];
        transform.GetComponent<RectTransform>().sizeDelta = GameControl.enemy_size[0];

        cnt = 0;
        timer_f = 0f;
        timer_i = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if (timer_i > 1) {

            timer_f = 0f;
            timer_i = 0;
            ++cnt;

            hp = GameControl.enemy_hp[cnt];
            atk = GameControl.enemy_atk[cnt];
            cd = GameControl.enemy_cd[cnt];
            transform.GetComponent<Image>().sprite = GameControl.enemy_img[cnt];
            transform.GetComponent<RectTransform>().sizeDelta = GameControl.enemy_size[cnt];

            ex_hp = (float)hp;
            ex_cd = cd;
            down = false;
            show_cd.text = "cd:" + ex_cd.ToString();
            transform.GetComponent<Image>().color = Color.white;
            hp_bar.rectTransform.sizeDelta = new Vector2(100 * (ex_hp / (float)hp), 7);

        }

        if (down) {
            timer_f += Time.deltaTime;
            timer_i = (int)timer_f;
        }

	}

    public void Hurt(int damage) {

        ex_hp -= damage;

        if (ex_hp <= 0)
        {
            ex_hp = 0;
            show_cd.text = "";

            if (cnt >= GameControl.enemy_num - 1)
            {
                transform.gameObject.SetActive(false);
                GameControl.GameOver(true);
            }
            else
            {
                transform.GetComponent<Image>().color = Color.clear;
                transform.GetComponent<Image>().sprite = null;
                down = true;
            }

        }

        hp_bar.rectTransform.sizeDelta = new Vector2(100 * (ex_hp / (float)hp), 7);

    }

    public int Attack() {

        if (down || !transform.gameObject.activeSelf) return 0;
        ex_cd--;
        if (ex_cd == 0)
        {
            ex_cd = cd;
            show_cd.text = "cd:" + ex_cd.ToString();
            return atk;
        }
        else
        {
            show_cd.text = "cd:" + ex_cd.ToString();
            return 0;
        }
    }

}
