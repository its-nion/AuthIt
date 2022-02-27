using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGameobject : MonoBehaviour
{
    private static DontDestroyGameobject instance = null;
    public static DontDestroyGameobject Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
