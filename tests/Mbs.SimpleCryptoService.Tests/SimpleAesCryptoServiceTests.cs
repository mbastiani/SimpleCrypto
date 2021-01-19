using Mbs.SimpleCrypto.Services;
using Mbs.SimpleCrypto.Services.Interfaces;
using System;
using Xunit;

namespace Mbs.SimpleCrypto.Tests
{
    public class SimpleAesCryptoServiceTests
    {
        private readonly ISimpleAesCryptoService _simpleAesCryptoService;
        private readonly string _privateEncryptionKey;
        private readonly string _contentToBeEncrypted;

        public SimpleAesCryptoServiceTests()
        {
            _simpleAesCryptoService = new SimpleAesCryptoService();
            _privateEncryptionKey = "C5hHQvWWkUZNvKLgmbBimuiKruCW5qHp";
            _contentToBeEncrypted = "Test AES Encryption";
        }
        
        [Fact]
        public void AES_Encrypt_And_Decrypt_Success()
        {
            var encryptedContent = _simpleAesCryptoService.AesEncrypt(_contentToBeEncrypted, _privateEncryptionKey);
            var decryptedContent = _simpleAesCryptoService.AesDecrypt(encryptedContent, _privateEncryptionKey);
            
            Assert.Equal(_contentToBeEncrypted, decryptedContent);
        }

        [Fact]
        public void AES_Encrypt_Without_Content_Must_Fail()
        {
            var exception = Record.Exception(()=> _simpleAesCryptoService.AesEncrypt(null, _privateEncryptionKey));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void AES_Encrypt_Without_Key_Must_Fail()
        {
            var exception = Record.Exception(() => _simpleAesCryptoService.AesEncrypt(_contentToBeEncrypted, null));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void AES_Encrypt_With_Invalid_Key_Must_Fail()
        {
            var exception = Record.Exception(() => _simpleAesCryptoService.AesEncrypt(_contentToBeEncrypted, "123"));
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public void AES_Decrypt_Without_Content_Must_Fail()
        {
            var exception = Record.Exception(() => _simpleAesCryptoService.AesDecrypt(null, _privateEncryptionKey));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void AES_Decrypt_Without_Key_Must_Fail()
        {
            var exception = Record.Exception(() => _simpleAesCryptoService.AesDecrypt(_contentToBeEncrypted, null));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void AES_Decrypt_With_Invalid_Key_Must_Fail()
        {
            var exception = Record.Exception(() => _simpleAesCryptoService.AesDecrypt(_contentToBeEncrypted, "123"));
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }
    }
}