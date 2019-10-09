using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//飞碟飞行动作
public class CCFlyAction : SSAction {

	public float speedx;
	public float speedy = 0;//y方向初速度
	public float gravity = 9.8F;//添加重力

	// Use this for initialization
	public override void Start () {
		
	}

	public static CCFlyAction getAction(float speed){
		CCFlyAction action = CreateInstance<CCFlyAction>();
		action.speedx = speed;//初始化初速度
		return action;
	}
	
	// Update is called once per frame
	public override void Update () {
		this.transform.position += new Vector3(speedx*Time.deltaTime, -speedy*Time.deltaTime-0.5F*gravity*Time.deltaTime*Time.deltaTime,0);
		speedy += gravity*Time.deltaTime;//类平抛运动
		//模拟飞碟飞行完成没有被击毁
		if(transform.position.y <= -3){
			destory = true;
			callback.SSActionEvent(this);//反馈
		}
	}
}