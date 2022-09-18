using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class CircleRectIntersect : MonoBehaviour
{
    public Vector2 RectPos;
    public float Width = 5f;
    public float Height = 3f;
    public float Rot = 45;

    public float Radius =0.5f;

    public Transform ball;
    public Transform quad;

    private Material quadMat;
    private float halfW, halfH;

    private void Awake()
    {
        quadMat = quad.GetComponent<MeshRenderer>().material;
    }

    [ContextMenu("Stopwatch")]
    void Start()
    {
        Stopwatch sw = new Stopwatch();


        // ------------------ 测试两种方法执行效率
        var cnt = 100000;
        sw.Start();
        for (int i = 0; i < cnt; i++)
        {
            //YHMath.CheckCircleRectInsert1(quad.position, new Vector2(halfW, halfH), ball.position, Radius);
            YHMath.CheckCircleRectRotInsert(quad.position, new Vector2(halfW, halfH), Rot, ball.position, Radius);
        }
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);

        sw.Restart();
        for (int i = 0; i < cnt; i++)
        {
            //YHMath.CheckCircleRectInsert2(quad.position, new Vector2(halfW, halfH), ball.position, Radius);
            YHMath.CheckCircleRectRotInsert2(quad.position, new Vector2(halfW, halfH), Rot, ball.position, Radius);
        }
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);


        Check();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var pos = Input.mousePosition;
            ball.position = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, -transform.position.z));

            Check();
        }
    }

    private void Check()
    {
        halfW = Width * 0.5f;
        halfH = Height * 0.5f;

        bool insert;
        //insert = YHMath.CheckCircleRectInsert1(quad.position, new Vector2(halfW, halfH), ball.position, Radius);
        //insert = YHMath.CheckCircleRectInsert2(quad.position, new Vector2(halfW, halfH), ball.position, Radius);

        //------------- 带旋转的检测
        //insert = YHMath.CheckCircleRectRotInsert(quad.position, new Vector2(halfW, halfH), Rot, ball.position, Radius);
        insert = YHMath.CheckCircleRectRotInsert2(quad.position, new Vector2(halfW, halfH), Rot, ball.position, Radius);

        if (quadMat != null)
            quadMat.color = insert ? Color.yellow : Color.white;
    }

    private void OnValidate()
    {
        ball.localScale = new Vector3(Radius*2, Radius*2, 1);
        quad.position = new Vector3(RectPos.x, RectPos.y, 0);
        quad.eulerAngles = new Vector3(0, 0, Rot);
        quad.localScale = new Vector3(Width, Height, 1);

        Check();
    }
}
