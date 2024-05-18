using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFunctions;


public static class GameBussiness {

    public static void NewGame(GameContext ctx) {


        Vector2 pos = new Vector2(5, 5);
        ctx.roleEntity.SetPos(pos);

        ctx.rooms.Add(1, CreateRoom());
        ctx.rooms.Add(2, CreateRoom());
    }


    static int roomIDRecord = 0;
    static RoomEntity CreateRoom() {
        RoomEntity room = new RoomEntity();
        room.id = roomIDRecord++;
        room.roles = new RoleEntity[UnityEngine.Random.Range(1, 5)];

        for (int i = 0; i < room.roles.Length; i++) {
            room.roles[i] = new GameObject("ROLE").AddComponent<RoleEntity>();
            room.roles[i].id = i;
        }
        return room;
    }

    public static void LoadGame(GameContext ctx) {

        Vector2 pos = new Vector2(0, 0);

        // pos = LoadType1();
       LoadType3(ctx);

        ctx.roleEntity.SetPos(pos);


    }

    public static void SaveGame(GameContext ctx) {


        Vector2 pos = ctx.roleEntity.transform.position;

        // 存到本地
        // SaveType1(pos);
        SaveType3(ctx);
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
        GFBufferEncoderWriter.WriteSingle(data, pos.x, ref offset);
        GFBufferEncoderWriter.WriteSingle(data, pos.y, ref offset);
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


    #region SaveType3
    static void SaveType3(GameContext ctx) {
        byte[] data = new byte[1024];
        int offset = 4;
        foreach (var room in ctx.rooms.Values) {
            RoomSaveModel roomSaveModel = new RoomSaveModel();
            roomSaveModel.id = room.id;
            roomSaveModel.roles = new List<RoleSaveModel>(room.roles.Length);

            for (int i = 0; i < room.roles.Length; i++) {
                RoleEntity roleEntity = room.roles[i];


                RoleSaveModel roleSaveModel = new RoleSaveModel();
                roleSaveModel.id = roleEntity.id;
                roleSaveModel.pos = roleEntity.transform.position;
                roomSaveModel.roles.Add(roleSaveModel);
            }


            roomSaveModel.WriteTo(data, ref offset);
        }

        int length = offset;
        offset = 0;
        GFBufferEncoderWriter.WriteUInt32(data, (uint)length, ref offset);

        using (FileStream fs = new FileStream("slot1.save", FileMode.Create)) {
            fs.Write(data, 0, length);
        }

    }

    static void LoadType3(GameContext ctx) {

        byte[] data = File.ReadAllBytes("slot1.save");
        int offset = 0;
        int length = (int)GFBufferEncoderReader.ReadUInt32(data, ref offset);

        while (offset < length) {
            RoomSaveModel roomSaveModel = new RoomSaveModel();
            roomSaveModel.FromBytes(data, ref offset);

            RoomEntity roomEntity = new RoomEntity();
            roomEntity.id = roomSaveModel.id;
            roomEntity.roles = new RoleEntity[roomSaveModel.roles.Count];

            for (int i = 0; i < roomSaveModel.roles.Count; i++) {
                RoleSaveModel roleSaveModel = roomSaveModel.roles[i];
                RoleEntity roleEntity = new GameObject("ROLE").AddComponent<RoleEntity>();
                roleEntity.id = roleSaveModel.id;
                roleEntity.SetPos(roleSaveModel.pos);
                roomEntity.roles[i] = roleEntity;
            }

            ctx.rooms.Add(roomEntity.id, roomEntity);
        }
        // 打印房间信息
        foreach (var room in ctx.rooms.Values) {
            Debug.Log("Room ID: " + room.id);
            foreach (var role in room.roles) {
                Debug.Log("\tRole ID: " + role.id + " Pos: " + role.transform.position);
            }
        }
    }




    #endregion
}