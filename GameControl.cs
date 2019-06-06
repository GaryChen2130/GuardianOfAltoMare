using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static string outcome;

    // For player's team
    public Sprite[] character_sprite = new Sprite[6];
    public static Sprite[] st_character_sprite = new Sprite[6];
    public static Sprite[] member_img = new Sprite[6];
    public static int[] member_hp = new int[6];
    public static int[] member_atk = new int[6];
    public static int[] member_type = new int[6];

    // For enemy
    public Sprite[] enemy_sprite = new Sprite[10];
    public static Sprite[] st_enemy_sprite = new Sprite[10];
    public static Sprite[] enemy_img = new Sprite[10];
    public static int[] enemy_hp = new int[10];
    public static int[] enemy_atk = new int[10];
    public static int[] enemy_cd = new int[10];
    public static Vector2[] enemy_size = new Vector2[10];
    public static int enemy_num;

    void Start() {

        outcome = "";

        for(int i = 0;i < st_character_sprite.Length; ++i)
        {
            st_character_sprite[i] = character_sprite[i];
        }

        for (int i = 0; i < st_enemy_sprite.Length; ++i)
        {
            st_enemy_sprite[i] = enemy_sprite[i];
        }

    }

    public static void GameOver(bool win) {
        if (win) outcome = "You Win!";
        else outcome = "Gameover...";
        ChangeScene.ChangeToSceneOnDelay("result");
    }

    public static void LoadStage(int n) {

        if (n == 1)
        {

            enemy_num = 3;

            enemy_hp[0] = 3000;
            enemy_atk[0] = 50;
            enemy_cd[0] = 3;
            enemy_img[0] = st_enemy_sprite[1];
            enemy_size[0] = new Vector2(150, 100);

            enemy_hp[1] = 1500;
            enemy_atk[1] = 80;
            enemy_cd[1] = 2;
            enemy_img[1] = st_enemy_sprite[2];
            enemy_size[1] = new Vector2(110, 110);

            enemy_hp[2] = 2000;
            enemy_atk[2] = 30;
            enemy_cd[2] = 1;
            enemy_img[2] = st_enemy_sprite[3];
            enemy_size[2] = new Vector2(100, 100);

        }

        else if (n == 2)
        {
            enemy_num = 2;

            enemy_hp[0] = 3000;
            enemy_atk[0] = 30;
            enemy_cd[0] = 1;
            enemy_img[0] = st_enemy_sprite[4];
            enemy_size[0] = new Vector2(110, 100);

            enemy_hp[1] = 4000;
            enemy_atk[1] = 100;
            enemy_cd[1] = 2;
            enemy_img[1] = st_enemy_sprite[5];
            enemy_size[1] = new Vector2(100, 100);

        }

        else if (n == 3) {

            enemy_num = 3;

            enemy_hp[0] = 5000;
            enemy_atk[0] = 50;
            enemy_cd[0] = 1;
            enemy_img[0] = st_enemy_sprite[7];
            enemy_size[0] = new Vector2(100,100);

            enemy_hp[1] = 10000;
            enemy_atk[1] = 80;
            enemy_cd[1] = 2;
            enemy_img[1] = st_enemy_sprite[8];
            enemy_size[1] = new Vector2(100, 100);

            enemy_hp[2] = 8000;
            enemy_atk[2] = 200;
            enemy_cd[2] = 3;
            enemy_img[2] = st_enemy_sprite[9];
            enemy_size[2] = new Vector2(180, 100);

        }

    }

}
