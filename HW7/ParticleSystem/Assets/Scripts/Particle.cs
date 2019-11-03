using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Particle : MonoBehaviour {

    public ParticleSystem particleSystem;//粒子系统
    private ParticleSystem.Particle[] particleArray;//粒子数组
    private int particleNum = 10000;//粒子总数
	//public Gradient colorGradient;//改变颜色
    private float minRadius = 2.0f;//小半径
    private float maxRadius = 15.0f;//大半径
    private float[] angle;//粒子角度
	private float[] circleExpend;//扩大后的半径
    private int level = 5;//旋转速度等级
	private int cnt = 0;//计算帧数
    private float speed = 0.1f;//粒子旋转速度

    // Use this for initialization
    void Start () {
		particleSystem.maxParticles = particleNum;//粒子总数设定
		particleArray = new ParticleSystem.Particle[particleNum];//申请数组空间
		particleSystem.Emit(particleNum);//发射粒子
		particleSystem.GetParticles(particleArray);//生成粒子并存入数组
        angle = new float[particleNum];
		circleExpend = new float[particleNum];
		//初始化粒子位置
        for (int i = 0; i < particleNum; i++) {
			//从半径中心随机让粒子分布在中心半径附近
            float midR = (maxRadius + minRadius) / 2;
            float minR = Random.Range(minRadius, midR);
            float maxR = Random.Range(midR, maxRadius);
            float r = Random.Range(minR, maxR);
            float angleinit = Random.Range(0.0f, 360.0f);
            angle[i] = angleinit;
			circleExpend[i] = r;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < particleNum; i++){
            if (i % 2 == 0){
                angle[i] += (i % level + 1) * speed;
            }
            else{
                angle[i] -= (i % level + 1) * speed;
            }
            angle[i] = angle[i] % 360;
            float rad = angle[i] / 180 * Mathf.PI;
			if(cnt < 150){
                circleExpend[i] += Time.deltaTime * 4;//放大
            }
			else{
				circleExpend[i] -= Time.deltaTime * 5;//收缩
			}
            particleArray[i].position = new Vector3(circleExpend[i] * Mathf.Cos(rad), circleExpend[i] * Mathf.Sin(rad), 0);
			
        }
        particleSystem.SetParticles(particleArray, particleNum);
		cnt = cnt+1;
		if(cnt == 300)
			cnt = 0;
    }

}