using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFollowAction : SSAction{
    private float speed = 2f;//巡逻兵速度
    private GameObject player;//追踪目标

    private PropFollowAction() { }
    public static PropFollowAction getAction(GameObject player){
        PropFollowAction action = CreateInstance<PropFollowAction>();
        action.player = player;
        return action;
    }

    public override void Update(){
         
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);
        //如果侦察兵没有跟随对象，或者需要跟随的玩家不在侦查兵的区域内
        if (!this.gameObject.GetComponent<Prop>().is_follow || this.gameObject.GetComponent<Prop>().player_pos != this.gameObject.GetComponent<Prop>().block){
            this.destroy = true;
            this.callback.SSActionEvent(this,1,this.gameObject);
        }
    }
    public override void Start(){
    }
}
