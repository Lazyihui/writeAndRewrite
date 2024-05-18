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

        // pos = LoadType1();
        pos = LoadType2();

        ctx.roleEntity.SetPos(pos);


    }

    public static void SaveGame(GameContext ctx) {


        Vector2 pos = ctx.roleEntity.transform.position;

        // 存到本地
        // SaveType1(pos);
        SaveType2(pos);
    }
    #region SaveType1
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
    #endregion

    #region SaveType2
    static void SaveType2(Vector2 pos) {

        byte[] data = new byte[8];

        float x = pos.x;
        int xInt = (int)(pos.x);
        int yInt = (int)(pos.y);

        data[0] = (byte)(xInt);
        data[1] = (byte)(xInt >> 8);
        data[2] = (byte)(xInt >> 16);
        data[3] = (byte)(xInt >> 24);

        data[4] = (byte)(yInt);
        data[5] = (byte)(yInt >> 8);
        data[6] = (byte)(yInt >> 16);
        data[7] = (byte)(yInt >> 24);

        File.WriteAllBytes("/save.dat", data);

    }

    static Vector2 LoadType2() {
        byte[] data = File.ReadAllBytes("/save.dat");

        int xInt = data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24;
        int yInt = data[4] | data[5] << 8 | data[6] << 16 | data[7] << 24;

        return new Vector2(xInt, yInt);

    }

    #endregion
}