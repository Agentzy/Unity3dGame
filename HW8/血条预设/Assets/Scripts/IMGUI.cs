using UnityEngine.UI;
using UnityEngine;

public class IMGUI : MonoBehaviour {
    public float blood;
    private float result;
    private Rect bloodBar;
    private Rect addBtn;
    private Rect downBtn;

    void Start () {
        // 初始化
        blood = 100;
        result = 100;
        bloodBar = new Rect((Screen.width - 350), 100, 200, 20);
        addBtn = new Rect((Screen.width - 350), 120, 60, 20);
        downBtn = new Rect((Screen.width - 200), 120, 60, 20);
    }

    void OnGUI () {
        if (GUI.Button(addBtn, "add"))
            result = blood + 10 > 100 ? 100 : blood + 10;
        if (GUI.Button(downBtn, "reduce"))
            result = blood - 10 < 0 ? 0 : blood - 10;
        GUI.color = new Color(1.0f, 0f, 0f, 0.5f);
        blood = Mathf.Lerp(blood, result, 0.05f);//更新血条
        GUI.HorizontalScrollbar(bloodBar, 0, blood, 0, 100);
    }

}
