using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public int stage_num;
    static float timer_f;
    static int timer_i;
    static int limit;
    static bool start;
    static bool timeout;
    static string target;

    void Start() {
        timer_f = 0f;
        timer_i = 0;
        limit = 0;
        start = false;
        timeout = false;
    }

    void Update() {

        if (timer_i > limit) {
            start = false;
            timeout = true;
            SceneManager.LoadScene(target);
        }

        if (start && !timeout) {
            timer_f += Time.deltaTime;
            //if (timer_i != (int)timer_f) Debug.Log(timer_i);
            timer_i = (int)timer_f;
        }

    }

    private static void TimerStart(int sec) {
        timer_f = 0f;
        timer_i = 0;
        limit = sec;
        start = true;
        timeout = false;
    }

    public void ChangeToScene(string scene_name) {

        //string recent_scene = SceneManager.GetActiveScene().name;
        //if (scene_name == recent_scene) return;

        //scene_stack.Push(recent_scene);
        if(stage_num > 0)GameControl.LoadStage(stage_num);
        SceneManager.LoadScene(scene_name);
        //PlayerData.instance.state["moving"] = false;

        return;

    }

    public static void ChangeToSceneOnDelay(string scene_name)
    {

        //string recent_scene = SceneManager.GetActiveScene().name;
        //if (scene_name == recent_scene) return;

        if (start) return;
        TimerStart(2);
        target = scene_name;

        return;

    }

    public void ReturnScene() {

        //if (scene_stack.Count > 0)SceneManager.LoadScene(scene_stack.Pop());
        return;

    }


}
