using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test3 : MonoBehaviour
{
    // Start is called before the first frame update
    private float xspeed = 8;//x方向初速度
    private float zspeed = 0;//z方向初速度
    private float g = (float)9.8;//重力加速度
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(Time.deltaTime * xspeed,-Time.deltaTime * zspeed,0);
        this.transform.Translate(v);
        zspeed += g*Time.deltaTime;
    }
}
