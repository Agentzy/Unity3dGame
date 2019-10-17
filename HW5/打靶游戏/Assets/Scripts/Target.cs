using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public int target_score;//环数
    public CCArrowAction arrowshoot;
    public FirstController controller;
    public void Start() {
        controller = (FirstController)SSDirector.getInstance().currentScenceController;
    }
    void OnTriggerEnter(Collider arrow) {
        if (arrow.gameObject.tag == "Arrow") {
            if (!arrow.gameObject.GetComponent<Arrow>().hit) {
                arrow.gameObject.GetComponent<Arrow>().hit = true;
                target_score = (name[0] - '0');//通过名称控制分数
                controller.score += target_score;

            }
            arrowshoot = (CCArrowAction)arrow.gameObject.GetComponent<Arrow>().action;
            arrow.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;//箭插入箭靶效果  
            arrowshoot.Destory();
        }
    }
}