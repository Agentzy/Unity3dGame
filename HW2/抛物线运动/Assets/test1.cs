using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    private float xspeed = 8;//x方向初速度
    private float zspeed = 0;//z方向初速度
    private float g = (float)9.8;//重力加速度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * xspeed;//横向x = v0*t
        this.transform.position += Vector3.down * Time.deltaTime * zspeed;
        zspeed += g*Time.deltaTime;
    }
}
