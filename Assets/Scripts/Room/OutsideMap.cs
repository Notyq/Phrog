using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideMap : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    private bool dead;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.y < transform.position.y)
            {
                previousRoom.GetComponent<Room>().ActivateRoom(false);
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }
}
