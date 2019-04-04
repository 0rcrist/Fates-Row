using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransfer : MonoBehaviour
{ 
    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void transfer()
    {
        SceneManager.LoadScene(2);

    }   
}
