using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace Journal
{
    public class _32bitEncryption
    {
        string Plain_Text;
        byte[] Encrypted_Bytes;
        string Encrypted_sting;
        //This class here the Rijndael is what will have most all of the methods we need to do aes encryption.
        //When this is called it will create both a key and Initialization Vector to use.
        RijndaelManaged Crypto = new RijndaelManaged();
        //This is just here to convert the Encrypted byte array to a string for viewing purposes.
        UTF8Encoding UTF = new UTF8Encoding();
        public _32bitEncryption() { }
        public _32bitEncryption(string _text, byte[] Key, byte[] IV)
        {
            Plain_Text = _text;
            Crypto.Key = Key;
            Crypto.IV = IV;
            Encrypted_Bytes = encrypt_function(Plain_Text, Crypto.Key, Crypto.IV);
            Encrypted_sting = ReturnEncrtpedbytetostring();
        }
        #region     GettersAndSetters
        public string ReturnEncrtpedbytetostring()
        {
            return UTF.GetString(Encrypted_Bytes);
        }
        public string ReturnEncrtpedbytetostring(byte[] _tmp)
        {
            return UTF.GetString(_tmp);
        }
        public RijndaelManaged GetCrypto()
        {
            return Crypto;
        }
        public void SetCrypto(RijndaelManaged _tmp)
        {
            Crypto = _tmp;
        }
        public void SetText(string _str)
        {
            Plain_Text = _str;
        }
        public string GetText()
        {
            return Plain_Text;
        }
        public void SetEncrptedText(string _str)
        {
            Encrypted_sting = _str;
        }
        public string GetEncryptrdText()
        {
            return Encrypted_sting;
        }
        public byte[] GetEncrptBytes()
        {
            return Encrypted_Bytes;
        }
        #endregion
        public byte[] encrypt_function(string plainText, byte[] Key, byte[] IV)

        {
            byte[] initVectorBytes = IV;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Properties.Settings.Default.Password, null);
            byte[] keyBytes = password.GetBytes(256 / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Padding = PaddingMode.Zeros;
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
           return cipherTextBytes;
            #region Doesnt work
            //RijndaelManaged Crypto = null;
            //MemoryStream MemStream = null;
            ////I crypto transform is used to perform the actual decryption vs encryption, hash function are a version of crypto transforms.
            //ICryptoTransform Encryptor = null;
            ////Crypto streams allow for encryption in memory.
            //CryptoStream Crypto_Stream = null;
            //System.Text.UTF8Encoding Byte_Transform = new System.Text.UTF8Encoding();
            ////Just grabbing the bytes since most crypto functions need bytes.
            //byte[] PlainBytes = Byte_Transform.GetBytes(Plain_Text);
            //try
            //{
            //    Crypto = new RijndaelManaged();
            //    Crypto.Key = Key;
            //    Crypto.IV = IV;
            //    MemStream = new MemoryStream();
            //    //Calling the method create encryptor method Needs both the Key and IV these have to be from the original Rijndael call
            //    //If these are changed nothing will work right.
            //    Encryptor = Crypto.CreateEncryptor(Crypto.Key, Crypto.IV);
            //    //The big parameter here is the cryptomode.write, you are writing the data to memory to perform the transformation
            //    Crypto_Stream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write);
            //    //The method write takes three params the data to be written (in bytes) the offset value (int) and the length of the stream (int)
            //    Crypto_Stream.Write(PlainBytes, 0, PlainBytes.Length);
            //}
            //finally
            //{
            //    //if the crypto blocks are not clear lets make sure the data is gone
            //    if (Crypto != null)
            //        Crypto.Clear();
            //    //Close because of my need to close things then done.
            //    Crypto_Stream.Close();
            //}
            ////Return the memory byte array
            //return MemStream.ToArray();
            //if (plainText == null || plainText.Length <= 0)
            //    throw new ArgumentNullException("plainText");
            //if (Key == null || Key.Length <= 0)
            //    throw new ArgumentNullException("Key");
            //if (IV == null || IV.Length <= 0)
            //    throw new ArgumentNullException("IV");
            //byte[] encrypted;
            //// Create an Aes object
            //// with the specified key and IV.
            //using (Aes aesAlg = Aes.Create())
            //{
            //    aesAlg.Key = Key;
            //    aesAlg.IV = IV;

            //    // Create a decrytor to perform the stream transform.
            //    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            //    // Create the streams used for encryption.
            //    using (MemoryStream msEncrypt = new MemoryStream())
            //    {
            //        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            //        {
            //            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            //            {

            //                //Write all data to the stream.
            //                swEncrypt.Write(plainText);
            //            }
            //            encrypted = msEncrypt.ToArray();
            //        }
            //    }
            //}


            // Return the encrypted bytes from the memory stream.
            //return encrypted;
            #endregion
        }
        public string decrypt_function(byte[] cipherText, byte[] Key, byte[]  IV )
        {
           
            byte[] initVectorBytes = IV;
            byte[] cipherTextBytes = cipherText;
            PasswordDeriveBytes password = new PasswordDeriveBytes(Properties.Settings.Default.Password, null);
            byte[] keyBytes = password.GetBytes(256 / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Padding = PaddingMode.Zeros;
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
     
        public byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}

