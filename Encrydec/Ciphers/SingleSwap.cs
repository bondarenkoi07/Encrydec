using System;

namespace Encrydec.Ciphers
{
    public class SingleSwap:IEncryptor
    {

        public string Encrypt(string message, string key) {    
            
            var width = key.Length;
            var height = message.Length % key.Length == 0 
                ? message.Length / key.Length
                : message.Length / key.Length +1;
            var permutation = new int[key.Length];
            for (int i = 0; i < permutation.Length; i++)
                permutation [i] = i;
            Array.Sort (key.ToCharArray (), permutation);
            try
            {
                char[] res = new char[message.Length];
                for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    res[i * width + j] = message[permutation[j] * height + i];
                return new string(res);
            }
            catch (Exception e)
            {
                return e.Data.ToString();
            }
            
 
        }
        public string Decrypt(string message, string key) {  
            
            var width = key.Length;
            var height = message.Length % key.Length == 0 
                ? message.Length / key.Length
                : message.Length / key.Length +1;
            var permutation = new int[key.Length];
            for (int i = 0; i < permutation.Length; i++)
                permutation [i] = i;
            Array.Sort (key.ToCharArray (), permutation);
            
            char[] res = new char[message.Length];
            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++) 
                res[permutation[i] * height + j] = message[j * width + i];              
            return new string (res);
        }
 
    }
}