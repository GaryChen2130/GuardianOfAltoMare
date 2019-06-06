using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour {

    public static bool draging;
    public static bool timeout;
    public static bool checking;
    private static float timer_f;
    private static int timer_i;

    // For turning stones
    public static int[,] type = new int[5, 6];
    public GameObject[] stone = new GameObject[30];
    public static GameObject[] turning_stone = new GameObject[30];
    public Sprite[] stone_img = new Sprite[6];
    public static Sprite[] turning_img = new Sprite[6];

    // For player's team
    public GameObject[] member = new GameObject[6];
    public static GameObject[] team_mem = new GameObject[6];
    public static int[] member_type = new int[6];
    public static int[] atk_cnt = new int[6];
    public Image hp_bar;
    int team_hp;
    public static float ex_team_hp;

    // For Enemy
    public GameObject enemy;
    public static GameObject st_enemy;

    // Use this for initialization
    void Start () {

        draging = false;
        timeout = false;
        timer_f = 0f;
        timer_i = 0;

        // Connect stone_img[] and turning_img[]
        for (int i = 0; i < 6; ++i) {
            turning_img[i] = stone_img[i];
        }

        // Connect stone[] and turning_stone[]
        for (int i = 0; i < 30; ++i) {
            int s = Random.Range(0, 6);
            turning_stone[i] = stone[i];
            turning_stone[i].GetComponent<Image>().sprite = stone_img[s];
            type[i / 6, i % 6] = s;
        }

        // Avoid continuous arrangement at beginning
        while (CheckStone.CountStone())
        {
            CheckStone.FillStone();
        }

        // Set team member
        team_hp = 0;
        for (int i = 0; i < 6; ++i) {
            member_type[i] = member[i].GetComponent<TeamData>().type;
            team_mem[i] = member[i];
            team_hp += team_mem[i].GetComponent<TeamData>().hp;
        }
        ex_team_hp = (float)team_hp;

        // Set enemy
        st_enemy = enemy;

    }
	
	// Update is called once per frame
	void Update () {

        if (timer_i > 10) {
            TimerCancel();
            draging = false;
            timeout = true;
            ResetAtk();
            if (CheckStone.CountStone())
            {
                CheckStone.FillStone();
                checking = true;
            }
            else {
                // Make enemy attack
                ex_team_hp -= st_enemy.GetComponent<EnemyData>().Attack();
                if (ex_team_hp <= 0)
                {
                    ex_team_hp = 0;
                    GameControl.GameOver(false);
                    for (int i = 0; i < 30; ++i) turning_stone[i].SetActive(false);
                }
            }
        
        }

        if (draging)
        {
            timer_f += Time.deltaTime;
            timer_i = (int)timer_f;
        }

        if (checking) {
            timer_f += Time.deltaTime;
            if (timer_i != (int)timer_f) {
                timer_i = (int)timer_f;
                if (CheckStone.CountStone())
                {
                    CheckStone.FillStone();
                }
                else {

                    TimerCancel();
                    checking = false;

                    // Recover HP
                    if (atk_cnt[5] >= 0) {
                        ex_team_hp += 10 * atk_cnt[5];
                        if (ex_team_hp > team_hp) ex_team_hp = team_hp;
                    }

                    // Make team member attack
                    for (int i = 0; i < 6; ++i) {
                        if (atk_cnt[member_type[i]] >= 0)
                        {
                            st_enemy.GetComponent<EnemyData>().Hurt(team_mem[i].GetComponent<TeamData>().Attack(atk_cnt[member_type[i]]));
                        }
                    }

                    // Make enemy attack
                    ex_team_hp -= st_enemy.GetComponent<EnemyData>().Attack();
                    if (ex_team_hp <= 0)
                    {
                        ex_team_hp = 0;
                        GameControl.GameOver(false);
                        for (int i = 0; i < 30; ++i) turning_stone[i].SetActive(false);
                    }

                }
            }
        }

        // Redraw the hp bar of player's team
        hp_bar.GetComponent<RectTransform>().sizeDelta = new Vector2(2*270 * (ex_team_hp / (float)team_hp), 15);

    }

    public static void TimerCancel() {
        timer_f = 0f;
        timer_i = 0;
    }

    public static void ResetAtk() {
        for (int i = 0; i < 6; ++i)
        {
            team_mem[i].GetComponent<TeamData>().Attack(0);
            atk_cnt[i] = 0;
        }
    }


}
