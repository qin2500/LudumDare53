using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{	
	private PauseScript _pauseScript;
	public void ChangeScene(string sceneName)
	{
		if (_pauseScript.getStatus())
		{
			_pauseScript.Continue();

		}
		SceneManager.LoadScene(sceneName);
	}
	public void Exit()
	{
		Application.Quit ();
	}
}