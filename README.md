# .Net Simple Crypto Package

A library to provide a simple crypto service. 

Currently supports only AES 256-bit encryption.


[![Nuget](https://img.shields.io/nuget/v/Mbs.SimpleCrypto.svg)](https://www.nuget.org/packages/Mbs.SimpleCrypto)

## Installing

You can install from NuGet using the following command:

`Install-Package Mbs.SimpleCrypto`

Or via the Visual Studio package manager.

## Setup

If you want to use dependency injection, you need register the crypto service with the service collection (generally in _Startup.cs_ file):

```c#
using Mbs.SimpleCrypto.Services;
using Mbs.SimpleCrypto.Services.Interfaces;

public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ISimpleAesCryptoService, SimpleAesCryptoService>();
}
``` 

If you want use mannualy, just do it:

```c#
using Mbs.SimpleCrypto.Services;
using Mbs.SimpleCrypto.Services.Interfaces;

public static async Task Main(string[] args)
{
    ISimpleAesCryptoService simpleAesCryptoService = new SimpleAesCryptoService();
}
```

## Usage 
If you use dependency injection, inject the `ISimpleAesCryptoService` in your class:

```c#
using Mbs.SimpleCrypto.Services;
using Mbs.SimpleCrypto.Services.Interfaces;

public class MyClass {

    private readonly ISimpleAesCryptoService _simpleAesCryptoService;

    public MyClass(ISimpleAesCryptoService simpleAesCryptoService)
    {
        _simpleAesCryptoService = simpleAesCryptoService;
    }

    public void MyMethod()
    {
        //Private encryption key must be 256 bits long (32 characters)
        string privateEncryptionKey = "C5hHQvWWkUZNvKLgmbBimuiKruCW5qHp";

        string encryptedContent = _simpleAesCryptoService.AesEncrypt("ContentToBeEncrypted", privateEncryptionKey);
        string decryptedContent = _simpleAesCryptoService.AesDecrypt(encryptedContent, _privateEncryptionKey);
    }
}
```

If you use mannualy:

```c#
using Mbs.SimpleCrypto.Services;
using Mbs.SimpleCrypto.Services.Interfaces;

public class MyClass {

    public void MyMethod()
    {
        ISimpleAesCryptoService simpleAesCryptoService = new SimpleAesCryptoService();
        
        //Private encryption key must be 256 bits long (32 characters)
        string privateEncryptionKey = "C5hHQvWWkUZNvKLgmbBimuiKruCW5qHp";
        
        string encryptedContent = simpleAesCryptoService.AesEncrypt("ContentToBeEncrypted", privateEncryptionKey);
        string decryptedContent = simpleAesCryptoService.AesDecrypt(encryptedContent, _privateEncryptionKey);
    }
}
```
