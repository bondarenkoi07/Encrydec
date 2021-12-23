using System;
using System.Linq;
using System.Runtime.Serialization;

namespace lab4.Ciphers
{
    public class SingleSwap:IEncryptor
    {
        private const string Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public string Encrypt(string message, string key) {    
            
            var width = key.Length;
            var height = message.Length % key.Length == 0 
                ? message.Length / key.Length
                :  message.Length / key.Length == 1 
                    ? 2 
                    :message.Length / key.Length +1;
            
            

            var permutation = new int[key.Length];
            for (int i = 0; i < permutation.Length; i++)
                permutation [i] = i;
            var index = 0;
            foreach (var a in Alphabet)
            {
                for (int i = 0; i < permutation.Length; i++)
                {
                    if (a == permutation[i])
                    {
                        permutation[index] = permutation[index] ^ permutation[i];
                        permutation[i] = permutation[index] ^ permutation[i];
                        permutation[index] = permutation[index] ^ permutation[i];
                        index++;
                    }
                }
            }


            var trail = "";
            for (int i = 0; i < message.Length % key.Length; i++)
            {
                trail += " ";
            }
            message = message + trail;
            char[] res = new char[message.Length];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    try
                    {
                        res[i * width + j] = message[permutation[j] * height + i];
                    }
                    catch (Exception e)
                    {
                        return e.Message+permutation[j]+" "+i + " "+message.Length + " " + res.Length;
                    }
                }


            return new string(res);
        }
        public string Decrypt(string message, string key) {  
            
            var width = key.Length;
            var height = message.Length % key.Length == 0 
                ? message.Length / key.Length
                :  message.Length / key.Length > 0 
                    ? message.Length / key.Length +1 
                    :message.Length / key.Length +2;
            var permutation = new int[key.Length];
            for (int i = 0; i < permutation.Length; i++)
                permutation [i] = i;

            var index = 0;
            foreach (var a in Alphabet)
            {
                for (int i = 0; i < permutation.Length; i++)
                {
                    if (a == permutation[i])
                    {
                        permutation[index] = permutation[index] ^ permutation[i];
                        permutation[i] = permutation[index] ^ permutation[i];
                        permutation[index] = permutation[index] ^ permutation[i];
                        index++;
                    }
                }
            }
            
            var trail = "";
            for (int i = 0; i < message.Length % key.Length; i++)
            {
                trail += " ";
            }
            message = message + trail;
            char[] res = new char[message.Length];
            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                try
                {
                    res[permutation[i] * height + j] = message[j * width + i]; 
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                         
            return new string (res);
        }
 
    }
}