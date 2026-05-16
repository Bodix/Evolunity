// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Utilities
{
	public static class AndroidNativeHelper
	{
		public static int GetKeyboardHeight()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject view = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")
						       .Get<AndroidJavaObject>("mUnityPlayer")
						       .Call<AndroidJavaObject>("getView"))
					{
						using (AndroidJavaObject rect = new AndroidJavaObject("android.graphics.Rect"))
						{
							view.Call("getWindowVisibleDisplayFrame", rect);

							return Screen.height - rect.Call<int>("height");
						}
					}
				}
			}
			else return 0;
		}
	}
}