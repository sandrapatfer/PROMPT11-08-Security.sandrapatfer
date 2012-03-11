using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Ex1
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("Usage: Ex1 <Hash algorithm> <file_path>");
                Console.WriteLine("Acceptable Hash algorithms: SHA1, MD5, SHA256, SHA384 and SHA512");
                return;
            }

            string algorithm = args[0];
            string filename = args[1];

            if (!File.Exists(filename))
            {
                Console.WriteLine("Invalid file name, file does not exist!");
                return;
            }

            using (var hashAlg = HashAlgorithm.Create(algorithm))
            {
                using (var fileStream = new FileStream(filename, FileMode.Open))
                {
                    using (var stream = new CryptoStream(fileStream, hashAlg, CryptoStreamMode.Read))
                    {
                        byte[] buf = new byte[1024];
                        while (stream.Read(buf, 0, buf.Length) != 0) ;
                        Console.Write("Hash wiht {0} algorithm: ", algorithm);
                        foreach (var b in buf)
                        {
                            Console.Write("{0:X}", b);
                        }
                        Console.WriteLine();
                    }
                }
            }
            Console.ReadKey();

        }
    }
}
