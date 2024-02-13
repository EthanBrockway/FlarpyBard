using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputScript : MonoBehaviour
{
    public GameObject confirmMenuScreen;
    public GameObject signUpScreen;
    public GameObject loginScreen;
   
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField emailInput;
    public int InputSelected;
    public GameObject invalidUsernameorPassword;
    public GameObject invalidEmail;

    private PlayerData _playerData;
    public void Start()
    {
        _playerData = new PlayerData();
    }
   
    public void Update()
    {
      
       if(UnityEngine.Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("yo");
            if (signUpScreen.activeInHierarchy)
            {
                ConfirmPrompt();
            }
            if(confirmMenuScreen.activeInHierarchy)
            {
                CreateUserAccount();
            }else
            {
                LoginUser();
            }
            
        }
        if(UnityEngine.Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("yo");
            InputSelected++;
            if(loginScreen.activeInHierarchy)
            {
              
                if (InputSelected > 1) InputSelected = 0;
                SelectLoginInputField();
            }
            else {
                if (InputSelected > 2) InputSelected = 0;
                SelectCreateUserInputField();
            }
           
            
        }
        void SelectLoginInputField()
        {
            switch (InputSelected)
            {
                case 0:
                    usernameInput.Select();
                    break;
                case 1:
                    passwordInput.Select();
                    break;
                
            }
        }
        void SelectCreateUserInputField()
        {
            switch (InputSelected)
            {
                case 0: emailInput.Select();
                    break;
                case 1: usernameInput.Select();
                    break;
                case 2: passwordInput.Select();
                    break;
            }
        }
    }
    public void EmailSeleceted() => InputSelected = 0;
    public void UsernameSelected() => InputSelected = 1;
    public void PasswordSelected() => InputSelected = 2;

    public void ConfirmPrompt()
    {
        confirmMenuScreen.SetActive(true);
      
    }
    public void SignUpScreen()
    {
        signUpScreen.SetActive(true);
        gameObject.SetActive(false);
        
    }
    public void LoginScreen()
    {
        loginScreen.SetActive(true);
        signUpScreen.SetActive(false);
    }
    public void LoginUser()
    {
        PlayerPrefs.SetString("username", usernameInput.text);
        PlayerPrefs.SetString ("password", passwordInput.text);
        _playerData.username = PlayerPrefs.GetString("username");

        _playerData.password = passwordInput.text;

        
        StartCoroutine(LogicScript.Login(_playerData.Stringify(), result =>
        {
            
            if(result)
            {
                Debug.Log(result);
                StartCoroutine(LogicScript.GetUser(PlayerPrefs.GetString("username"), result => {
                    PlayerPrefs.SetInt("HighScore", result.highscore);
                    PlayerPrefs.SetInt("currentSelectedSkin", result.currentSelectedSkin);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }));
              
            }else
            {
                Debug.Log("Not A Valid Username");
                invalidUsernameorPassword.SetActive(true);
            }
            
        }));
        
    }
    public void CreateUserAccount ()
    {
        PlayerPrefs.SetString("email", emailInput.text);
        PlayerPrefs.SetString("username", usernameInput.text);
        PlayerPrefs.SetString("password", passwordInput.text);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("currentSelectedSkin", 0);
        _playerData.username = PlayerPrefs.GetString("username");
        _playerData.email = PlayerPrefs.GetString("email");
        _playerData.password = PlayerPrefs.GetString("password");
        _playerData.highscore = PlayerPrefs.GetInt("highscore", 0);
        _playerData.currentSelectedSkin = PlayerPrefs.GetInt("currentSelectedSkin", 0);

        StartCoroutine(LogicScript.CreateUser(_playerData.Stringify(), result =>
        {
            if(result) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log(result);
            }
            else
            {
                Debug.Log("Not A Valid Username");
                invalidEmail.SetActive(true);
            }

        }));
       
    }
    public void Cancel()
    {
        confirmMenuScreen.SetActive(false);
        invalidEmail.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        confirmMenuScreen.SetActive(false);
        gameObject.SetActive(false);
    }
    public void quitGame()
    {
        Debug.Log("Quitting!");
        UnityEngine.Application.Quit();
    }
}
