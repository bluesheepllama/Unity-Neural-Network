using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadWrite : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    public bool addSingleRow = false;


    // Use this for initialization
    void Start()
    {
        string[] rowDataTemp = new string[3];
        rowDataTemp[0] = "C";
        rowDataTemp[1] = "Distance";
        rowDataTemp[2] = "CanJump";
        rowData.Add(rowDataTemp);
        //Save();
    }

    public void AddSingleRown()
    {
        string[] rowDataTemp = new string[1];
        rowDataTemp[0] = "Trained";
        rowData.Add(rowDataTemp);
        addSingleRow = false;
    }

    public void SaveDistance(double[] C)
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[3];
        //rowDataTemp[0] = "C";
        //rowDataTemp[1] = "Distance";
        //rowDataTemp[2] = "CanJump";
        //rowData.Add(rowDataTemp);
        if (addSingleRow)
            AddSingleRown();

        // You can add up the values in as many cells as you want.
        //for (int i = 0; i < C.Length; i++)
        //{
            rowDataTemp = new string[3];
            rowDataTemp[1] = "" + C[0]; // name
        rowDataTemp[2] = "" + C[1];
        //rowDataTemp[1] = "" + i; // ID
        //rowDataTemp[2] = "$" + i;// + UnityEngine.Random.Range(5000, 10000); // Income
        rowData.Add(rowDataTemp);
        //}

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void SaveDouble(double input, string title,int col)
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[1];
        rowDataTemp[0] = title;
        //rowDataTemp[1] = "";
        // rowDataTemp[2] = "";
        rowData.Add(rowDataTemp);
        if (addSingleRow)
            AddSingleRown();

        // You can add up the values in as many cells as you want.
        //for (int i = 0; i < C.Length; i++)
        //{
            rowDataTemp = new string[1];
            rowDataTemp[0] = "" + input; // name
            
            rowData.Add(rowDataTemp);
        rowData.Add(new string[] { " ", " " });
        //}

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }


    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath + "/" + "Saved_data.csv";
#endif
    }

    private string getPathDistance()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_dataDistance.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath + "/" + "Saved_data.csv";
#endif
    }

    private string getPathBias()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath + "/" + "Saved_data.csv";
#endif
    }

}
