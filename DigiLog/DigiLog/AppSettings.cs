using System.Security.Cryptography;

public class AppSettings
{
    public string Secret { get; set; }

    public void GenerateRandomKey()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] key = new byte[32];
            rng.GetBytes(key);
            Secret = Convert.ToBase64String(key);
        }
    }
}
