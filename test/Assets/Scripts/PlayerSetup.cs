using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    public GameObject PlayerUi;
    private GameObject Can;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else {
            Can = Instantiate(PlayerUi);
            GetComponent<PlayerUnit>().joy = Can.GetComponentInChildren<FixedJoystick>();
            Can.GetComponentInChildren<Button>().onClick.AddListener(GetComponent<PlayerUnit>().isJumping);
        }
}

    private void OnDestroy()
    {
        if (isLocalPlayer) {
            Destroy(Can);
        }

    }

}
