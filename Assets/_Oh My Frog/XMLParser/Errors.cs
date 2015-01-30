using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OMF_Errors;

namespace OMF_Errors {
    public enum  ErrorCode{
        IS_OK,
        FILE_NOT_FOUND,
        UNDEFINED_XML_NODE,
        TAG_OPENED,
        LAYER_NOT_DEFINED,
        GO_NOT_FOUND
    }
}

public class Error
{
    public Dictionary<ErrorCode, string> errors;

    public Error()
    {
        errors = new Dictionary<ErrorCode, string>();
        errors[ErrorCode.IS_OK] = "is ok";
        errors[ErrorCode.FILE_NOT_FOUND] = "File not found";
        errors[ErrorCode.UNDEFINED_XML_NODE] = "Node not defined, if you like defined go in 'basePaser' ";
        errors[ErrorCode.TAG_OPENED] = "Tag not closed";
        errors[ErrorCode.LAYER_NOT_DEFINED] = "This layer not defined max 10 layers (0_XXX - 9_XXX)";
        errors[ErrorCode.GO_NOT_FOUND] = "GameObject not found";



    }

    public string getError(ErrorCode errorCode)
    {
        return errors[errorCode];
    }

}