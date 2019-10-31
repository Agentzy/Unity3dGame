using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    public GameObject player;//相机跟随玩家
    public float speed = 5f;//相机跟随的速度
    Vector3 offset;//相机和玩家的相对位置

    void Start(){
        offset = transform.position - player.transform.position;
    }

    //相机位置刷新
    void FixedUpdate(){
        Vector3 target = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
