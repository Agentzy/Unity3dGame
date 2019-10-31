using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollide : MonoBehaviour{
    public int block = 0;
    FirstSceneController controller;
    private void Start(){
        controller = SSDirector.getInstance().currentScenceController as FirstSceneController;
    }
    void OnTriggerEnter(Collider collider){
        //玩家进入该区域标记玩家当前区域
        if (collider.gameObject.tag == "Player"){
            controller.player_pos = block;
        }
    }
}
