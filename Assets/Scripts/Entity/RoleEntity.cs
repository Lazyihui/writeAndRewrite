using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleEntity : MonoBehaviour {

    public int id;
    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }


    void Update() {
        Vector2 moveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
        transform.position += (Vector3)moveAxis * Time.deltaTime;
    }
}
