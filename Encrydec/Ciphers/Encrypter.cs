
namespace lab4.Ciphers {
    public interface IEncryptor
    {
        string Encrypt(string message, string key);
        string Decrypt(string message, string key);
    }
}