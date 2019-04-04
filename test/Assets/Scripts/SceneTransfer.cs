using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    public void transfer() {
        SceneManager.LoadScene(2);
        DontDestroyOnLoad(this);
    }
}
