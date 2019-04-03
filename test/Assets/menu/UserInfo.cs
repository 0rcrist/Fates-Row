using UnityEngine.UI;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public Text username;
    // Start is called before the first frame update
    void Start()
    {
        if (AccountManager.isLoggedIn)
            username.text = AccountManager.LoggedInUserName;
    }

    public void LogOut()
    {
        AccountManager.instance.LogOut();
    }
}
