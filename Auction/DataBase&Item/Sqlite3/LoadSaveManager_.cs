using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;

public class AuctionItemManager
{
    public int ID;
    public string Name;
    public string Des;
    public int Value;
    public string Rank;
    public int GL;
    public string Type;

    public AuctionItemManager(int _id, string _name, string _des, int _val, string _rank, int _gl, string _type)
    {
        ID = _id;
        Name = _name;
        Des = _des;
        Value = _val;
        Rank = _rank;
        GL = _gl;
        Type = _type;
    }

    public enum ItemType
    {
        Eqip,
        Use,
        None
    }
}

public class LoadSaveManager_ : MonoBehaviour
{
    public List<AuctionItemManager> _ItemList = new List<AuctionItemManager>();

    void Start()
    {

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        yield return StartCoroutine(ItemDbParsing("LoadSaveManager.db));  // 아이템 정보 파싱. 


    }


    // 코루틴 .
    IEnumerator ItemDbParsing(string p)
    {

        string Filepath = Application.persistentDataPath + "/" + p;

        if (!File.Exists(Filepath))
        {
            Debug.LogWarning("File \"" + Filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/" + p);

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            File.WriteAllBytes(Filepath, loadDB.bytes);
        }

        string connectionString = "URI=file:" + Filepath;


        _ItemList.Clear();

        // using을 사용함으로써 비정상적인 예외가 발생할 경우에도 반드시 파일을 닫히도록 할 수 있다.
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
            {

                string sqlQuery = "SELECT * FROM BaseItemList";


                dbCmd.CommandText = sqlQuery;
                using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
                {
                    while (reader.Read())
                    {
                        // Debug.Log(reader.GetString(1));  //  타입명 . (몇 열에있는것을 불러 올 것인가)
                        _ItemList.Add(new AuctionItemManager(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                                      reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6)));
                    }
                    
                    reader.Close();
                }
            }
        }

        // 확인용
        for (int i = 0; i < _ItemList.Count; i++)
        {
            Debug.Log(_ItemList[i].ID + " , " + _ItemList[i].Name + " , " + _ItemList[i].Des + " , " + _ItemList[i].Value
                + " , " + _ItemList[i].Rank + " , " + _ItemList[i].GL);
        }
        yield return null;
    }

}
