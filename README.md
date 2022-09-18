# algorithm
记录一些开发过程中遇到的算法<br>
note:<br>
unity version 2021.3.5f1c1 <br>
带编号和标题的子文件夹放的是一些例子和一些简单的测试代码<br>
核心代码都在文件夹 [Assets/Core/](/Assets/Core/) 下<br>


## 目录 <br>
****
* [001.CircleRectIntersect判断圆与矩形相交](#001.CircleRectIntersect判断圆与矩形相交)


### 001.CircleRectIntersect判断圆与矩形相交
首先讨论简单的对齐矩形（边和坐标轴平行）和圆相交的情况。
#### 方法1：<br>
这种方法很直觉，很好理解。<br>
首先，找到相交和相离的边界条件。如下图：<br>
想象一下，有个圆圆的轮子绕着矩形滚一圈，圆心的坐标，是不是就是边界条件？<br>
那么，<br>
假设矩形中心在坐标原点的情况下，<br>
1、当圆在拐角时的边界： `|BG| = r`  即  `x^2 + y^2 = r^2`  那么要相交也就是   `x^2 + y^2 < r^2` <br>
拐角必然是离圆心最近的顶点，分别找出离圆心最近的x,y值，即可根据`x^2 + y^2 < r^2`判断。 <br>
2、当圆在矩形上方时： `|EF| = r` 即  `y - halfH = r`  那么相交就是 `y < r + halfH` ;  <br>
显然，当圆在矩形下方时， `-y < r + halfH`; <br>
另外，圆在矩形上方/下方的充要条件是 `- halfW < x < halfW` 即  `|x| < halfW` <br>
3、当圆在在两侧时，思路一样。<br>
综上，<br>
假设矩形中心点rectPos，长宽的一半halfSize，圆心circlePos，半径radius: <br>
```CS
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
```


![image](Assets/001CircleRectIntersect/CircleRectInsert.png)
  
#### 方法2：<br>
设c为矩形中心，h为矩形半長，p为圆心，r为半径。<br>
方法是计算圆心与矩形的最短距离 u，若 u 的长度小于 r 则两者相交。<br>
这种方法比较抽象<br>
[参考知乎答案](https://www.zhihu.com/question/24251545) <br>
<br>

