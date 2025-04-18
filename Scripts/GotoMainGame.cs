using UnityEngine;
using UnityEngine.SceneManagement; 

public class GotoMainGame : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
