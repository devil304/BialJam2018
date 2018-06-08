using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Jobs;
using Unity.Collections;

struct trololo : IJob
{
    [ReadOnly]
    public NativeArray<Vector3> cos;
    public void Execute()
    {

    }
}
public class RegisterHostMessage : MessageBase
{
    public Vector3 rotaterate;
}
public class client : MonoBehaviour {
    public Text tx;
    public GameObject pufip;
    NetworkClient myClient;
    string lastmsg;

    public void OnConnected(NetworkMessage nm)
    {
        Debug.Log("Connected to server");
    }

    public void OnDisconnected(NetworkMessage nm)
    {
        Debug.Log("Disconnected from server");
        pufip.SetActive(true);
    }

    public void OnError(NetworkMessage nm)
    {
        if (("Error connecting with code " + nm.ToString()) != lastmsg)
        {
            Debug.Log("Error connecting with code " + nm.ToString());
            lastmsg = "Error connecting with code " + nm.ToString();
            pufip.SetActive(true);
        }
    }


    // Use this for initialization
    void Start () {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        myClient.RegisterHandler(MsgType.Error, OnError);
    }
	
	// Update is called once per frame
	void Update () {
        if (myClient.isConnected)
        {
            pufip.SetActive(false);
        }
    }
    public const short RegisterHostMsgId = 888;
    private void FixedUpdate()
    {
        if (myClient.isConnected)
        {
            RegisterHostMessage msg = new RegisterHostMessage();
            Input.gyro.enabled=true;
            msg.rotaterate = Input.gyro.rotationRate;
            myClient.SendByChannel(RegisterHostMsgId,msg,1);
        }
    }
    public void CON()
    {
        if (!myClient.isConnected && tx.text != "")
        {
            pufip.SetActive(true);
            myClient.Connect(tx.text, 8075);
        }
    }
}
