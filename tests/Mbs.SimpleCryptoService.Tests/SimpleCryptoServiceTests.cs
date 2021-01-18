using Xunit;

namespace Mbs.SimpleCrypto.Tests
{
    public class SimpleCryptoServiceTests
    {
        private readonly ISimpleCryptoService _simpleCryptoService;
        private readonly string _secretKey;

        public SimpleCryptoServiceTests()
        {
            _simpleCryptoService = new SimpleCryptoService();
            _secretKey = "C5hHQvWWkUZNvKLgmbBimuiKruCW5qHp";
        }
        
        [Fact]
        public void AES_Success()
        {
            var contentToBeEncrypted = "Test AES Encryption";

            var cryptedContent = _simpleCryptoService.AES_Encrypt(contentToBeEncrypted, _secretKey);
            var decryptedContent = _simpleCryptoService.AES_Decrypt(cryptedContent, _secretKey);
            
            Assert.Equal(contentToBeEncrypted, decryptedContent);
        }
    }
}