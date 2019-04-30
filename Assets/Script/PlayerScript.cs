using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerScript : NetworkBehaviour {
    
      
    public GameObject shell;
    public Transform  shellSpain;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public float force = 6.0f;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        float translation = Input.GetAxis("Vertical") * speed;
        
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    
        translation *= Time.deltaTime;
    
        rotation *= Time.deltaTime;
       
        transform.Translate(0, 0, translation);
       
        transform.Rotate(0, rotation, 0);


        if (Input.GetButtonDown("Fire1"))
        {
            CmdFire();
        }
    }
    [Command]
    private void CmdFire()
    {
        GameObject  bull = (GameObject) Instantiate(shell,shellSpain.position,shellSpain.rotation); 
        bull.GetComponent<Rigidbody>().velocity = bull.transform.forward * force;
        NetworkServer.Spawn(bull);
        
        Destroy(bull,2);
    }

    public override void OnStartLocalPlayer(){
       GetComponent<MeshRenderer>().material.color =Color.blue;
    //    base.OnStartLocalPlayer();
    }
}
