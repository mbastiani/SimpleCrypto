namespace Mbs.SimpleCrypto
{
    public interface ISimpleCryptoService
    {
        string AES_Encrypt(string contentToBeEncrypted, string encryptionKey);
        string AES_Decrypt(string contentToBeDecrypted, string encryptionKey);
    }
}