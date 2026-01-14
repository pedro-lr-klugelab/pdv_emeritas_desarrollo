using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Farmacontrol_PDV.HELPERS
{
	class Crypto_helper
	{
		private static string prm_key = "sJwZQdixgSsto8HfP0ILv7g/8FMZyOQJAOJSkrpVhfk=";
		private static string prm_iv = "Ic5ArLqcq5/b/DSGnakbE6DGnmR8w7U+Ncxd+z39QGY=";

		public static string Encrypt(string prm_text_to_encrypt)
		{
			var sToEncrypt = prm_text_to_encrypt;

			var rijndael = new RijndaelManaged()
			{
				Padding = PaddingMode.PKCS7,
				Mode = CipherMode.CBC,
				KeySize = 256,
				BlockSize = 256,
			};

			var encryptor = rijndael.CreateEncryptor(Convert.FromBase64String(prm_key), Convert.FromBase64String(prm_iv));

			var msEncrypt = new MemoryStream();
			var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

			var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

			csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
			csEncrypt.FlushFinalBlock();

			var encrypted = msEncrypt.ToArray();

			return (Convert.ToBase64String(encrypted));
		}

		public static string Decrypt(string prm_text_to_decrypt)
		{
			var sEncryptedString = prm_text_to_decrypt;

			var rijndael = new RijndaelManaged()
			{
				Padding = PaddingMode.PKCS7,
				Mode = CipherMode.CBC,
				KeySize = 256,
				BlockSize = 256,
			};

			var decryptor = rijndael.CreateDecryptor(Convert.FromBase64String(prm_key), Convert.FromBase64String(prm_iv));

			var sEncrypted = Convert.FromBase64String(sEncryptedString);

			var fromEncrypt = new byte[sEncrypted.Length];

			var msDecrypt = new MemoryStream(sEncrypted);
			var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

			csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

			return (Encoding.ASCII.GetString(fromEncrypt));
		}

		public static string Generate_key()
		{
			var rijndael = new RijndaelManaged()
			{
				Padding = PaddingMode.PKCS7,
				Mode = CipherMode.CBC,
				KeySize = 256,
				BlockSize = 256,
			};

			rijndael.GenerateIV();

			return Convert.ToBase64String(rijndael.IV);
		}

		public static string Generate_iv()
		{
			var rijndael = new RijndaelManaged()
			{
				Padding = PaddingMode.PKCS7,
				Mode = CipherMode.CBC,
				KeySize = 256,
				BlockSize = 256,
			};

			rijndael.GenerateIV();

			return Convert.ToBase64String(rijndael.IV);
		}
	}
}
