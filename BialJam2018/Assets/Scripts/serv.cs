using UnityEngine;
using UnityEngine.Networking;

public class serv : MonoBehaviour {
    public int[] ids;
    public Transform[] cube;
    public struct touchcontrol
    {
        public int phase;
        public Vector2 pos;
        public int fid;
        public int tc;
    }
    public class RegisterHostMessage : MessageBase
    {
        public Vector3 rotaterate;
        public Vector3 accel;
        public touchcontrol[] tc;
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
        RegisterHostMessage rhm = netMsg.ReadMessage<RegisterHostMessage>();
        Vector3 tmp = rhm.rotaterate;
        Vector3 tmpx = rhm.accel;
        int hymm = rhm.tc.Length;
        Debug.Log(tmp+" # "+tmpx + " # " +hymm);
        for(int ti=0;ti<ids.Length;ti++)
        {
            if(ids[ti] == netMsg.conn.connectionId)
            {
                if ()
                {

                }
                cube[ti].Rotate(tmp);
            }
        }
    }
}
