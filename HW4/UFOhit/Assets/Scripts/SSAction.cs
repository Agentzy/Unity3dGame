using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myinterface;

public class SSAction : ScriptableObject {

	public bool enable = true; //动作是否正在进行
	public bool destory = false;//动作是否需要销毁
	public GameObject gameObject;//控制的游戏对象
	public Transform transform; //游戏对象的transform
	public ISSActionCallback callback; //动作处理接口
	
	protected SSAction(){} //保证SSAction不会被new
	// Use this for initialization
	public virtual void Start () {
		throw new System.NotImplementedException();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		throw new System.NotImplementedException();
	}
}