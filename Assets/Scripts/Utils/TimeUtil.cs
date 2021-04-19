using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeUtil
{

	/// <summary>
	/// 把秒数转换成时：分：秒字符串
	/// </summary>
	/// <param name="seconds">秒数</param>
	/// <returns></returns>
	public static string GetTime(int seconds)
	{
		int hrs = seconds / 60 / 60;
		int min = seconds / 60 % 60;
		int sec = seconds % 60;
		return string.Format("{0:00}:{1:00}:{2:00}", hrs, min, sec);
	}
	
	// 倒计时协程
	public static IEnumerator<WaitForSeconds> CountDown(int timeCountDown, Text countDownText, Action callBack)
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			timeCountDown--;
			string timeLeft = GetTime(timeCountDown);
			countDownText.text = "Refresh time:" + timeLeft;
            
            
			if (timeCountDown <= 0)
			{
				callBack.Invoke();
				yield break;
			}
		}
	}
}