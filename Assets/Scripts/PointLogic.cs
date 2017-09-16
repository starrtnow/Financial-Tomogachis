using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLogic : MonoBehaviour {

    // Use this for initialization
    public int totalPoints = 0;

    public float daily_budget = 5.0f;
    public float daily_expenditures = 0.0f;

    public GameObject msg_panel;
    public Text msg_text;

    public Text MoodText;
    public int Health = 100;

    public Button button;

    void Start()
    {
     
    }
    void add_transaction(float transaction)
    {
        daily_expenditures += transaction;
    }

    void SubtractHealth()
    {
        Health -= 10;
    }

    public void ChangeHealth(int health)
    {
        Health = health;
    }

    public void DeltaHealth(int delta)
    {
        Health += delta;
    }

    public void PassADay()
    {
        var diff = daily_budget - daily_expenditures;
        msg_panel.SetActive(true);
        if (diff > 0)
        {
            //pop - Congrats! You saved blah blah! You're 5% closer to your goal of blah blah blah
            msg_text.text = String.Format("Congrats! Compared to average American teen, you gained {0} dollars today! With your current income, you are 5% closer to your goal of $700 dollars", diff);
            var factor = 10 * diff / daily_budget;
            Health += (int)factor;
        }
        else
        {
            msg_text.text = String.Format("Aww, you spent {0} more than the average American teen. With your current income, you are -2% closer to your goal of $700 dollars", -1 * diff);
            var factor = 10 * diff / daily_budget;
            Health += (int)factor;
            //pop - Aww, you went X over what the average american teens spends.
        }
        daily_expenditures = 0;
    }

    public void HideMsgPanel(GameObject o)
    {
        o.SetActive(false);
    }

    public string getHealthMessage()
    {
        if (Health > 80)
        {
            return "Cat is healthy!" + Health;
        }
        else if (Health > 60)
        {
            return "Cat's feeling okay." + Health;
        }
        else if (Health > 40)
        {
            return "Cat's starting to feel a little sick..." + Health;
        }
        else if (Health > 20)
        {
            return "Cat's not feeling so great..." + Health;
        }
        else
        {
            return "Your cat's close to dying!" + Health;
        }
    }

    float calculate_health_change()
    {
        return daily_budget - daily_expenditures;
    }
	
	// Update is called once per frame
	void Update () {
        {
            var message = getHealthMessage();
            MoodText.text = message;
        }
	}
}
