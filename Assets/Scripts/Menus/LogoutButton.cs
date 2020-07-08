using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour
{
    public void Logout()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
