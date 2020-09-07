using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void RestartGame()
	{
		Debug.Log("Restart!");
		Scene activeScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(activeScene.name); // Tek level olduğu için aynısını çağırıyorum
	}
}
