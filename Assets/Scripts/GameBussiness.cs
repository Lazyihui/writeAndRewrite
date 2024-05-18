using System;
using System.Collections;
using UnityEngine;
public static class GameBussiness {

    public static void NewGame(GameContext ctx) {
        Vector2 pos = new Vector2(5, 5);
        if (ctx.roleEntity == null) {
            Debug.LogError("RoleEntity is null");
        }
        ctx.roleEntity.SetPos(pos);
    }

    public static void LoadGame(GameContext ctx) {
        Debug.Log("LoadGame");
    }

    public static void SaveGame(GameContext ctx) {
        Debug.Log("SaveGame");
    }

}