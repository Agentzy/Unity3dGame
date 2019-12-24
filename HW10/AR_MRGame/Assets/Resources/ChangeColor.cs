using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;

public class ChangeColor : MonoBehaviour, IVirtualButtonEventHandler
{
 
    public int index;
    public GameObject cube;
    public GameObject btn;
    public Color[] colors = {Color.blue, Color.red, Color.green, Color.black};

    void Start() {
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++) {
            vbs[i].RegisterEventHandler(this);
        }
        index = 0;
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb) {
        index++;
        btn.GetComponent<MeshRenderer>().material.color = Color.red;
        cube.GetComponent<Renderer>().material.color = colors[index%4];

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb) {
        btn.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}