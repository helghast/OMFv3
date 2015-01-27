using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class XMLSimpleParser
{
    private XmlReader reader;
    public XMLSimpleParser()
    {

    }

    public void BeginParse(string file_name)
    {
        reader = XmlReader.Create("assets/" + file_name);
    }

    public void ReadLine()
    {
        reader.Read();
        while (reader.Name == "")
        {
            reader.Read();
        }
    }

    public string GetNameNode()
    {
        return reader.Name;
    }

    public bool IsStartElement()
    {
        return (reader.IsStartElement());
    }

    public int GetIntAtt(string key, int default_value)
    {
        string string_value = reader.GetAttribute(key);
        if (string_value == null)
            return default_value;
        else
        {
            int int_value;
            if (int.TryParse(string_value, out int_value))
            {
                return int_value;
            }
            return default_value;
        }
    }

    public float GetFloatAtt(string key, float default_value)
    {
        string string_value = reader.GetAttribute(key);
        if (string_value == null)
            return default_value;
        else
        {
            float float_value;
            if (float.TryParse(string_value, out float_value))
            {
                return float_value;
            }
            return default_value;
        }
    }

    public bool GetBoolAtt(string key, bool default_value)
    {
        string string_value = reader.GetAttribute(key);
        if (string_value == null)
            return default_value;
        else
        {
            if (string_value == "true" || string_value == "yes")
                return true;

            if (string_value == "false" || string_value == "no")
                return false;

            return default_value;
        }
    }

    public string GetStringAtt(string key, string default_value)
    {
        string string_value = reader.GetAttribute(key);
        if (string_value == null)
            return default_value;
        else
        {
            return string_value;
        }
    }
}