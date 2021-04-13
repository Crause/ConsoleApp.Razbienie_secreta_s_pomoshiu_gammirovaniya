using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Разбиение_секрета_с_помощью_гаммирования
{
  class Program
  {
    static void Main(string[] args)
    {
      //Input secret
      Console.WriteLine("Введите секрет:");
      string secret_string = Console.ReadLine();
      Console.WriteLine($"Секрет: {secret_string} | Бинарный код: {StringToBinary(secret_string)}");

      //Input gammas
      Console.WriteLine("\nВведите гаммы (для завершения ввода отправьте -):");
      List<string> Gammas = new List<string>();

      for (;;)
      {
        string input = Console.ReadLine();
        if (input == "-") break;
        if (input.Length == secret_string.Length) Gammas.Add(input);
        if (input.Length > secret_string.Length)
        {
          Gammas.Add(input.Substring(0, secret_string.Length));
        }
        if (input.Length < secret_string.Length)
        {
          int j = 0;
          int lenght = input.Length;
          while (input.Length != secret_string.Length)
          {
            if (j == lenght) j = 0;
            input += input[j];
            j++;
          }
          Gammas.Add(input);
        }
        
      }
      Console.WriteLine(); 
      
      //Output gammas
      for (int i = 0; i < Gammas.Count; i++)
      {
        Console.WriteLine($"Гамма {i+1}: {Gammas[i]} | Бинарный код: {StringToBinary(Gammas[i])}");
      }

      //Encryption
      string Cipher_binary = StringToBinary(secret_string);
      for (int i = 0; i < Gammas.Count; i++)
      {
        Cipher_binary = XOR(Cipher_binary, StringToBinary(Gammas[i]));
      }
      Console.WriteLine($"\nШифрограмма: {BinaryToString(Cipher_binary)} | Бинарный код: {Cipher_binary}");
     

      //Decryption
      string decoded_secret_binary = Cipher_binary;
      for (int i = 0; i < Gammas.Count; i++)
      {
        decoded_secret_binary = XOR(decoded_secret_binary, StringToBinary(Gammas[i]));
      }
      Console.WriteLine($"\nСекрет: {BinaryToString(decoded_secret_binary)} | Бинарный код: {decoded_secret_binary}");
      Console.ReadLine();
    }
    private static string StringToBinary(string text)
    {
      StringBuilder str = new StringBuilder("");
      StringBuilder str1 = new StringBuilder("");
      for (int i = 0; i < text.Length; i++)
      {
        str.Append(Convert.ToString(text[i], 2).PadLeft(8, '0'));
      }
      return str.ToString();
    }
    private static string BinaryToString(string bin)
    {
      byte[] byteList = new byte[bin.Length / 8];

      for (int i = 0, j = 0; i < bin.Length - 1; i += 8, j++)
      {
        byteList[j] = Convert.ToByte(bin.Substring(i, 8), 2);
      }
      return Encoding.UTF8.GetString(byteList);
    }
    private static string XOR(string s1, string s2)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < s1.Length; i++)
        sb.Append((byte)(s1[i] ^ s2[i]));
      return sb.ToString();
    }
  }
}
