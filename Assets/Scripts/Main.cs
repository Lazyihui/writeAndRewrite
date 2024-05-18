using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    [SerializeField] Panel_Login panel_Login;

    GameContext ctx;
    void Start() {

        panel_Login.Ctor();
        panel_Login.OnNewGameHandle = () => {
            GameBussiness.NewGame();
            panel_Login.Hide();
        };

        panel_Login.OnLoadGameHandle = () => {
            GameBussiness.LoadGame();
            panel_Login.Hide();
        };
        ctx = new GameContext();
        Debug.Log("Hello World!");
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            GameBussiness.SaveGame();
        }
    }
}
