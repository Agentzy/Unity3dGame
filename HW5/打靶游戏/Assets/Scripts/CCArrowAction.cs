using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//箭飞行动作
public class CCArrowAction : SSAction {
    Vector3 force;
    public float wind;
    public static CCArrowAction GetSSAction(){
        CCArrowAction action = ScriptableObject.CreateInstance<CCArrowAction>();
        return action;
    }
    // Use this for initialization
    public override void Start(){
        force = new Vector3(0, 0, 10);
        //添加刚体
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;  
        gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}

    // Update is called once per frame
    public override void Update(){
        wind = Random.Range(-5f,5f);//添加风力
        //风的力持续作用在箭身上
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(wind,0.2f,2), ForceMode.Force);

        //检测是否被击中或是超出边界
        if (this.transform.position.z > 20 && this.gameObject.GetComponent<Arrow>().hit == false){
            this.destory = true;
            this.callback.SSActionEvent(this);
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }

    public void Destory(){
        this.destory = true;
        this.callback.SSActionEvent(this);
        Destroy(gameObject.GetComponent<BoxCollider>());
    }
}