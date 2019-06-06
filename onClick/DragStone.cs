using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStone : MonoBehaviour {

    private Vector3 pos;
    public bool onDrag;
    public int num;

    void Start() {
        pos = transform.position;
    }

	// Update is called once per frame
    void Update() {

        if (onDrag && GameData.timeout) {
            onDrag = false;
            transform.position = pos;
        }

        if (onDrag) {
            transform.position = Input.mousePosition;
        }

    }

    void OnTriggerEnter2D(Collider2D collider) {

        if (!onDrag) return;
        Vector3 temp = collider.transform.position;
        collider.transform.position = pos;
        pos = temp;

        int ex_num,type;
        GameData.turning_stone[collider.GetComponent<DragStone>().num] = this.gameObject;
        GameData.turning_stone[num] = collider.gameObject;
        type = GameData.type[collider.GetComponent<DragStone>().num/6, collider.GetComponent<DragStone>().num%6];
        GameData.type[collider.GetComponent<DragStone>().num / 6, collider.GetComponent<DragStone>().num % 6] = GameData.type[this.num/6,this.num%6];
        GameData.type[this.num / 6, this.num % 6] = type;
        ex_num = collider.GetComponent<DragStone>().num;
        collider.GetComponent<DragStone>().num = this.num;
        this.num = ex_num;

    }

    public void onClick() {

        if (GameData.checking) return;
        onDrag = !onDrag;
        GameData.draging = onDrag;
        GameData.timeout = !onDrag;

        if (onDrag) pos = transform.position;
        else
        {
            transform.position = pos;
            GameData.TimerCancel();
        }

        if (GameData.timeout)
        {
            GameData.ResetAtk();
            if(CheckStone.CountStone())
            {
                CheckStone.FillStone();
                GameData.checking = true;
            }
            else
            {
                // Make enemy attack
                GameData.ex_team_hp -= GameData.st_enemy.GetComponent<EnemyData>().Attack();
                if (GameData.ex_team_hp <= 0)
                {
                    GameData.ex_team_hp = 0;
                    GameControl.GameOver(false);
                    for (int i = 0; i < 30; ++i) GameData.turning_stone[i].SetActive(false);
                }
            }

        }

    }

}
