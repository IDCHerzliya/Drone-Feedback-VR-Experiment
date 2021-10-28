using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SimpleLogger
{

    private static readonly SimpleLogger _instance = new SimpleLogger();
    private static StreamWriter _fileWriter;

    static SimpleLogger()
    {
        SetupFile();
    }

    public SimpleLogger()
    {

    }

    public static SimpleLogger Instance
    {
        get
        {
            return _instance;
        }
    }

    private static string _filePath;

    private static void SetupFile()
    {
        string name = "OmriAndriodData_";
        int i = 1;
        var path = Path.Combine(Application.persistentDataPath, name + i + ".txt");
        while (File.Exists(path))
        {
            i++;
            path = Path.Combine(Application.persistentDataPath, name + i + ".txt");
        }
        _filePath = path;
        _fileWriter = new StreamWriter(_filePath);
        _fileWriter.AutoFlush = true;
        _fileWriter.WriteLine("Data For Experiment Session"+ i); 
    }


    public static void WriteToFile(string data)
    {
        _fileWriter.WriteLine(data);
        _fileWriter.Flush();
    }
}
