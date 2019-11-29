using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Antivirus.Signature
{
    public class SignatureAnalyzer
    {
        public List<IVirusSignature> SignatureList { get; set; }

        public SignatureAnalyzer()
        {
            SignatureList = new List<IVirusSignature>
            {
                new SkynetSignature()
            };
        }

        public SignatureDirectoryResult CheckDirectory(string path)
        {
            var directoryResult = new SignatureDirectoryResult();

            foreach (var file in Directory.GetFiles(path))
            {
                var fileBytes = File.ReadAllBytes(file);
                var fileResult = new SignatureFileResult(new FileInfo(file).Name);

                foreach (var signature in SignatureList)
                {
                    var checkRes = CheckFile(fileBytes, signature.Signature);

                    if (checkRes)
                    {
                        fileResult.Viruses.Add(signature.Name);
                    }
                }

                directoryResult.FileResults.Add(fileResult);
            }

            return directoryResult;
        }

        public bool CheckFile(byte[] fileBytes, byte[] signature)
        {
            if (fileBytes.Length < signature.Length) return false;

            var index = Enumerable.Range(0, fileBytes.Length - signature.Length + 1);

            for (int i = 0; i < signature.Length; i++)
            {
                index = index.Where(x => fileBytes[x + i] == signature[i]).ToArray();
            }

            return index.Any();
        }
    }
}
