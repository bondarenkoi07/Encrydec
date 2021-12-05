using System;
using System.Linq;
using Encrydec.Ciphers;

namespace Encrydec
{
    public static class CiphersParametersValidator
    {
        private static bool CheckMessage(string message, IEncryptor cipherType)
        {
            return cipherType switch
            {
                Ceasar  => message.Length > 0,
                Vigner  => message.Length > 0 && !message.Contains('\n'),
                _ => message.Length > 0 && !message.Contains('\n') 
            };
        }
        
        private static bool CheckKey(string key, string message, IEncryptor cipherType)
        {
            return cipherType switch
            {
                Ceasar  => CheckCeasarKey(key, message),
                Vigner  => CheckVignerKey(key, message),
                _ => CheckTable(key, message)
            };
        }
        
        public static bool CheckMessageAndKey(string key, string message, IEncryptor cipherType)
        {
            return CheckMessage(message, cipherType) && CheckKey(key, message, cipherType);
        }

        private static bool CheckCeasarKey(string key, string message)
        {
            var isValid = false;
            
            if (int.TryParse(key, out var value))
            {
                isValid = value > 0 && value < message.Length;
            }

            return isValid;
        }
        
        private static bool CheckVignerKey(string key, string message)
        {
            return key.Length > 0 && message.Length>0;
        }

        private static bool CheckTable(string key, string message)
        {
            return message.Length>key.Length && key.Length>1;
        }
    }
}