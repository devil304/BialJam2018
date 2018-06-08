using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class serv : MonoBehaviour {
    public int[] ids;
    public Transform[] cube;
    public class RegisterHostMessage : MessageBase
    {
        public Vector3 rotaterate;
    }
    public string ip = "0";
    // Use this for initialization
    public void OnConnected(NetworkMessage nm)
    {
        for(int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == -100)
            {
                ids[i] = nm.conn.connectionId;
                break;
            }else if(i == ids.Length - 1)
            {
                nm.conn.Disconnect();
                break;
            }
        }
        Debug.Log("Connected to server");
    }

    public void OnDisconnected(NetworkMessage nm)
    {
        Debug.Log("Disconnected from server");
        for (int ti = 0; ti < ids.Length; ti++)
        {
            if (ids[ti] == nm.conn.connectionId)
            {
                ids[ti] = -100;
            }
        }
    }

    public void OnError(NetworkMessage nm)
    {
        Debug.Log("Error connecting with code " + nm.ToString());
    }
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        if (ip != "0")
        {
            NetworkServer.Listen(ip,8075);
        }
        else
        {
            NetworkServer.Listen(8075);
        }
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        NetworkServer.RegisterHandler(MsgType.Error, OnError);
        NetworkServer.RegisterHandler(888, messrecived);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void messrecived(NetworkMessage netMsg)
    {
        Vector3 tmp = netMsg.ReadMessage<RegisterHostMessage>().rotaterate;
        Debug.Log(tmp);
        for(int ti=0;ti<ids.Length;ti++)
        {
            if(ids[ti] == netMsg.conn.connectionId)
            {
                cube[ti].Rotate(tmp);
            }
        }
    }
}
