namespace lab4.Ciphers
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
            var retValue = "";
            message = message.ToUpper();
            for (int i = 0; i < message.Length; i++)
            {
                var alphabetIndex = Alphabet.IndexOf(message[i]);
                var codeIndex = Alphabet.IndexOf(key[i%key.Length]);

                retValue += message[i]==' '? ' '
                    : encrypting
                    ? Alphabet[(alphabetIndex + codeIndex) % Alphabet.Length].ToString().ToLower()
                    : alphabetIndex < codeIndex
                        ? Alphabet[alphabetIndex - codeIndex + Alphabet.Length].ToString().ToLower()
                        : Alphabet[(alphabetIndex - codeIndex) % Alphabet.Length].ToString().ToLower();

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