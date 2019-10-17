using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myinterface;

public class FirstController : MonoBehaviour, ISceneController, UserAction{

	public IActionManager fly_manager1,fly_manager2;
	public DiskFactory DF;
	public UserGUI user_gui;
	public ScoreManager score_manager;
	public int round = 1;
	public bool start = false;//游戏开始
	public bool over = false;//游戏结束
	private int score2 = 15;//升级分数
	private int score3 = 30;
	public int action_type = 0;
	private List<Disk> disk_notshot = new List<Disk>();          //没有被打中的飞碟队列

	// Use this for initialization
	void Start () {
		SSDirector director = SSDirector.getInstance();
		director.currentScenceController = this;
		DF = DiskFactory.diskfactory;
		score_manager = Singleton<ScoreManager>.Instance;
		user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
		fly_manager1 = gameObject.AddComponent<CCFlyActionManager>() as CCFlyActionManager;
		fly_manager2 = gameObject.AddComponent<PhysicalActionManager>() as PhysicalActionManager;
	}
	
	// Update is called once per frame
	int count = 0;
	void Update () {
		//出现飞碟的时间延迟
		int val = 0;
        switch (round){
            case 1:
                val = Random.Range(150, 180);
                break;
            case 2:
                val = Random.Range(100, 150);
                break;
            case 3:
                val = Random.Range(80, 100);
                break;
        }
		if(start == true){
			count ++;
			if(count >= val){
				GameObject d = DF.GetDisk(round);
				if(action_type == 0){
					fly_manager2.UFOfly(d);
				}	
				else{
					fly_manager1.UFOfly(d);
				}
				disk_notshot.Add(d.GetComponent<Disk>());//加入未打中的对列并检测
				count = 0;
			}
			for (int i = 0; i < disk_notshot.Count; i++){
				Disk temp = disk_notshot[i];
				//飞碟飞出摄像机视野也没被打中
				if (temp.transform.position.y <= -6 && temp.gameObject.activeSelf == true){
					disk_notshot.Remove(disk_notshot[i]);
					//玩家血量-1
					user_gui.ReduceBlood();
				}
        	}
			if (score_manager.score >= score2 && round == 1){
				round++;
			}
			if (score_manager.score >= score3 && round == 2){
				round++;
			}
        }
	}

	public void LoadResources(){
        
    }

    public int getRound(){
        return round;
    }

	public void hit(Vector3 pos){
		Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
		//Debug.Log("444");
        for (int i = 0; i < hits.Length; i++){
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<Disk>() != null){
                score_manager.Record(hit.collider.gameObject);
				//Debug.Log("444");
				StartCoroutine(WaitingParticle(0.08f, hit, DF, hit.collider.gameObject));
				break;
            }
        }
	}

	//暂停几秒后回收飞碟
    IEnumerator WaitingParticle(float wait_time, RaycastHit hit, DiskFactory disk_factory, GameObject d)
    {
        yield return new WaitForSeconds(wait_time);
        //等待之后执行的动作  
        hit.collider.gameObject.transform.position = new Vector3(0, -10, 0);
        DF.FreeDisk(d);//释放GameObject否则血量也会减少
    }
	public int getScore(){
		return score_manager.score/2;
	}
    public void reStart(){
		score_manager.score = 0;
        round = 1;
		start = true;
	}

	public void endGame(){
		start = false;
	}

	public void chooseAction(int choose){
		action_type = choose;
	}
}
