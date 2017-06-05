using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
	void Update ()
    {
        Debug.Log("TEST");
		if (Input.anyKey)
		{
			SceneManager.LoadScene("MainScene");
		}
	}
}
