using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEvent : MonoBehaviour
{
    public NPC npc;
    // Start is called before the first frame update
    void Start()
    {
        npc = GetComponentInParent<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack(){
        npc.Attack();
    }
}
