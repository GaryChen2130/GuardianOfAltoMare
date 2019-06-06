using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour {

    public Text result;

    void Update() {
        result.text = GameControl.outcome;
    }

}
