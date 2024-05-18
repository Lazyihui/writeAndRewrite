using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Login : MonoBehaviour {

    [SerializeField] Button newGameButton;
    [SerializeField] Button loadGameButton;

    public Action OnNewGameHandle;

    public Action OnLoadGameHandle;

    public void Ctor() {
        newGameButton.onClick.AddListener(() => {
            OnNewGameHandle.Invoke();
        });

        loadGameButton.onClick.AddListener(() => {
            OnLoadGameHandle.Invoke();
        });
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

}
