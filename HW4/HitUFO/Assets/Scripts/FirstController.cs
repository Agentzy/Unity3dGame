using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myinterface;

public class FirstController : MonoBehaviour, ISceneController, UserAction{

	public CCFlyActionManager fly_manager;
	public DiskFactory DF;
	public UserGUI user_gui;
	public ScoreManager score_manager;
	public int round = 1;
	public bool start = false;//游戏开始
	public bool over = false;//游戏结束
	private int score2 = 15;//升级分数
	private int score3 = 30;
	private List<Disk> disk_notshot = new List<Disk>();          //没有被打中的飞碟队列

	// Use this for initialization
	void Start () {
		SSDirector director = SSDirector.getInstance();
		director.currentScenceController = this;
		DF = DiskFactory.diskfactory;
		
		//score_manager = new ScoreManager();
		score_manager = Singleton<ScoreManager>.Instance;
		/*if(score_manager == null){
			Debug.Log("333");
		}*/
		fly_manager = gameObject.AddComponent<CCFlyActionManager>() as CCFlyActionManager;
		user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
        //start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(start == true){
			Disk d = DF.GetDisk(round);
			fly_manager.UFOfly(d);
			disk_notshot.Add(d);//加入未打中的对列并检测
			for (int i = 0; i < disk_notshot.Count; i++){
				Disk temp = disk_notshot[i];
				//飞碟飞出摄像机视野也没被打中
				if (temp.transform.position.y < -5 && temp.gameObject.activeSelf == true){
					//DF.FreeDisk(disk_notshot[i]);
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
		bool not_hit = false;
        for (int i = 0; i < hits.Length; i++){
            RaycastHit hit = hits[i];
			//击中
            if (hit.collider.gameObject.GetComponent<Disk>() != null){
                not_hit = true;
            }
			if(!not_hit)
				return;
			score_manager.Record(hit.collider.gameObject.GetComponent<Disk>());
			//显示爆炸粒子效果
			Transform explode = hit.collider.gameObject.transform.GetChild(0);
			explode.GetComponent<ParticleSystem>().Play();
			StartCoroutine(WaitingParticle(0.08f, hit, DF, hit.collider.gameObject));
		}
	}

	//暂停几秒后回收飞碟
    IEnumerator WaitingParticle(float wait_time, RaycastHit hit, DiskFactory disk_factory, GameObject d)
    {
        yield return new WaitForSeconds(wait_time);
        //等待之后执行的动作  
        hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
        //DF.FreeDisk(d);
    }
	public int getScore(){
		return score_manager.score;
	}
    public void reStart(){
		score_manager.score = 0;
        round = 1;
		start = true;
	}
}
