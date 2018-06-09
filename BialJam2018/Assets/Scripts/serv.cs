using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class serv : MonoBehaviour {
    public int[] ids;
    public Transform[] cube;
    public Color newcol;
    public Color test;
    public float[] tess;
    public bool tests = false;
    public float lolnope;
    public int lastphase;
    public Vector3 starttrans;
    public Quaternion startrot;
    public bool notmoved;
    public float upcorrect;
    public float leftcor;
    public RectTransform[] rt;
    public Vector3 startrttr;
    public Quaternion startrtrot;
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
        starttrans = cube[0].transform.position;
        startrot = cube[0].transform.rotation;
        startrttr = rt[0].position;
        startrtrot = rt[0].rotation;
        tess = new float[2];
        tess[0] = -100;
        tess[1] = 100;
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
        if(tmpx.y > tess[0])
        {
            tess[0] = tmpx.y;
        }else if (tmpx.y < tess[1])
        {
            tess[1] = tmpx.y;
        }
        int hymm = rhm.tc.Length;
        Debug.Log(tmp+" # "+tmpx + " # " +hymm);
        for(int ti=0;ti<ids.Length;ti++)
        {
            if(ids[ti] == netMsg.conn.connectionId)
            {
                if (ti == 0)
                {
                    if (tests && tmpx.y < -0.75)
                    {
                        test = newcol;
                        tests = false;
                    }
                    if (tmpx.y > 3)
                    {
                        tests = true;
                    }
                    cube[ti].transform.Translate(Vector3.up * -tmp.x * lolnope);
                    cube[ti].transform.Translate(Vector3.right * tmp.z * lolnope);
                    rt[ti].transform.Translate(Vector3.up * -tmp.x * lolnope*5);
                    rt[ti].transform.Translate(Vector3.right * tmp.z * lolnope*5);
                }
                else
                {
                    if (tests && tmpx.y < -1)
                    {
                        test = newcol;
                        tests = false;
                    }
                    if (tmpx.y > 1.9)
                    {
                        tests = true;
                    }
                    cube[ti].transform.Translate(Vector3.up * (-tmp.x-upcorrect) * lolnope);
                    cube[ti].transform.Translate(Vector3.right * (-tmp.z - leftcor) * lolnope);
                }
                rt[ti].Rotate(Vector3.forward * tmp.y);
                cube[ti].Rotate(Vector3.forward * tmp.y);
                if (rhm.tc.Length == 1)
                {
                    if(rhm.tc[0].phase == 1)
                    {
                        notmoved = false;
                    }else if (rhm.tc[0].phase == 0)
                    {
                        notmoved = true;
                    }
                    if(rhm.tc[0].phase == 3 && rhm.tc[0].tc == 2 &&notmoved)
                    {
                        Debug.Log("wtf");
                        cube[ti].transform.position = starttrans;
                        cube[ti].transform.rotation = startrot;
                        rt[ti].rotation = startrtrot;
                        rt[ti].position = startrttr;
                    }
                    lastphase = rhm.tc[0].phase;
                }
            }
        }
    }
}
