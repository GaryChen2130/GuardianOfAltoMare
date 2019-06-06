using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckStone : MonoBehaviour {

    public static int[,] record = new int[5,6];
    public GameObject sound_effect = null;
    public static GameObject st_sound_effect;

    void Start() {
        st_sound_effect = sound_effect;
    }

    public static void init() {
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 6; ++j) record[i, j] = 1;
        }
    }

    public static bool CountStone() {

        bool has_remove = false;

        // Count stone of same type in a row
        init();
        for (int i = 0; i < 5; ++i) {
            for (int j = 1; j < 6; ++j) {
                if (GameData.type[i, j] == GameData.type[i, j - 1]) {
                    record[i, j] = record[i,j - 1] + 1;
                }
            }
        }

        // Remove the stones who appear continuously in a row
        for (int i = 4; i >= 0; --i)
        {
            for (int j = 5; j >= 0; --j)
            {
                if (record[i, j] >= 3)
                {
                    int k = j - 1;
                    has_remove = true;
                    ++GameData.atk_cnt[GameData.type[i, j]];
                    GameData.turning_stone[i * 6 + j].SetActive(false);
                    if (st_sound_effect != null) Instantiate(st_sound_effect,Vector2.zero,Quaternion.identity);
                    while ((k >= 0) && (GameData.type[i, j] == GameData.type[i, k]))
                    {
                        ++GameData.atk_cnt[GameData.type[i, k]];
                        GameData.turning_stone[i * 6 + k].SetActive(false);
                        --k;
                    }
                }
            }
        }

        // Count stone of same type in a column
        init();
        for (int j = 0; j < 6; ++j) {
            for (int i = 1; i < 5; ++i) {
                if (GameData.type[i, j] == GameData.type[i - 1, j])
                {
                    record[i, j] = record[i - 1,j] + 1;
                }
            }
        }

        // Remove the stones who appear continuously in a column
        for (int j = 5; j >= 0; --j)
        {
            for (int i = 4; i >= 0; --i)
            {
                if (record[i, j] >= 3)
                {
                    int k = i - 1;
                    has_remove = true;
                    if (GameData.turning_stone[i * 6 + j].activeSelf)
                    {
                        ++GameData.atk_cnt[GameData.type[i, j]];
                        GameData.turning_stone[i * 6 + j].SetActive(false);
                    }
                    if (st_sound_effect != null) Instantiate(st_sound_effect, Vector2.zero, Quaternion.identity);
                    while ((k >= 0) && (GameData.type[i, j] == GameData.type[k, j]))
                    {
                        if (GameData.turning_stone[k * 6 + j].activeSelf)
                        {
                            ++GameData.atk_cnt[GameData.type[k, j]];
                            GameData.turning_stone[k * 6 + j].SetActive(false);
                        }
                        --k;
                    }
                }
            }
        }

        return has_remove;

    }

    public static bool FallStone() {

        bool has_fall = false;

        for (int i = 3; i >= 0; --i) {
            for (int j = 0; j < 6; ++j) {
                if (GameData.turning_stone[i*6 + j].activeSelf && !GameData.turning_stone[(i + 1) * 6 + j].activeSelf) {
                    GameData.turning_stone[(i + 1) * 6 + j].GetComponent<Image>().sprite = GameData.turning_stone[i * 6 + j].GetComponent<Image>().sprite;
                    GameData.type[i + 1, j] = GameData.type[i, j];
                    GameData.turning_stone[(i + 1) * 6 + j].SetActive(true);
                    GameData.turning_stone[i * 6 + j].SetActive(false);
                    has_fall = true;
                }
            }
        }

        return has_fall;

    }

    public static void FillStone() {

        bool has_fall = true;

        // Make the stones fall down
        while (has_fall) {
            has_fall = FallStone();
        }

        // Refill the turning table
        for (int i = 0; i < 5; ++i) {
            for (int j = 0; j < 6; ++j) {
                if (!GameData.turning_stone[i * 6 + j].activeSelf) {
                    int s = Random.Range(0, 6);
                    GameData.turning_stone[i*6 + j].GetComponent<Image>().sprite = GameData.turning_img[s];
                    GameData.type[i,j] = s;
                    GameData.turning_stone[i * 6 + j].SetActive(true);
                }
            }
        }

    }	

}
