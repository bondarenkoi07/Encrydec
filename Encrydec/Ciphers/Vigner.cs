namespace Encrydec.Ciphers
{
    public class Vigner : IEncryptor
    {
        private const string Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        
        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }

            return p.Substring(0, n);
        }
        private string Do(string message, string key, bool encrypting)
        {
            key = key.ToUpper();
            var gamma = GetRepeatKey(key, message.Length);
            var retValue = "";
            var q = Alphabet.Length;
            message = message.ToUpper();
            for (int i = 0; i < message.Length; i++)
            {
                var alphabetIndex = Alphabet.IndexOf(message[i]);
                var codeIndex = Alphabet.IndexOf(gamma[i]);
                if (alphabetIndex < 0)
                {
                    retValue += message[i].ToString();
                }
                else
                {
                    retValue += Alphabet[(q + alphabetIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                }
            }

            return retValue;
        }
        
        public string Encrypt(string message, string key)
        {
            return Do(message,key, true);
        }
        
        public string Decrypt(string message, string key)
        {
            return Do(message,key,false);
        }
    }
}