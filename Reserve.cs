﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reserve : MonoBehaviour {

    private void Awake(){
        DontDestroyOnLoad(gameObject);
    }

}
