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
    public int[] lastphase;
    public Vector3[] starttrans;
    public Quaternion[] startrot;
    public bool[] notmoved;
    public float upcorrect;
    public float leftcor;
    public RectTransform[] rt;
    public Vector3[] startrttr;
    public Quaternion[] startrtrot;
    public float forcorr;
    public float tmpxx, tmpy, tmpz;
    public Camera mc;
    public RenderTexture[] rtx;
    public Camera[] cameras;
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
        foreach(RenderTexture rtt in rtx)
        {
            rtt.width = mc.pixelWidth / 2;
            rtt.height = mc.pixelHeight;
        }
        notmoved = new bool[2];
        lastphase = new int[2];
        startrot = new Quaternion[2];
        startrtrot = new Quaternion[2];
        startrttr = new Vector3[2];
        starttrans = new Vector3[2];
        starttrans[0] = cube[0].transform.position;
        startrot[0] = cube[0].transform.rotation;
        startrttr[0] = rt[0].position;
        startrtrot[0]= rt[0].rotation;
        starttrans[1] = cube[1].transform.position;
        startrot[1] = cube[1].transform.rotation;
        startrttr[1] = rt[1].position;
        startrtrot[1] = rt[1].rotation;
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
    IEnumerator corrpls()
    {
        yield return new WaitForSecondsRealtime(3);
        upcorrect = tmpxx;
        leftcor = tmpz;
        forcorr = tmpy;
    }
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
                    if (rt[ti].anchoredPosition.y < -450)
                    {
                        if (tmp.x * lolnope > 0 && rt[ti].anchoredPosition.y > -500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        if (tmp.x * lolnope > 0)
                        {
                            cameras[ti].transform.Rotate(Vector3.right * (lolnope * 50));
                        }else if(rt[ti].anchoredPosition.y <= -500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        else if (tmp.x * lolnope < 0)
                        {
                            cameras[ti].transform.Rotate(Vector3.right * ((lolnope * 50) - (tmp.x)));
                        }
                    }
                    else if (rt[ti].anchoredPosition.y > 450)
                    {
                        if (tmp.x * lolnope < 0 && rt[ti].anchoredPosition.y <500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        if(tmp.x * lolnope < 0)
                        {
                            cameras[ti].transform.Rotate(Vector3.right * (-lolnope * 50));
                        }else if(rt[ti].anchoredPosition.y >= 500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        else if(tmp.x * lolnope > 0)
                        {
                            cameras[ti].transform.Rotate(Vector3.right * ((-lolnope*50) - (tmp.x)));
                        }
                    }
                    else
                    {
                        rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                    }
                    if(rt[ti].anchoredPosition.y >= 600)
                    {
                        rt[ti].anchoredPosition = new Vector2(rt[ti].anchoredPosition.x,490);
                    }else if(rt[ti].anchoredPosition.y <= -600)
                    {
                        rt[ti].anchoredPosition = new Vector2(rt[ti].anchoredPosition.x, -490);
                    }
                    if (rt[ti].anchoredPosition.x >= 500)
                    {
                        rt[ti].anchoredPosition = new Vector2(390, rt[ti].anchoredPosition.y);
                    }
                    else if (rt[ti].anchoredPosition.x <= -500)
                    {
                        rt[ti].anchoredPosition = new Vector2(-390, rt[ti].anchoredPosition.y);
                    }


                    if (rt[ti].anchoredPosition.x < -350)
                    {
                        if (-tmp.z * lolnope > 0 && rt[ti].anchoredPosition.x > -400)
                        {
                            rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                        }
                        if (-tmp.z * lolnope > 0)
                        {
                            cameras[ti].transform.Rotate(-Vector3.up * (lolnope * 50));
                        }
                        else if (rt[ti].anchoredPosition.x <= -400)
                        {
                            rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                        }
                        else if (-tmp.z * lolnope < 0)
                        {
                            cameras[ti].transform.Rotate(-Vector3.up * ((lolnope * 50) - (-tmp.z)));
                        }
                    }
                    else if (rt[ti].anchoredPosition.x > 350)
                    {
                        if (-tmp.z * lolnope < 0 && rt[ti].anchoredPosition.x < 400)
                        {
                            rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                        }
                        if (-tmp.z * lolnope < 0)
                        {
                            cameras[ti].transform.Rotate(-Vector3.up * (-lolnope * 50));
                        }
                        else if (rt[ti].anchoredPosition.x >= 400)
                        {
                            rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                        }
                        else if (-tmp.z * lolnope > 0)
                        {
                            cameras[ti].transform.Rotate(-Vector3.up * ((-lolnope * 50) - (-tmp.z)));
                        }
                    }
                    else
                    {
                        rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                    }
                    cube[ti].transform.Translate(Vector3.up * -tmp.x * lolnope);
                    cube[ti].transform.Translate(Vector3.right * tmp.z * lolnope);
                    rt[ti].Rotate(Vector3.forward * -tmp.y);
                    cube[ti].Rotate(Vector3.forward * tmp.y);
                }
                else
                {
                    if (upcorrect == -100 && leftcor == -100 && forcorr == -100)
                    {
                        if(tmpxx == -100 && tmpy == -100 && tmpz == -100 && (tmp.x != 0 && tmp.z != 0 && tmp.y != 0))
                        {
                            tmpxx = tmp.x;
                            tmpy = tmp.y;
                            tmpz = tmp.z;
                        }
                        if (tmpxx < 0 && tmp.x<tmpxx && tmp.x!=0)
                        {
                            tmpxx = tmp.x;
                        } else if (tmpxx > 0 && tmp.x>tmpxx && tmp.x != 0) {
                            tmpxx = tmp.x;
                        }
                        if (tmpy < 0 && tmp.y < tmpy && tmp.y != 0)
                        {
                            tmpy = tmp.y;
                        }
                        else if (tmpy > 0 && tmp.y > tmpy && tmp.y != 0)
                        {
                            tmpy = tmp.y;
                        }
                        if (tmpz < 0 && tmp.z < tmpz && tmp.z != 0)
                        {
                            tmpz = tmp.z;
                        }
                        else if (tmpz > 0 && tmp.z > tmpz && tmp.z != 0)
                        {
                            tmpz = tmp.z;
                        }
                        StartCoroutine(corrpls());
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
                        float corrx = tmp.x - upcorrect;
                        float corrz = tmp.z - leftcor;
                        float corry = tmp.y - forcorr;
                        cube[ti].transform.Translate(Vector3.up * corrx * lolnope);
                        cube[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                        if (rt[ti].anchoredPosition.y < -450)
                        {
                            if (corrx * lolnope > 0 && rt[ti].anchoredPosition.y > -500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            if (corrx * lolnope > 0)
                            {
                                cameras[ti].transform.Rotate(Vector3.right * (lolnope * 50));
                            }
                            else if (rt[ti].anchoredPosition.y <= -500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            else if (corrx * lolnope < 0)
                            {
                                cameras[ti].transform.Rotate(Vector3.right * ((lolnope * 50) - (corrx)));
                            }
                        }
                        else if (rt[ti].anchoredPosition.y > 450)
                        {
                            if (corrx * lolnope < 0 && rt[ti].anchoredPosition.y < 500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            if (corrx * lolnope < 0)
                            {
                                cameras[ti].transform.Rotate(Vector3.right * (-lolnope * 50));
                            }
                            else if (rt[ti].anchoredPosition.y >= 500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            else if (corrx * lolnope > 0)
                            {
                                cameras[ti].transform.Rotate(Vector3.right * ((-lolnope * 50) - (corrx)));
                            }
                        }
                        else
                        {
                            rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                        }
                        if (rt[ti].anchoredPosition.y >= 600)
                        {
                            rt[ti].anchoredPosition = new Vector2(rt[ti].anchoredPosition.x, 490);
                        }
                        else if (rt[ti].anchoredPosition.y <= -600)
                        {
                            rt[ti].anchoredPosition = new Vector2(rt[ti].anchoredPosition.x, -490);
                        }
                        if (rt[ti].anchoredPosition.x >= 500)
                        {
                            rt[ti].anchoredPosition = new Vector2(390, rt[ti].anchoredPosition.y);
                        }
                        else if (rt[ti].anchoredPosition.x <= -500)
                        {
                            rt[ti].anchoredPosition = new Vector2(-390, rt[ti].anchoredPosition.y);
                        }


                        if (rt[ti].anchoredPosition.x < -350)
                        {
                            if (-corrz * lolnope > 0 && rt[ti].anchoredPosition.x > -400)
                            {
                                rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                            }
                            if (-corrz * lolnope > 0)
                            {
                                cameras[ti].transform.Rotate(-Vector3.up * (lolnope * 50));
                            }
                            else if (rt[ti].anchoredPosition.x <= -400)
                            {
                                rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                            }
                            else if (-corrz * lolnope < 0)
                            {
                                cameras[ti].transform.Rotate(-Vector3.up * ((lolnope * 50) - (-corrz)));
                            }
                        }
                        else if (rt[ti].anchoredPosition.x > 350)
                        {
                            if (-corrz * lolnope < 0 && rt[ti].anchoredPosition.x < 400)
                            {
                                rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                            }
                            if (-corrz * lolnope < 0)
                            {
                                cameras[ti].transform.Rotate(-Vector3.up * (-lolnope * 50));
                            }
                            else if (rt[ti].anchoredPosition.x >= 400)
                            {
                                rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                            }
                            else if (-corrz * lolnope > 0)
                            {
                                cameras[ti].transform.Rotate(-Vector3.up * ((-lolnope * 50) - (-corrz)));
                            }
                        }
                        else
                        {
                            rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                        }
                        rt[ti].Rotate(Vector3.forward * -corry);
                        cube[ti].Rotate(Vector3.forward * -corry);
                    }
                }
                if (rhm.tc.Length == 1)
                {
                    if(rhm.tc[0].phase == 1)
                    {
                        notmoved[ti] = false;
                    }else if (rhm.tc[0].phase == 0)
                    {
                        notmoved[ti] = true;
                    }
                    if(rhm.tc[0].phase == 3 && rhm.tc[0].tc == 2 &&notmoved[ti])
                    {
                        Debug.Log("wtf");
                        cube[ti].transform.position = starttrans[ti];
                        cube[ti].transform.rotation = startrot[ti];
                        rt[ti].rotation = startrtrot[ti];
                        rt[ti].position = startrttr[ti];
                    }
                    lastphase[ti] = rhm.tc[0].phase;
                }
            }
        }
    }
}
