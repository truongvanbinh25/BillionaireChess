using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private Web web;

    public TMP_InputField userName;
    public TMP_InputField password;
    public TextMeshProUGUI notify;

    private void Start()
    {
        web = Web.Instance;
    }

    public void LoginAccount()
    {
        StartCoroutine(web.Login(userName.text, password.text, notify));
    }
}
