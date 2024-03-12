using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class Web : MonoBehaviour
{
    //IP server: 192.168.1.96

    public static Web Instance { get; private set; }
    public string url = "http://192.168.1.96/Unity/";

    public void Awake()
    {
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetUsers("http://localhost/Unity/GetUser.php"));
        //StartCoroutine(Login("http://localhost/Unity/Login.php", "binhevip", "123"));
        //StartCoroutine(Register("http://localhost/Unity/Register.php", "binhevip2", "123"));
    }

    public IEnumerator GetUsers(TextMeshProUGUI textMeshPro)
    {
        string uri = "http://localhost/Unity/GetUser.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    textMeshPro.text = ": Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    textMeshPro.text = ": HTTP Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                    textMeshPro.text = ": Received: " + webRequest.downloadHandler.text;
                    break;
            }
        }
    }

    public IEnumerator Login(string loginUser, string password, TextMeshProUGUI notify)
    {
        string uri = "http://localhost/Unity/Login.php";
        WWWForm form = new WWWForm();
        form.AddField("loginUser", loginUser);
        form.AddField("loginPassword", password);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    notify.text = ": Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    notify.text = ": HTTP Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                    notify.text = ": Received: " + webRequest.downloadHandler.text;
                    // Kiểm tra xác thực ở đây, dựa trên nội dung của webRequest.downloadHandler.text
                    if (webRequest.downloadHandler.text.Contains("success"))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    break;
            }
        }
    }

    public IEnumerator Register(TextMeshProUGUI textMeshPro, string loginUser, string password)
    {
        string uri = "http://localhost/Unity/Register.php";
        WWWForm form = new WWWForm();
        form.AddField("loginUser", loginUser);
        form.AddField("loginPassword", password);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    textMeshPro.text = ": Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    textMeshPro.text = ": HTTP Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                    textMeshPro.text = ": Received: " + webRequest.downloadHandler.text;
                    break;
            }
        }
    }
}
