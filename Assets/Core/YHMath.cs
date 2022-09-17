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
    public static bool CheckCircleRectInsert1(Vector2 rectPos, Vector2 halfSize, Vector2 circlePos, float radius)
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
    public static bool CheckCircleRectInsert2(Vector2 rectPos, Vector2 halfSize, Vector2 circlePos, float radius)
    {
        // 第1步：转换至第1象限
        Vector2 v = new Vector2(Mathf.Abs(rectPos.x - circlePos.x), Mathf.Abs(rectPos.y - circlePos.y));
        //第2步：求圆心至矩形的最短距离矢量
        Vector2 u = Vector2.Max(v - halfSize, Vector2.zero);
        // 第3步：长度平方与半径平方比较
        return Vector2.Dot(u, u) <= radius * radius;
    }

    /// <summary>
    /// 把圆心坐标转换到矩形空间（坐标原点在矩形中心，坐标轴平行于矩形的边）
    /// </summary>
    /// <param name="rectPos"></param>
    /// <param name="halfSize"></param>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public static bool CheckCircleRectRotInsert(Vector2 rectPos,  Vector2 halfSize, float rectRot, Vector2 circlePos, float radius)
    {
        //按行排列3x3矩阵
        var mat3x3 = Get2DTRMatrix(rectPos, rectRot);
        // ----------------
        // [row0]            
        // [row1]    点乘    [circlePos, 1] （转置）
        // [0,0,1]
        float x = mat3x3.V00 * circlePos.x + mat3x3.V01 * circlePos.y + mat3x3.V02;
        float y = mat3x3.V10 * circlePos.x + mat3x3.V11 * circlePos.y + mat3x3.V12;
        Vector2 circleLocalPos = new Vector2(x,y);

        // 注意这里转到了矩形本地坐标，那矩形中心点就是(0,0)了
        return CheckCircleRectInsert2(Vector2.zero, halfSize, circleLocalPos, radius);
    }

    //public static 

    #endregion

    #region 变换
    /// <summary>
    /// 2d平面上的变换矩阵
    /// 只变换平移和旋转
    /// </summary>
    /// <param name="pos">局部坐标原点在世界空间的坐标 </param>
    /// <param name="rot">局部坐标系在世界空间的旋转</param>
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
