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
#### 方法1：<br>
![image](Assets/001CircleRectIntersect/CircleRectInsert.png)
  
#### 方法2：<br>
设c为矩形中心，h为矩形半長，p为圆心，r为半径。<br>
方法是计算圆心与矩形的最短距离 u，若 u 的长度小于 r 则两者相交。<br>
这种方法比较抽象<br>
[参考知乎答案](https://www.zhihu.com/question/24251545) <br>
<br>

