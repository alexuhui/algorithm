using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YHMath
{
    #region 圆与矩形相交
    /// <summary>
    /// 判断圆与矩形相交，适用于矩形有边平行于坐标轴的情况
    /// </summary>
    /// <param name="rectPos"></param>
    /// <param name="circlePos"></param>
    /// <param name="halfSize"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool CheckCircleRectInsert1(Vector2 rectPos, Vector2 circlePos, Vector2 halfSize, float radius)
    {
        //条件1 拐角
        float minx, miny;
        //找出离圆心最近的顶点的x值
        minx = Mathf.Min(Mathf.Abs(rectPos.x + halfSize.x - circlePos.x), Mathf.Abs(rectPos.x - halfSize.x - circlePos.x));
        //找出离圆心最近的顶点的y值
        miny = Mathf.Min(Mathf.Abs(rectPos.y + halfSize.y - circlePos.y), Mathf.Abs(rectPos.y - halfSize.y - circlePos.y));
        if (minx * minx + miny * miny <= radius * radius) return true;

        //条件2 外边  
        float lenx = Mathf.Abs(rectPos.x - circlePos.x);
        float leny = Mathf.Abs(rectPos.y - circlePos.y);
        if (lenx < halfSize.x + radius && leny < halfSize.y)
            return true;
        if (leny < halfSize.y + radius && lenx < halfSize.x)
            return true;

        return false;
    }

    /// <summary>
    /// 判断圆与矩形相交，适用于矩形有边平行于坐标轴的情况
    /// </summary>
    /// <param name="rectPos"></param>
    /// <param name="circlePos"></param>
    /// <param name="halfSize"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool CheckCircleRectInsert2(Vector2 rectPos, Vector2 circlePos, Vector2 halfSize, float radius)
    {
        // 第1步：转换至第1象限
        Vector2 v = new Vector2(Mathf.Abs(rectPos.x - circlePos.x), Mathf.Abs(rectPos.y - circlePos.y));
        //第2步：求圆心至矩形的最短距离矢量
        Vector2 u = Vector2.Max(v - halfSize, Vector2.zero);
        // 第3步：长度平方与半径平方比较
        return Vector2.Dot(u, u) <= radius * radius;
    }

    #endregion
}
