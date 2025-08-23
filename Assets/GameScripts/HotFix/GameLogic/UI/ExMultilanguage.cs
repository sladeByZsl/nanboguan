using System;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ExMultilanguage : MonoBehaviour
{
    public string key;
    private Text txt; 
    private void Awake()
    {
        txt = this.GetComponent<Text>();
    }

    private void OnEnable()
    {
        if(txt==null) return;
        txt.text = LocalizationManager.Instance.GetText(key);
    }
}