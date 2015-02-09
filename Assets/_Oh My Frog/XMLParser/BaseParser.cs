using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;
using OMF_Errors;

public abstract class BaseXMLParser
{
    private XmlTextReader reader;
    Stack m_names;

    public BaseXMLParser()
    {
        reader = null;
        m_names = new Stack();
    }

    public ErrorCode xmlParseFile(TextAsset textAsset)
    {
        MemoryStream assetStream = new MemoryStream(textAsset.bytes);
        reader = new XmlTextReader(assetStream);

        if (reader == null)
            return ErrorCode.FILE_NOT_FOUND;
        return Parse();
    }

    public ErrorCode Parse()
    {
        ErrorCode error;
        string elementName;
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    elementName = reader.Name;
                    m_names.Push(elementName);
                    error = onStartElement(elementName, getAttributes());
                    if (error != ErrorCode.IS_OK)
                    {
                        return error;
                    }
                    break;
                case XmlNodeType.EndElement:
                    elementName = m_names.Pop().ToString();
                    error = onEndElement(elementName);
                    if (error != ErrorCode.IS_OK)
                    {
                        return error;
                    }
                    break;
                case XmlNodeType.Comment:
                case XmlNodeType.XmlDeclaration:
                case XmlNodeType.Whitespace:
                    break;
                default:
                    return ErrorCode.UNDEFINED_XML_NODE;
            }
            //Console.WriteLine(reader[0]);
        }
        if (m_names.Count > 0)
            return ErrorCode.TAG_OPENED;
        reader.Close();
        return ErrorCode.IS_OK;
    }


    public Dictionary<string, string> getAttributes() {
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        if (reader.HasAttributes) {
            for (int i = 0; i < reader.AttributeCount; i++) {
                reader.MoveToAttribute(i);
                attrs.Add(reader.Name, reader.Value);
            }
        }
        return attrs;
    }
    public abstract ErrorCode onStartElement(string elementName, Dictionary<string, string> attrs);
    public abstract ErrorCode onEndElement(string elementName);


}

