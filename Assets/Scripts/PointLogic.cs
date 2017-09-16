using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLogic : MonoBehaviour {

    // Use this for initialization
    public int totalPoints = 0;

    public float daily_budget = 5.0f;
    public float daily_expenditures = 0.0f;

    public Text MoodText;
    public int Health = 100;

    public Button button;

    void Start()
    {
        button.onClick.AddListener(SubtractHealth);
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

    public string getHealthMessage()
    {
        if (Health > 80)
        {
            return "Cat is healthy!";
        }
        else if (Health > 60)
        {
            return "Cat's feeling okay.";
        }
        else if (Health > 40)
        {
            return "Cat's starting to feel a little sick...";
        }
        else if (Health > 20)
        {
            return "Cat's not feeling so great...";
        }
        else
        {
            return "Your cat's close to dying!";
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
