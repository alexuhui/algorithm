using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExFun
{
	public static Vector3 Inverse(this Vector3 v3)
	{
		var x = (v3.x == 0f) ? 0f : (1f / v3.x);
		var y = (v3.y == 0f) ? 0f : (1f / v3.y);
		var z = (v3.z == 0f) ? 0f : (1f / v3.z);
		return new Vector3(x, y, z);
	}
}
