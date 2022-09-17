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
    public static bool CheckCircleRectInsert1(Vector2 rectPos, Vector2 circlePos, Vector2 halfSize, float radius)
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
    public static bool CheckCircleRectInsert2(Vector2 rectPos, Vector2 circlePos, Vector2 halfSize, float radius)
    {
        // ��1����ת������1����
        Vector2 v = new Vector2(Mathf.Abs(rectPos.x - circlePos.x), Mathf.Abs(rectPos.y - circlePos.y));
        //��2������Բ�������ε���̾���ʸ��
        Vector2 u = Vector2.Max(v - halfSize, Vector2.zero);
        // ��3��������ƽ����뾶ƽ���Ƚ�
        return Vector2.Dot(u, u) <= radius * radius;
    }

    #endregion
}
