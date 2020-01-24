using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAWScaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new WAWFile("C:\\Users\\Алексей\\Documents\\Hackerdom\\harry.wav");

            foreach (var d in file.StereoDifference().Take(8))
                Console.WriteLine(d);
            var diff = file.StereoDifference().Select(x => x == 1).ToArray();
            BitArray bits = new BitArray(diff);

            byte[] bytes = new byte[bits.Length / 8];
            bits.CopyTo(bytes, 0);
            
            Console.WriteLine(bytes[0]);

            File.WriteAllBytes("C:\\Users\\Алексей\\Documents\\Hackerdom\\harry2.wav", ToByte(diff));

            Console.ReadLine();
        }

        static private byte[] ToByte(bool[] bits)
        {
            byte[] bytes = new byte[bits.Length / 8];

            for (var i = 0; i < bytes.Length; i++)
            {
                byte f = 128;
                for (var j = 0; j < 8; j++)
                {
                    bytes[i] += (bits[i * 8 + j]) ? f : (byte) 0;
                    f /= 2;
                }
            }

            return bytes;
        }
    }
}
