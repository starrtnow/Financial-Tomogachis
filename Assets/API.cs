using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API : MonoBehaviour {

    // Use this for initialization
    public string URL = "ec2-34-213-177-23.us-west-2.compute.amazonaws.com:8080";
    public string fuck = "ec2-34-213-177-23.us-west-2.compute.amazonaws.com:8080/transaction?user=1&transname=1&transamt=money&transcat=1";
    private bool _lock;
    private WWW _www;
    void Start () {
        _lock = false;
        _www = null;
        MakeTransaction("1", "1", 10, "1");
        Debug.Log("something ahpeennd");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void MakeTransaction(string userid, string name, float money, string category)
    {
        if (!_lock){
            var form = new WWWForm();
            form.AddField("user", userid);
            form.AddField("transname", name);
            form.AddField("transamt", money.ToString());
            form.AddField("transcat", category);
            var www = new WWW(fuck);
            var result = WaitForRequest(www);
            Debug.Log(result);
        }
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
    }
}
