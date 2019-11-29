using System;
using System.Collections.Generic;
using System.Text;

namespace Antivirus.Signature
{
    public class SignatureDirectoryResult
    {
        public List<SignatureFileResult> FileResults { get; set; }

        public SignatureDirectoryResult()
        {
            FileResults = new List<SignatureFileResult>();
        }
    }

    public class SignatureFileResult
    {
        public string FileName { get; set; }
        public List<string> Viruses { get; set; }

        public SignatureFileResult(string fileName)
        {
            FileName = fileName;
            Viruses = new List<string>();
        }
    }
}
