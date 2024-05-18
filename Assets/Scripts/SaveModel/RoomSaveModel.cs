using System;
using System.Collections.Generic;
using GameFunctions;

[GFBufferEncoderMessage]
public struct RoomSaveModel : IGFBufferEncoderMessage<RoomSaveModel>
{
    public int id;
    public List<RoleSaveModel> roles;
    public void WriteTo(byte[] dst, ref int offset)
    {
        GFBufferEncoderWriter.WriteInt32(dst, id, ref offset);
        GFBufferEncoderWriter.WriteMessageList(dst, roles, ref offset);
    }

    public void FromBytes(byte[] src, ref int offset)
    {
        id = GFBufferEncoderReader.ReadInt32(src, ref offset);
        roles = GFBufferEncoderReader.ReadMessageList(src, () => new RoleSaveModel(), ref offset);
    }
}