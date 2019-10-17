using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAction : SSAction{
    public float speedx;
    private float gravity = 10f;
    // Use this for initialization
    public override void Start(){
        //添加刚体
        if (!this.gameObject.GetComponent<Rigidbody>()){
            this.gameObject.AddComponent<Rigidbody>();
        }
        //重力和初速度配置
        this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * gravity * 0.5f, ForceMode.Acceleration);
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(speedx, 0, 0), ForceMode.VelocityChange);
    }

    private PhysicalAction(){}
    public static PhysicalAction getAction(float speed){
        PhysicalAction action = CreateInstance<PhysicalAction>();
        action.speedx = speed;
        return action;
    }

    // Update is called once per frame
    override public void Update(){
        if (transform.position.y <= -8)
        {
            Destroy(this.gameObject.GetComponent<Rigidbody>());//移除刚体
            destory = true;
            callback.SSActionEvent(this);//反馈
        }
    }
}