using System;
using System.Collections.Generic;
using System.Text;

namespace Antivirus.Signature
{
    /// <summary>
    /// Использует "бипер" в цикле для каждой буквы при выводе своего сообщения
    /// </summary>
    public class SkynetSignature : IVirusSignature
    {
        public string Name => "Skynet";
        public byte[] Signature => new byte[] {0x50, 0xB4, 0x7F, 0xE4, 0x61, 0x24, 0xFC, 0x34, 0x02, 0xE6, 0x61, 0xB9, 0x00, 0x01, 0xE2, 0xFE, 0xFE, 0xCC, 0x75, 0xF3, 0x58, 0xC3};
    }

}
