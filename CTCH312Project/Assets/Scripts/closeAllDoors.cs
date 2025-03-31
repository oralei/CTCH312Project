using UnityEngine;

public class closeAllDoors : MonoBehaviour
{
    [Header("1F doors")]
    public DoorController door1;
    public DoorController door2;

    [Header("2F doors")]
    public DoorController door3;
    public DoorController door4;
    public DoorController door5;
    public DoorController door6;
    public DoorController door7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeDoors()
    {
        if (door1.isOpening == true)
            door1.isOpening = false;

        if (door2.isOpening == true)
            door2.isOpening = false;

        if (door3.isOpening == true)
            door3.isOpening = false;

        if (door4.isOpening == true)
            door4.isOpening = false;

        if (door5.isOpening == true)
            door5.isOpening = false;

        if (door6.isOpening == true)
            door6.isOpening = false;

        if (door7.isOpening == true)
            door7.isOpening = false;
    }
}
