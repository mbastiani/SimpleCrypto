namespace Mbs.SimpleCrypto.Services.Interfaces
{
    public interface ISimpleAesCryptoService
    {
        string AesEncrypt(string contentToBeEncrypted, string privateEncryptionKey);
        string AesDecrypt(string contentToBeDecrypted, string privateEncryptionKey);
    }
}