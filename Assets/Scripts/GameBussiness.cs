using System;
using System.IO;
using System.Collections;
using UnityEngine;
using GameFunctions;


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

        byte[] data = new byte[1024];
        int offset = 2;
        GFBufferEncoderWriter.WriteSingle(data,pos.x,ref offset);
        GFBufferEncoderWriter.WriteSingle(data,pos.y,ref offset);
        int length = offset;
        GFBufferEncoderWriter.WriteUInt16(data, (ushort)length, ref offset);

        using (FileStream fs = new FileStream("slot1.save", FileMode.Create)) {
            fs.Write(data, 0, length);
        }
    }

    static Vector2 LoadType2() {
        byte[] data = File.ReadAllBytes("slot1.save");
        int offset = 0;

        int length = GFBufferEncoderReader.ReadUInt16(data, ref offset);
        float x = GFBufferEncoderReader.ReadSingle(data, ref offset);
        float y = GFBufferEncoderReader.ReadSingle(data, ref offset);

        return new Vector2(x, y);

    }

    #endregion
}