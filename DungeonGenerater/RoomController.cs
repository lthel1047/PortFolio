using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo{
    public string name;
    public int x;
    public int y;
}
public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    void Awake() {
        instance = this;    
    }

    void Start(){
        LoadRoom("Start",0,0);
        LoadRoom("Empty",1,1);
        //LoadRoom("Empty",-1,0);
       // LoadRoom("Empty",0,1);
        //LoadRoom("Empty",0,-1);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue(){
        if(isLoadingRoom){
            return;
        }
        if(loadRoomQueue.Count == 0){
            return;
        }

        currentLoadRoomData=loadRoomQueue.Dequeue();
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    public void LoadRoom(string name, int x, int y){

        if(DoseRoomExist(x,y)){
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name =name;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);

    }

    IEnumerator LoadRoomRoutine(RoomInfo info){
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom =SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false){
            yield return null;
        }
    }

    public void RegisterRoom(Room room){
        room.transform.position=new Vector3(
            currentLoadRoomData.x * room.Width,
            currentLoadRoomData.y * room.Height,
            0
        );

        room.X=currentLoadRoomData.x;
        room.Y=currentLoadRoomData.y;
        room.name=currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
        room.transform.parent = transform;
    
        isLoadingRoom = false;

        if(loadedRooms.Count == 0)
        {
            CameraController.instance.currRoom=room;
        }
        loadedRooms.Add(room);

    }

    public bool DoseRoomExist(int x,int y){
        return loadedRooms.Find(item => item.X == x && item.Y == y)!=null;
    }

     public void OnPlayerEnterRoom(Room room){
        CameraController.instance.currRoom = room;
        currRoom = room;
    }
}
