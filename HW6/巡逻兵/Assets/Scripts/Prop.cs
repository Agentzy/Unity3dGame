using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour{
    public int block;//巡逻兵所在区域
    public bool is_follow = false;//是否追踪
    public int player_pos = -1;//玩家所在区域
    public GameObject player;//追踪玩家
    public Vector3 start_position;//初始位置  
    
     //防止碰撞后旋转
	void Update () {
        if (this.gameObject.transform.localEulerAngles.x != 0 || gameObject.transform.localEulerAngles.z != 0){
            gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localEulerAngles.y, 0);
        }
        if (gameObject.transform.position.y != 0){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }
	}
}
