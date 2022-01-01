using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using Azure.Security.KeyVault.Secrets;

// ReadKeyVault();
KeyVaultEncryption();

void ReadKeyVault()
{
    const string tenantId = "ac11fdf6-cc10-4290-9f63-eaabb6cd0b33";
    const string clientId = "a1dd25e0-c3bd-444f-9289-564719ec1b94";
    const string clientSecret = "Wi57Q~X4Wge3bXDTfyvShYajyy3y5X.~jIjWs";
    const string secretName = "dbPassword";

    var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    var keyVaulturl = new Uri("https://mnl2002.vault.azure.net/");
    var secretClient = new SecretClient(keyVaulturl, clientSecretCredential);
    var secret = secretClient.GetSecret(secretName);
    Console.WriteLine($"Secret value id is {secret.Value.Value}");

}

void KeyVaultEncryption()
{
    const string tenantId = "tenant id here";
    const string clientId = "client id here";
    const string clientSecret = "secret here";
    const string keyName = "key1";
    const string textToEncrypt = "this ia plain text";

    var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    var keyVaulturl = new Uri("https://mnl2002.vault.azure.net/");
    var secretClient = new KeyClient(keyVaulturl, clientSecretCredential);
    var key = secretClient.GetKey(keyName);
    var cryptoClient = new CryptographyClient(key.Value.Id, clientSecretCredential);
    var textToBytes = Encoding.UTF8.GetBytes(textToEncrypt);
    var encryptedResult = cryptoClient.Encrypt(EncryptionAlgorithm.RsaOaep256, textToBytes);
    Console.WriteLine("Encrypted text");
    Console.WriteLine(Convert.ToBase64String(encryptedResult.Ciphertext));
    var decryptResult = cryptoClient.Decrypt(EncryptionAlgorithm.RsaOaep256, encryptedResult.Ciphertext);

    Console.WriteLine("Plain text");
    Console.WriteLine(Encoding.UTF8.GetString(decryptResult.Plaintext));




}