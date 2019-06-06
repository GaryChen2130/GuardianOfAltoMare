using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

    public Button[] invisible_btn = new Button[5];
    public Button[] visible_btn = new Button[5];

    public void UpdateMenu()
    {

        for (int i = 0; i < 5; ++i)
        {
            if (invisible_btn[i] != null) invisible_btn[i].gameObject.SetActive(false);
            if (visible_btn[i] != null) visible_btn[i].gameObject.SetActive(true);
        }

    }

}
