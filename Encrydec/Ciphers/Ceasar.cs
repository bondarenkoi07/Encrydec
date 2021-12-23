using System;

namespace lab4.Ciphers
{
    public class Ceasar:IEncryptor
    {
        private const string Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private string Do(string message, int key)
        {
            var fullAlfabet = Alphabet + Alphabet.ToLower();
            var letterQty = fullAlfabet.Length;
            var retVal = "";
            for (int i = 0; i < message.Length; i++)
            {
                var c = message[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + key) % letterQty;
                    retVal += fullAlfabet[codeIndex];
                }
            }

            return retVal;
        }
        
        public string Encrypt(string message, string key)
        {
            return Do(message,Convert.ToInt32(key));
        }
        
        public string Decrypt(string message, string key)
        {
            return Do(message,-Convert.ToInt32(key));
        }

        public override string ToString()
        {
            return "Шифр Цезаря";
        }
    }
}