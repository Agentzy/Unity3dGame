using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory {
    public GameObject disk = null;

    private List<Disk> used = new List<Disk>();//正在使用的飞碟
	private List<Disk> free = new List<Disk>();//未被激活的飞碟
    public static DiskFactory diskfactory = new DiskFactory();

    public DiskFactory(){
        //Debug.Log("333");
        disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk2"));
        disk.AddComponent<Disk>();
        disk.SetActive(false);
    }

    public Disk GetDisk(int round){
        FreeDisk();
        float random_speed;//飞碟速度
		int random_num;//飞碟数量
		int _score;//飞碟得分
        //根据round随机飞碟数量和速度
        if (round == 1){
            random_num = Random.Range(0, 3);
            random_speed = Random.Range(20, 30);
        }
        else if (round == 2){
            random_num = Random.Range(0, 5);
            random_speed = Random.Range(30, 40);
        }
        else {
            random_num = Random.Range(0, 7);
            random_speed = Random.Range(40, 50);
        } 
        //设定飞船属性
		Vector3 random_start;
		Color _color;
        Debug.Log("33");
		if(random_num <= 3){
			_score = 1;
			random_start = new Vector3(Random.Range(-100, -70), Random.Range(30, 70), Random.Range(100, 120));
			_color = Color.white; 
		}
        else if(random_num <= 5 && random_num > 3){
			_score = 3;
			random_start = new Vector3(Random.Range(100, 130), Random.Range(30, 80), Random.Range(110, 130));
			_color = Color.black;
		}
        else{
			_score = 5;
			random_start =  new Vector3(Random.Range(100, 130), Random.Range(30, 90), Random.Range(110, 130));
			_color = Color.red;
		}

		//从空闲列表中获取同类型
		GameObject diskdata = null;
        if (free.Count > 0){
            diskdata = free[0].gameObject;
            free.Remove(free[0]);
        }

		else{
            Debug.Log("333");
			diskdata = GameObject.Instantiate<GameObject>(disk, Vector3.zero, Quaternion.identity);
		}
		diskdata.SetActive(true);
        Disk newdisk;
        newdisk = diskdata.AddComponent<Disk>();
		//添加属性
		newdisk.Speed = random_speed;
		float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
		newdisk.direction = new Vector3(RanX, 1, 0);
		newdisk.score = _score;
		newdisk.StartPoint = random_start;
		newdisk.color = _color;
		used.Add(newdisk); //添加到使用列表
        return newdisk; 
    }

    //飞碟回收
	public void FreeDisk(){
		for(int i = 0;i < used.Count;i ++){
			//如果是要回收的
            if(!used[i].gameObject.activeSelf){
				free.Add(used[i]);
				used.Remove(used[i]);
				break;
			}
		}
    }
}
