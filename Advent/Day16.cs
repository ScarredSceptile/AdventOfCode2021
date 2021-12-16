using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day16
    {
        //This does both
        public static void Complete()
        {
            var input = Input.Get("Day16")[0];
            var message = "";
            foreach (var c in input)
                message += ToBinary(c);
            (int VersionSum, List<long> packageSum) = ReadPackageVersionSum(message);
            Console.WriteLine($"Version Sum: {VersionSum}\nPackage Total: {packageSum[0]}");
        }

        private static (int VersionSum, List<long> packageSum) ReadPackageVersionSum(string message)
        {
            int i = 0;
            int versionSum = 0;
            List<long> packSum = new List<long>();
            while (i < message.Length)
            {
                if (message.Length - i < 11)
                    break;

                versionSum += Convert.ToInt32(message.Substring(i, 3), 2);
                i += 3;
                var typeID = Convert.ToInt32(message.Substring(i, 3), 2);
                i += 3;
                if (typeID == 4)
                {
                    string value = "";
                    while (message[i] != '0')
                    {
                        value += message.Substring(i + 1, 4);
                        i += 5;
                    }
                    value += message.Substring(i + 1, 4);
                    i += 5;
                    packSum.Add(Convert.ToInt64(value, 2));
                }
                else
                {
                    var lengthID = message[i];
                    i++;
                    if (lengthID == '0')
                    {
                        var bitlength = Convert.ToInt32(message.Substring(i, 15), 2);
                        i += 15;
                        (int VersionSum, List<long> packageSum) = ReadPackageVersionSum(message.Substring(i, bitlength));
                        versionSum += VersionSum;
                        packSum.Add(CalculatePacket(typeID, packageSum));
                        i += bitlength;
                    }
                    else if (lengthID == '1')
                    {
                        var packages = Convert.ToInt32(message.Substring(i, 11), 2);
                        i += 11;
                        List<long> Values = new List<long>();
                        for (int j = 0; j < packages; j++)
                        {
                            (int Version, int length, List<long> PackageSum) = ReadSinglePackageVersionSum(message[i..]);
                            versionSum += Version;
                            PackageSum.ForEach(n => Values.Add(n));
                            i += length;
                        }
                        packSum.Add(CalculatePacket(typeID, Values));
                    }
                }

            }
            return (versionSum, packSum);
        }

        private static (int Version, int length, List<long> packageSum) ReadSinglePackageVersionSum(string message)
        {
            int i = 0;
            int versionSum = 0;
            List<long> packSum = new List<long>();

            versionSum += Convert.ToInt32(message.Substring(i, 3), 2);
            i += 3;
            var typeID = Convert.ToInt32(message.Substring(i, 3), 2);
            i += 3;
            if (typeID == 4)
            {
                string value = "";
                while (message[i] != '0')
                {
                    value += message.Substring(i + 1, 4);
                    i += 5;
                }
                value += message.Substring(i + 1, 4);
                i += 5;
                packSum.Add(Convert.ToInt64(value, 2));
            }
            else
            {
                var lengthID = message[i];
                i++;
                if (lengthID == '0')
                {
                    var bitlength = Convert.ToInt32(message.Substring(i, 15), 2);
                    i += 15;
                    (int VersionSum, List<long> packageSum) = ReadPackageVersionSum(message.Substring(i, bitlength));
                    versionSum += VersionSum;
                    packSum.Add(CalculatePacket(typeID, packageSum));
                    i += bitlength;
                }
                else if (lengthID == '1')
                {
                    var packages = Convert.ToInt32(message.Substring(i, 11), 2);
                    i += 11;
                    List<long> Values = new List<long>();
                    for (int j = 0; j < packages; j++)
                    {
                        (int Version, int length, List<long> PackageSum) = ReadSinglePackageVersionSum(message[i..]);
                        versionSum += Version;
                        PackageSum.ForEach(n => Values.Add(n));
                        i += length;
                    }
                    packSum.Add(CalculatePacket(typeID, Values));
                }
            }
            return (versionSum, i, packSum);
        }

        private static long CalculatePacket(int typeID, List<long> packets)
        {
            long p = 0;

            switch (typeID)
            {
                case 0:
                    p = packets.Sum();
                    break;
                case 1:
                    p = 1;
                    packets.ForEach(n => p *= n);
                    break;
                case 2:
                    p = packets.Min();
                    break;
                case 3:
                    p = packets.Max();
                    break;
                case 5:
                    p = packets[0] > packets[1] ? 1 : 0;
                    break;
                case 6:
                    p = packets[0] < packets[1] ? 1 : 0;
                    break;
                case 7:
                    p = packets[0] == packets[1] ? 1 : 0;
                    break;
                default: break;
            }

            return p;
        }

        private static string ToBinary(char value)
        {
            Dictionary<char, string> dict = new Dictionary<char, string>
            {
                { '0', "0000" },
                { '1', "0001" },
                { '2', "0010" },
                { '3', "0011" },
                { '4', "0100" },
                { '5', "0101" },
                { '6', "0110" },
                { '7', "0111" },
                { '8', "1000" },
                { '9', "1001" },
                { 'A', "1010" },
                { 'B', "1011" },
                { 'C', "1100" },
                { 'D', "1101" },
                { 'E', "1110" },
                { 'F', "1111" }
            };
            return dict[value];
        }
    }
}
