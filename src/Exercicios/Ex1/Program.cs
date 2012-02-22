using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var hashAlg = HashAlgorithm.Create())
            {
                using (var fileStream = new FileStream(args[0], FileMode.Open))
                {
                    using (var stream = new CryptoStream(fileStream, hashAlg, CryptoStreamMode.Read))
                    {
                        byte[] buf = new byte[1024];
                        while (stream.Read(buf, 0, buf.Length) != 0) ;
                        //                    Console.WriteLine(string.Format("hash is: {0}", Encoding.ASCII.GetString(hashAlg.Hash)));
                        foreach (var b in buf)
                        {
                            Console.Write("{0:X}", b);
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
