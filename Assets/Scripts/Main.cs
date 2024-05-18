using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    [SerializeField] Panel_Login panel_Login;

    [SerializeField] RoleEntity role;

    GameContext ctx;
    void Awake() {
        ctx = new GameContext();
        ctx.roleEntity = role;
        
        panel_Login.Ctor();
        panel_Login.OnNewGameHandle = () => {
            GameBussiness.NewGame(ctx);
            // if (ctx.roleEntity == null) {
            //     Debug.LogError("RoleEntity is null");
            // } else {
            //     Debug.Log("RoleEntity is not null");
            // }
            panel_Login.Hide();
        };

        panel_Login.OnLoadGameHandle = () => {
            GameBussiness.LoadGame(ctx);
            panel_Login.Hide();
        };
        Debug.Log("Hello World!");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameBussiness.SaveGame(ctx);
        }
    }
}
