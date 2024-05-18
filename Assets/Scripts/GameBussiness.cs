using System;
using System.IO;
using System.Collections;
using UnityEngine;
public static class GameBussiness {

    public static void NewGame(GameContext ctx) {


        Vector2 pos = new Vector2(5, 5);
        ctx.roleEntity.SetPos(pos);
    }

    public static void LoadGame(GameContext ctx) {

        Vector2 pos = new Vector2(0, 0);

        pos = LoadType1();
        
        ctx.roleEntity.SetPos(pos);

    }

    public static void SaveGame(GameContext ctx) {


        Vector2 pos = ctx.roleEntity.transform.position;

        // 存到本地
        SaveType1(pos);
    }

    static void SaveType1(Vector2 pos) {
        string x = pos.x.ToString();
        string y = pos.y.ToString();
        string result = x + "\r\n" + y;
        File.WriteAllText("/save.txt", result);
    }
    static Vector2 LoadType1() {
        string[] lines = File.ReadAllLines("/save.txt");
        string x = lines[0];
        string y = lines[1];
        float posX = float.Parse(x);
        float posY = float.Parse(y);
        return new Vector2(posX, posY);
    }
}