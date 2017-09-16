using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAPI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float MakeTransaction(float amt, int cat)
    {
        return -amt;
    }
}
