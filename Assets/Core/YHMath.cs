using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YHMath
{
    #region Բ������ཻ
    /// <summary>
    /// �ж�Բ������ཻ�������ھ����б�ƽ��������������
    /// </summary>
    /// <param name="rectPos"></param>
    /// <param name="circlePos"></param>
    /// <param name="halfSize"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool CheckCircleRectInsert1(Vector2 rectPos, Vector2 halfSize, Vector2 circlePos, float radius)
    {
        //����1 �ս�
        float minx, miny;
        //�ҳ���Բ������Ķ����xֵ
        minx = Mathf.Min(Mathf.Abs(rectPos.x + halfSize.x - circlePos.x), Mathf.Abs(rectPos.x - halfSize.x - circlePos.x));
        //�ҳ���Բ������Ķ����yֵ
        miny = Mathf.Min(Mathf.Abs(rectPos.y + halfSize.y - circlePos.y), Mathf.Abs(rectPos.y - halfSize.y - circlePos.y));
        if (minx * minx + miny * miny <= radius * radius) return true;

        //����2 ���  
        float lenx = Mathf.Abs(rectPos.x - circlePos.x);
        float leny = Mathf.Abs(rectPos.y - circlePos.y);
        if (lenx < halfSize.x + radius && leny < halfSize.y)
            return true;
        if (leny < halfSize.y + radius && lenx < halfSize.x)
            return true;

        return false;
    }

    /// <summary>
    /// �ж�Բ������ཻ�������ھ����б�ƽ��������������
    /// </summary>
    /// <param name="rectPos"></param>
    /// <param name="circlePos"></param>
    /// <param name="halfSize"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool CheckCircleRectInsert2(Vector2 rectPos, Vector2 halfSize, Vector2 circlePos, float radius)
    {
        // ��1����ת������1����
        Vector2 v = new Vector2(Mathf.Abs(rectPos.x - circlePos.x), Mathf.Abs(rectPos.y - circlePos.y));
        //��2������Բ�������ε���̾���ʸ��
        Vector2 u = Vector2.Max(v - halfSize, Vector2.zero);
        // ��3��������ƽ����뾶ƽ���Ƚ�
        return Vector2.Dot(u, u) <= radius * radius;
    }

    /// <summary>
    /// ��Բ������ת�������οռ䣨����ԭ���ھ������ģ�������ƽ���ھ��εıߣ�
    /// </summary>
    /// <param name="rectPos"></param>
    /// <param name="halfSize"></param>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public static bool CheckCircleRectRotInsert(Vector2 rectPos,  Vector2 halfSize, float rectRot, Vector2 circlePos, float radius)
    {
        //��������3x3����
        var mat3x3 = Get2DTRMatrix(rectPos, rectRot);
        // ----------------
        // [row0]            
        // [row1]    ���    [circlePos, 1] ��ת�ã�
        // [0,0,1]
        float x = mat3x3.V00 * circlePos.x + mat3x3.V01 * circlePos.y + mat3x3.V02;
        float y = mat3x3.V10 * circlePos.x + mat3x3.V11 * circlePos.y + mat3x3.V12;
        Vector2 circleLocalPos = new Vector2(x,y);

        // ע������ת���˾��α������꣬�Ǿ������ĵ����(0,0)��
        return CheckCircleRectInsert2(Vector2.zero, halfSize, circleLocalPos, radius);
    }

    //public static 

    #endregion

    #region �任
    /// <summary>
    /// 2dƽ���ϵı任����
    /// ֻ�任ƽ�ƺ���ת
    /// </summary>
    /// <param name="pos">�ֲ�����ԭ��������ռ������ </param>
    /// <param name="rot">�ֲ�����ϵ������ռ����ת</param>
    public static Matrix3x3 Get2DTRMatrix(Vector2 pos, float rot)
    {
        var cos = Mathf.Cos(Mathf.Deg2Rad * rot);
        var sin = Mathf.Sin(Mathf.Deg2Rad * rot);
        var row0 = new Vector3(cos, -sin, pos.x);
        var row1 = new Vector3(sin, cos, pos.y);
        var row2 = new Vector3(0, 0, 1);
        Matrix3x3 matrix = Matrix3x3.CreateFromRows(row0, row1, row2).Inverse();
        return matrix;
    }

    #endregion
}
