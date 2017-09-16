using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Specialized;
using System.Net;
using UnityEngine.Networking;

public static class Http
{
    public static byte[] Post(string uri, NameValueCollection pairs)
    {
        byte[] response = null;
        using (WebClient client = new WebClient())
        {
            response = client.UploadValues(uri, pairs);
        }
        return response;
    }
}


public class API : MonoBehaviour {

    // Use this for initialization
    public string URL = "ec2-34-213-177-23.us-west-2.compute.amazonaws.com:8080/transaction?user=1&transname=a&transamt=10&transcat=1";
    public string fuck = "ec2-34-213-177-23.us-west-2.compute.amazonaws.com:8080/transaction?user=1";
    private bool _lock;
    private WWW _www;
    void Start () {
        _lock = false;
        _www = null;
        MakeTransaction("1", "1", 10, "1");
        StartCoroutine(fuckmylife());
	}
	
	// Update is called once per frame
	void Update () {
	}

 
    void MakeTransaction(string userid, string name, float money, string category)
    {
        var dummy = new WWWForm();
        UnityWebRequest.Post(URL, dummy);

// Debug.Log(result);
    }

    IEnumerator fuckmylife()
    {
        UnityWebRequest a = UnityWebRequest.Post("ec2-34-213-177-23.us-west-2.compute.amazonaws.com:8080/transaction?user=1&transname=a&transamt=10&transcat=1", new WWWForm());
        yield return a.Send();
        if(a.isError)
        {
            Debug.Log(a.error);
        } else
        {
            Debug.Log("It bloody worked");
        }


        var dummy = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post(URL, dummy))
        {
            yield return www;
            print(www);
        }
    }

    IEnumerator WaitForRequest(WWW www)
    {
        Debug.Log("we got here");
        yield return www;
        print(www.text);

    }
}
