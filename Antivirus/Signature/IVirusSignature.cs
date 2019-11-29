using System;
using System.Collections.Generic;
using System.Text;

namespace Antivirus.Signature
{
    public interface IVirusSignature
    {
        string Name { get; }
        byte[] Signature { get; }
    }
}
