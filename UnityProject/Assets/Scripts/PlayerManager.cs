﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;

    public void SelfDestruct() {
        this.gameObject.SetActive(false);
    }
}
