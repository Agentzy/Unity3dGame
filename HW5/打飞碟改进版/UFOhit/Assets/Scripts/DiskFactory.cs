using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory :MonoBehaviour{
    public GameObject disk = null;

    private List<Disk> used = new List<Disk>();//正在使用的飞碟
	private List<Disk> free = new List<Disk>();//未被激活的飞碟
    public static DiskFactory diskfactory = new DiskFactory();

    public DiskFactory(){
        disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"));
        disk.AddComponent<Disk>();
        disk.SetActive(false);
    }

    public GameObject GetDisk(int round){
        FreeDisk();//不会创建太多初始对象
        float random_speed;//飞碟速度
		int random_num;
		int _score;//飞碟得分
        //根据round随机飞碟数量和速度
        if (round == 1){
            random_num = Random.Range(0, 3);
            random_speed = Random.Range(20, 30);
        }
        else if (round == 2){
            random_num = Random.Range(0, 7);
            random_speed = Random.Range(30, 40);
        }
        else {
            random_num = Random.Range(0, 10);
            random_speed = Random.Range(40, 50);
        } 
        //设定飞船属性
		Vector3 random_start;
        GameObject diskdata = null;
        if (free.Count > 0){
            diskdata = free[0].gameObject;
            free.Remove(free[0]);
        }

		else{
			diskdata = GameObject.Instantiate<GameObject>(disk, Vector3.zero, Quaternion.identity);
		}
		diskdata.SetActive(true);
        Color _color;
        //第三轮猜出红色，第二轮出黑色
		if(random_num > 6 && round >= 3){
            _score = 5;
			random_start =  new Vector3(Random.Range(-80, -90), Random.Range(30, 70), 0);
			_color = Color.red;
		}
        else if(random_num <= 6 && random_num > 3 && round >= 2){
			_score = 3;
			random_start = new Vector3(Random.Range(-80, -100), Random.Range(30,70), 0);
            _color = Color.black;
		}
        else {
			_score = 2;
			random_start = new Vector3(Random.Range(-80, -120), Random.Range(30,90), 0);
			_color = Color.yellow; 
		}
		//添加属性
		diskdata.GetComponent<Disk>().Speed = random_speed;
		float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
		diskdata.GetComponent<Disk>().direction = new Vector3(RanX, 1, 0);
		diskdata.GetComponent<Disk>().score = _score;
        diskdata.GetComponent<Disk>().color = _color;
		diskdata.GetComponent<Disk>().StartPoint = random_start;
		used.Add(diskdata.GetComponent<Disk>()); //添加到使用列表
        return diskdata; 
    }

    //飞碟回收
    public void FreeDisk(GameObject disk){
        for(int i = 0;i < used.Count; i++){
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID()){
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                //Destroy(used[i].GetComponent<Disk>());
                used.Remove(used[i]);
                break;
            }
        }
    }
    public void FreeDisk(){
        foreach (Disk x in used){
            if (!x.gameObject.activeSelf){
                free.Add(x);
                //Destroy(x.gameObject.GetComponent<Disk>());
                used.Remove(x);
                return;
            }
        }
    }
}
