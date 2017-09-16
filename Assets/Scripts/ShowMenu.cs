using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenu : MonoBehaviour {

    // Use this for initialization

    public GameObject Panel;
    public PointLogic Logic;
    public FakeAPI API;
    public InputField text;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPanel(GameObject o)
    {
        o.SetActive(true);
    }

    public void ProcessTransaction(InputField text)
    {
        var amt = Int32.Parse(text.text);
        Logic.daily_expenditures += amt;
    }

    public void HidePanel(GameObject o)
    {

        text.text = "";
        o.SetActive(false);
    }

   

    
}
