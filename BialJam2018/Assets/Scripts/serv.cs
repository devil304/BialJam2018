using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Animations;

public class serv : MonoBehaviour {
    public int[] ids;
    public Transform[] cube;
    public float[] tess;
    public bool tests = false;
    public float lolnope;
    public int[] lastphase;
    public bool[] notmoved;
    public float upcorrect;
    public float leftcor;
    public RectTransform[] rt;
    public Vector2[] startrttr;
    public Quaternion[] startrtrot;
    public float forcorr;
    public float tmpxx, tmpy, tmpz;
    public Camera mc;
    public RenderTexture[] rtx;
    public Camera[] cameras;
    public Transform[] cbs;
    public float[] camwych;
    public Vector2[] startpost, wektorpada;
    public bool[] archerrr;
    public float dcien;
    public Transform trst;
    public Vector3[] trranstest;
    public Transform[] handstart;
    public CharacterController[] cc;
    public Animator[] animki;
    public Vector3 movedir;
    public int tix;
    public bool[] dzig;
    public float maxwys;
    public Vector3[] controll;
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
        dzig = new bool[2];
        controll = new Vector3[2];
        movedir = new Vector3(0, 0, 0);
        trranstest = new Vector3[2];
        archerrr = new bool[2];
        startpost = new Vector2[2];
        wektorpada = new Vector2[2];
        camwych = new float[2];
        camwych[0] = 0;
        camwych[1] = 0;
        foreach (RenderTexture rtt in rtx)
        {
            rtt.width = mc.pixelWidth / 2;
            rtt.height = mc.pixelHeight;
        }
        notmoved = new bool[2];
        lastphase = new int[2];
        startrtrot = new Quaternion[2];
        startrttr = new Vector2[2];
        startrttr[0] = rt[0].anchoredPosition;
        startrtrot[0]= rt[0].localRotation;
        startrttr[1] = rt[1].anchoredPosition;
        startrtrot[1] = rt[1].localRotation;
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
        /*if(dzig[tix]&&cube[tix].transform.localPosition.y < maxwys)
        {
            cube[tix].transform.Translate(Vector3.forward);
        }
        else if(cube[tix].transform.localPosition.y >= 1.3f)
        {
            cube[tix].transform.Translate(-Vector3.forward);
            dzig[tix] = false;
        }*/
        //Debug.Log(cube[tix].transform.localPosition.y);
        if (cc[tix].isGrounded)
        {
                controll[tix] = cc[tix].transform.TransformDirection(new Vector3(wektorpada[tix].x / 40, 0, wektorpada[tix].y / 40));
            cc[tix].Move(controll[tix] * Time.deltaTime);
            if (wektorpada[tix].magnitude > 150 && wektorpada[tix].magnitude < 350)
            {
                //Debug.Log(wektorpada[ti].y + "#" + wektorpada[ti].x);
                if (wektorpada[tix].y > 150)
                {
                    animki[tix].SetInteger("animka", 3);
                }
                else
                {
                    animki[tix].SetInteger("animka", 1);
                }

            }
            else if (wektorpada[tix].magnitude >= 350)
            {
                if (wektorpada[tix].y > 150)
                {
                    animki[tix].SetInteger("animka", 4);
                }
                else
                {
                    animki[tix].SetInteger("animka", 2);
                }
            }
        }
        else
        {
            cc[tix].Move(Vector3.up * -10 * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        
    }
    void LateUpdate()
    {
        for (int ti = 0; ti < 2; ti++)
        {
            //cube[ti].transform.Rotate(Vector3.right * (lolnope * 60)* (trranstest[ti].y * 2));
            cube[ti].transform.Translate(trranstest[ti]*2);
        }
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
        //Debug.Log(tmp+" # "+tmpx);
        for(int ti=0;ti<ids.Length;ti++)
        {
            if(ids[ti] == netMsg.conn.connectionId)
            {
                if (rhm.tc.Length == 1)
                {
                    switch (rhm.tc[0].phase)
                    {
                        case 0:
                            startpost[ti] = rhm.tc[0].pos;
                            wektorpada[ti] = new Vector2(0, 0);
                            break;
                        case 1:
                            if ((rhm.tc[0].pos - startpost[ti]).magnitude >= 150)
                            {
                                wektorpada[ti] = new Vector2((rhm.tc[0].pos - startpost[ti]).x, (rhm.tc[0].pos - startpost[ti]).y);
                            }
                            break;
                    }
                    if (rhm.tc[0].phase != 3 && rhm.tc[0].phase != 4 && rhm.tc[0].phase != 0)
                    {
                        if (!archerrr[ti])
                        {
                            Debug.Log("nodziała");
                            tix = ti;
                        }
                        else
                        {
                            animki[ti].SetInteger("animka", 0);
                            dcien = wektorpada[ti].magnitude;
                        }
                    }
                    else if(rhm.tc[0].phase == 3 || rhm.tc[0].phase == 4)
                    {
                        wektorpada[ti] = new Vector2(0, 0);
                        animki[ti].SetInteger("animka", 0);
                    }
                }
                //Debug.Log(wektorpada[ti]);
                if (ti == 0)
                {
                    if (tests && tmpx.y < -0.75)
                    {
                        dzig[ti] = true;
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
                        if (tmp.x * lolnope > 0 && camwych[ti] > -40)
                        {
                            cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * (lolnope * 60));
                            camwych[ti] -= (lolnope * 60);
                        }
                        else if(rt[ti].anchoredPosition.y <= -500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        else if (tmp.x * lolnope < 0 && camwych[ti] > -40)
                        {
                            cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * ((lolnope * 60) - (tmp.x)));
                            camwych[ti] -= (lolnope * 60) - (tmp.x);
                        }
                    }
                    else if (rt[ti].anchoredPosition.y > 450)
                    {
                        if (tmp.x * lolnope < 0 && rt[ti].anchoredPosition.y <500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        if(tmp.x * lolnope < 0 && camwych[ti] < 50)
                        {
                            cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * (-lolnope * 60));
                            camwych[ti] -= (-lolnope * 60);
                        }
                        else if(rt[ti].anchoredPosition.y >= 500)
                        {
                            rt[ti].transform.Translate(Vector3.up * tmp.x * lolnope);
                        }
                        else if(tmp.x * lolnope > 0 && camwych[ti] < 50)
                        {
                            cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * ((-lolnope*60) - (tmp.x)));
                            camwych[ti] -= (-lolnope * 60) - (tmp.x);
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
                            cbs[ti].transform.Rotate(-Vector3.up * (lolnope * 60));
                        }
                        else if (rt[ti].anchoredPosition.x <= -400)
                        {
                            rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                        }
                        else if (-tmp.z * lolnope < 0)
                        {
                            cbs[ti].transform.Rotate(-Vector3.up * ((lolnope * 60) - (-tmp.z)));
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
                            cbs[ti].transform.Rotate(-Vector3.up * (-lolnope * 60));
                        }
                        else if (rt[ti].anchoredPosition.x >= 400)
                        {
                            rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                        }
                        else if (-tmp.z * lolnope > 0)
                        {
                            cbs[ti].transform.Rotate(-Vector3.up * ((-lolnope * 60) - (-tmp.z)));
                        }
                    }
                    else
                    {
                        rt[ti].transform.Translate(Vector3.right * -tmp.z * lolnope);
                    }
                    trranstest[ti] = Vector3.forward * tmp.x * lolnope;
                    trranstest[ti]+=Vector3.right * tmp.z * lolnope;
                    rt[ti].Rotate(Vector3.forward * -tmp.y);
                    cube[ti].Rotate(Vector3.up * -tmp.y);
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
                            dzig[ti] = true;
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
                            if (corrx * lolnope > 0 && camwych[ti] > -40)
                            {
                                cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * (lolnope * 60));
                                camwych[ti] -= (lolnope * 60);
                            }
                            else if (rt[ti].anchoredPosition.y <= -500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            else if (corrx * lolnope < 0 && camwych[ti]> -40)
                            {
                                cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * ((lolnope * 60) - (corrx)));
                                camwych[ti] -= ((lolnope * 60) - (corrx));
                            }
                        }
                        else if (rt[ti].anchoredPosition.y > 450)
                        {
                            if (corrx * lolnope < 0 && rt[ti].anchoredPosition.y < 500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            if (corrx * lolnope < 0 && camwych[ti] < 50)
                            {
                                cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * (-lolnope * 60));
                                camwych[ti] -= (-lolnope * 60);
                            }
                            else if (rt[ti].anchoredPosition.y >= 500)
                            {
                                rt[ti].transform.Translate(Vector3.up * corrx * lolnope);
                            }
                            else if (corrx * lolnope > 0 && camwych[ti] < 50)
                            {
                                cameras[ti].transform.parent.transform.parent.transform.Rotate(Vector3.right * ((-lolnope * 60) - (corrx)));
                                camwych[ti] -= (-lolnope * 60) - (corrx);
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
                                cbs[ti].transform.Rotate(-Vector3.up * (lolnope * 60));
                            }
                            else if (rt[ti].anchoredPosition.x <= -400)
                            {
                                rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                            }
                            else if (-corrz * lolnope < 0)
                            {
                                cbs[ti].transform.Rotate(-Vector3.up * ((lolnope * 60) - (-corrz)));
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
                                cbs[ti].transform.Rotate(-Vector3.up * (-lolnope * 60));
                            }
                            else if (rt[ti].anchoredPosition.x >= 400)
                            {
                                rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                            }
                            else if (-corrz * lolnope > 0)
                            {
                                cbs[ti].transform.Rotate(-Vector3.up * ((-lolnope * 60) - (-corrz)));
                            }
                        }
                        else
                        {
                            rt[ti].transform.Translate(Vector3.right * -corrz * lolnope);
                        }
                        trranstest[ti] = Vector3.forward * -corrz * lolnope;
                        trranstest[ti] += Vector3.right * -corrz * lolnope;
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
                        Transform tmpp = handstart[ti].parent;
                        handstart[ti].parent = cube[ti].transform.parent;
                        cube[ti].transform.position = handstart[ti].position;
                        handstart[ti].parent = tmpp;
                        rt[ti].anchoredPosition = startrttr[ti];
                        cube[ti].transform.rotation = handstart[ti].rotation;
                        rt[ti].localRotation = startrtrot[ti];
                        if (archerrr[ti] && tmpx.x >= 0.8)
                        {
                            cube[ti].transform.Rotate(Vector3.up * 90);
                            rt[ti].Rotate(Vector3.forward * -90);
                        }
                        else if(archerrr[ti] && tmpx.x <= -0.8)
                        {
                            cube[ti].transform.Rotate(Vector3.up * -90);
                            rt[ti].Rotate(Vector3.forward * 90);
                        }
                    }else if (archerrr[ti]&& rhm.tc[0].phase == 3 && rhm.tc[0].tc == 3 && notmoved[ti])
                    {
                        archerrr[ti] = false;
                    }else if (!archerrr[ti] && rhm.tc[0].phase == 3 && rhm.tc[0].tc == 3 && notmoved[ti]&&(tmpx.x>=0.9 || tmpx.x<=-0.9))
                    {
                        archerrr[ti] = true;
                    }
                    lastphase[ti] = rhm.tc[0].phase;
                }
            }
        }
    }
}
