using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour{
    //飞碟初始位置
	public Vector3 direction { 
		get { 
			return direction; 
		} 
		set { 
			gameObject.transform.Rotate(value); 
		} 
	}

	public Vector3 StartPoint {get{return gameObject.transform.position; } set { gameObject.transform.position = value;}}//开始点
    public Color color {get{return gameObject.GetComponent<Renderer>().material.color;}set{gameObject.GetComponent<Renderer>().material.color = value;}}//颜色
    public float Speed {get;set;}//速度
	public int score {get;set;}//得分
}