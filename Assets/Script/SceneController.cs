using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string namaScene;
    // public static SceneController instance;

    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("House"))
        {
            Debug.Log("Selamat, kamu sampai di garis finish!");
            ChangeScene(namaScene);
        }
    }
}
