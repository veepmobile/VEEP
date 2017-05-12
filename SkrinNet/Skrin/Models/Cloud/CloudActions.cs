using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Cloud
{
    public enum CloudActions:short
    {
        AddNote,
        UpdateNote,
        DeleteNote,
        AddAddress,
        UpdateAddress,
        DeleteAddress,
        AddFile,
        DeleteFile
    }
}