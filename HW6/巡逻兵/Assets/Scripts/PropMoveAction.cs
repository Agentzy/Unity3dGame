using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMoveAction : SSAction{
    private enum Dirction { EAST, NORTH, WEST, SOUTH };
    private float pos_x, pos_z;
    private float move_length;//移动的距离
    private float move_speed = 1.5f;//巡逻速度
    private bool move_sign = true;//是否到达目的地
    private Dirction dirction = Dirction.EAST;  //移动的方向
    

    private PropMoveAction(){}
    public static PropMoveAction getAction(Vector3 pos){
        PropMoveAction action = CreateInstance<PropMoveAction>();
        action.pos_x = pos.x;
        action.pos_z = pos.z;
        //设定移动矩形的边长
        action.move_length = Random.Range(4, 7);
        return action;
    }

    public override void Start(){}
    public override void Update(){
        if (move_sign){
            //不需要转向则设定一个目的地，按照矩形移动
            switch (dirction){
                case Dirction.EAST:
                    pos_x -= move_length;
                    break;
                case Dirction.NORTH:
                    pos_z += move_length;
                    break;
                case Dirction.WEST:
                    pos_x += move_length;
                    break;
                case Dirction.SOUTH:
                    pos_z -= move_length;
                    break;
            }
            move_sign = false;
        }
        this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
        float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));
        //当前位置与目的地距离浮点数的比较
        if (distance > 1){
            transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_x, 0, pos_z), move_speed * Time.deltaTime);
        }
        else{
            dirction = dirction + 1;
            if(dirction > Dirction.SOUTH){
                dirction = Dirction.EAST;
            }
            move_sign = true;
        }
        //触发跟随后侦查动作删除
        if (this.gameObject.GetComponent<Prop>().is_follow && this.gameObject.GetComponent<Prop>().player_pos == this.gameObject.GetComponent<Prop>().block){
            this.destroy = true;
            this.callback.SSActionEvent(this,0,this.gameObject);
        }
    }

        
}
