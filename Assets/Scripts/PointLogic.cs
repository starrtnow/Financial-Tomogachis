using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLogic : MonoBehaviour {

    // Use this for initialization
    public int totalPoints = 0;

    public float daily_budget = 5.0f;

    public float daily_expenditures = 0.0f;

    void add_transaction(float transaction)
    {
        daily_expenditures += transaction;
    }

    float calculate_health_change()
    {
        return daily_budget - daily_expenditures;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
