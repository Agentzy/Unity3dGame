using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstSceneController : MonoBehaviour, UserAction, ISceneController{
    public PropFactory PF;
    public int player_pos = -1;//玩家当前所在block
    public GameObject player;     
    public ScoreRecorder recorder;
    public PropActionManager action_manager;
    public Camera main_camera;
    private List<GameObject> props;//巡逻兵
    private bool game_over = false;

    void Start(){
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        PF = Singleton<PropFactory>.Instance;
        recorder = Singleton<ScoreRecorder>.Instance;
        action_manager = gameObject.AddComponent<PropActionManager>() as PropActionManager;
        LoadResources();
        /*if(main_camera == null)
            Debug.Log("333");*/
        main_camera.GetComponent<CameraFollow>().player = player;
    }

    void Update(){
        //告知巡逻兵玩家当前所在的位置
        for (int i = 0; i < props.Count; i++){
            props[i].gameObject.GetComponent<Prop>().player_pos = player_pos;
        }
    }

    public void LoadResources(){
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(Resources.Load("Prefabs/player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
        props = PF.GetPatrols();
        //所有侦察兵移动
        for (int i = 0; i < props.Count; i++){
            action_manager.prop_run(props[i]);
        }
    }
    //玩家移动
    public void PlayerMove(float pos_X, float pos_Z){
        if(!game_over && player != null){
            if (pos_X != 0 || pos_Z != 0){
                player.GetComponent<Animator>().SetBool("run", true);
            }
            else{
                player.GetComponent<Animator>().SetBool("run", false);
            }
            //移动和旋转
            player.transform.Translate(0, 0, pos_Z * 5f * Time.deltaTime);
            player.transform.Rotate(0, pos_X*1.5f, 0);
            player.transform.Translate(pos_X * 5f * Time.deltaTime, 0, 0);
            
            //防止碰撞带来的旋转
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0){
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
            if (player.transform.position.y != 0){
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }
        }
    }

    public int getScore(){
        return recorder.getScore();
    }

    public bool isEndGame(){
        return game_over;
    }
    public void reStart(){
        game_over = false;

        recorder.setScore(-1);
        player.GetComponent<Animator>().Play("New State");
        player.transform.position = new Vector3(0, 9, 0);
        PF.PropInit(props);
        for (int i = 0; i < props.Count; i++){
            action_manager.prop_run(props[i]);
        }
    }

    void OnEnable(){
        GameEventManager.addScore += AddScore;
        GameEventManager.GameOver += Gameover;
    }
    void OnDisable(){
        GameEventManager.addScore -= AddScore;
        GameEventManager.GameOver -= Gameover;
    }
    void AddScore(){
        recorder.AddScore();
    }
    void Gameover(){
        game_over = true;
        action_manager.DestroyAllAction();
    }
}
