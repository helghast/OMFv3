using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OMF_Errors;

public class ErrorParser : BaseXMLParser  {

    public override ErrorCode onStartElement(string elementName, Dictionary<string, string> attrs) {
        return ErrorCode.IS_OK;
    }
    public override ErrorCode onEndElement(string elementName) {
        return ErrorCode.IS_OK;
    
    }
    

}
