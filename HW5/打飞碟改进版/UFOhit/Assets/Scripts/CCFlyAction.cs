using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//飞碟飞行动作
public class CCFlyAction : SSAction {

	public float speedx;
	public float speedy = 0;//y方向初速度
	public float gravity = 10;//添加重力

	// Use this for initialization
	public override void Start () {}

	public static CCFlyAction getAction(float speed){
		CCFlyAction action = CreateInstance<CCFlyAction>();
		action.speedx = speed;//初始化初速度
		return action;
	}
	
	// Update is called once per frame
	public override void Update () {
		// x=vx*t y = Vy*t+1/2gt^2
		this.transform.position += new Vector3(speedx*Time.deltaTime, -speedy * Time.deltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime,0);
		speedy += gravity*Time.deltaTime;//类平抛运动
		//模拟飞碟飞行完成没有被击毁,注意要比扣血的y小
		if(transform.position.y <= -8){
			destory = true;
			callback.SSActionEvent(this);//反馈
		}
	}
}