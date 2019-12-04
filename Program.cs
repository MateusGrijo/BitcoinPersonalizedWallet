using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using System.Threading;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;


class Program
{
    public static string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\";

    static void generateName(object _name)
    {
        string name = (string)_name;
        Key key = new Key();
        BitcoinSecret secretKey = key.GetBitcoinSecret(Network.Main);
        BitcoinAddress addressKey = secretKey.PubKey.GetAddress(Network.Main);

        if (("1" + name).Trim().ToUpper() == addressKey.ToString().Substring(0, name.Length + 1).Trim().ToUpper())
        {
            string s = secretKey + " ----- " + addressKey.ToString();
            System.IO.File.WriteAllText(location + name + " - BitcoinAddress-" + addressKey.ToString() + ".txt", s);;
            Environment.Exit(0);
        }
        Console.WriteLine(addressKey.ToString().Substring(0, name.Length + 1).Trim().ToUpper());
    }

   

    static void MagicName()
    {
        Console.WriteLine("Enter your name: ");
        String name = Console.ReadLine().Trim();
        Console.WriteLine("Enter delay(1ms): ");
        int delay = int.Parse(Console.ReadLine().Trim());

        while (true)
        {
            System.Threading.Thread t = new Thread(generateName);
            t.Start(name);
            t.Join();
            System.Threading.Thread.Sleep(delay);
        }
    }



    static void Main(string[] args)
    {
        MagicName();
    }



}
