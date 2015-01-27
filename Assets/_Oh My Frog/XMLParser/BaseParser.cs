using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;

public abstract class BaseXMLParser
{
    private string filename;
    private XmlTextReader reader;
    Stack m_names;

    public BaseXMLParser()
    {
        reader = null;
        filename = "";
        m_names = new Stack();
    }

    public bool xmlParseFile(string name)
    {
        filename = name;
        reader = new XmlTextReader(filename);

        if (reader == null) 
            return false; //failed
        Parse();
        return true;
    }

    public bool xmlParseFile(TextAsset textAsset)
    {
        MemoryStream assetStream = new MemoryStream(textAsset.bytes);
        reader = new XmlTextReader(assetStream);

        if (reader == null)
            return false; //failed
        Parse();
        return true;
    }

    public int Parse()
    {
        string elementName;
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    elementName = reader.Name;
                    m_names.Push(elementName);
                    onStartElement(elementName, getAttributes());
                    break;
                case XmlNodeType.EndElement:
                    elementName = m_names.Pop().ToString();
                    onEndElement(elementName);
                    break;
                default:
                    Console.WriteLine(reader.NodeType);
                    break;
            }
            //Console.WriteLine(reader[0]);
        }
        reader.Close();
        return 0;
    }


    public Dictionary<string, string> getAttributes() {
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        //attr[] attrs = new attr[reader.AttributeCount];
        if (reader.HasAttributes) {
            for (int i = 0; i < reader.AttributeCount; i++) {
                reader.MoveToAttribute(i);
                attrs.Add(reader.Name, reader.Value);
            }
        }
        return attrs;
    }
    public abstract void onStartElement(string elementName, Dictionary<string, string> attrs);
    public abstract void onEndElement(string elementName);

}

