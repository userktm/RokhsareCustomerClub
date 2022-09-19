using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Utility
{
    public class Hash
    {
        public static string HashPassword(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, System.Web.Configuration.FormsAuthPasswordFormat.SHA1.ToString());
        }
    }

    public class MD5Encryption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message">Data to Encrypt</param>
        /// <param name="Passphrase"></param>
        /// <returns></returns>
        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }

    public class EncryptSimple
    {
        private static string EncryptNumber(string key)
        {
            Random rnd = new Random();
            string result = "";
            for (int i = 0; i < key.Length; i++)
            {
                char chr = key[i];

                switch (chr)
                {
                    case '0':
                        result += "a";
                        break;
                    case '1':
                        result += "b";
                        break;
                    case '2':
                        result += "c";
                        break;
                    case '3':
                        result += "d";
                        break;
                    case '4':
                        result += "e";
                        break;
                    case '5':
                        result += "f";
                        break;
                    case '6':
                        result += "g";
                        break;
                    case '7':
                        result += "h";
                        break;
                    case '8':
                        result += "i";
                        break;
                    case '9':
                        result += "j";
                        break;

                }

                result += rnd.Next(1, 99999).ToString();


            }

            return result;
        }

        private static string DecryptNumber(string key)
        {
            string result = "";
            for (int i = 0; i < key.Length; i++)
            {
                char chr = key[i];
                switch (chr)
                {
                    case 'a':
                        result += "0";
                        break;
                    case 'b':
                        result += "1";
                        break;
                    case 'c':
                        result += "2";
                        break;
                    case 'd':
                        result += "3";
                        break;
                    case 'e':
                        result += "4";
                        break;
                    case 'f':
                        result += "5";
                        break;
                    case 'g':
                        result += "6";
                        break;
                    case 'h':
                        result += "7";
                        break;
                    case 'i':
                        result += "8";
                        break;
                    case 'j':
                        result += "9";
                        break;

                }
            }

            //char[] ckey = new char[key.Length];

            //for (int i = 0; i < key.Length; i++)
            //    ckey[i] = key[i];

            //byte[] bkeys = new byte[ckey.Length];
            //string[] keys = new string[bkeys.Length];
            //string targetkey = "";

            //for (int i = 0; i < bkeys.Length; i++)
            //{
            //    ckey[i] = key[i];
            //    bkeys[i] = (byte)((byte)ckey[i] - 50);
            //    if (bkeys[i].ToString().Length == 1)
            //        keys[i] = "0" + bkeys[i].ToString();
            //    else
            //        keys[i] = bkeys[i].ToString();

            //    targetkey += keys[i];
            //}

            return result;

            //return targetkey;
        }

        private static string Encryption(string key)
        {
            try
            {
                string result = "";
                if (!string.IsNullOrEmpty(key))
                {
                    int keylen = key.Length;
                    int keylenLength = keylen.ToString().Length;
                    int hexsplitcount = 6;
                    int asciiCount = 3;
                    string intstring = "";//= keylenLength.ToString() + keylen.ToString();

                    for (int i = 0; i < key.Length; i++)
                    {
                        string asciicode = ((byte)key[i]).ToString();

                        while (asciicode.Length < asciiCount)
                        {
                            asciicode = "0" + asciicode;
                        }


                        intstring += asciicode;
                    }
                    for (int i = 0; i < intstring.Length; i = i + hexsplitcount)
                    {

                        string tmp = i + hexsplitcount < intstring.Length ? intstring.Substring(i, hexsplitcount) : intstring.Substring(i);
                        string hex = string.Format("{0:X}", int.Parse(tmp));

                        result += hex + "-";

                    }
                    result = result.Remove(result.LastIndexOf('-'));
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string Decryption(string key)
        {
            try
            {
                string result = "";
                if (!string.IsNullOrEmpty(key))
                {
                    int hexsplitcount = 6;
                    int asciiCount = 3;
                    string[] hexstr = key.Split('-');
                    string intstring = "";

                    for (int i = 0; i < hexstr.Length; i++)
                    {
                        string hex = Convert.ToInt32(hexstr[i], 16).ToString();
                        while (hex.Length < hexsplitcount)
                            hex = "0" + hex;

                        intstring += hex;
                    }
                    for (int i = 0; i < intstring.Length; i = i + asciiCount)
                    {
                        string tmp = i + asciiCount < intstring.Length ? intstring.Substring(i, asciiCount) : intstring.Substring(i);
                        string ascii = tmp != "000" ? ((char)byte.Parse(tmp)).ToString() : "";
                        result += ascii;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Encrypt(string key)
        {
            string str = Encryption(key);

            return str;
        }

        public static string Decrypt(string hashedKey)
        {

            string str = Decryption(hashedKey);
            return str;

        }

        public static bool IsEncryptMode(string key)
        {
            string[] splitedkey = key.Split('-');

            if (splitedkey.Length > 2)
            {
                return true;
            }
            return false;
        }

    }

    public class PDPEncryption
    {
        public static string EncryptString(string toEncode)
        {
            //try
            //{
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            byte[] iv = { 8, 7, 6, 5, 4, 3, 2, 1 };

            PDP_Security enc = new PDP_Security(key, iv);
            string encoded = enc.Encrypt(toEncode);
            //string encoded = Security.
            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(encoded);
            return Convert.ToBase64String(toEncodeAsBytes);
            //}
            //catch (Exception ex)
            //{
            //    //do your error handling here  
            //}
            //ExtractAndSerialize.Encryption64 oES =
            //new ExtractAndSerialize.Encryption64();
            //return oES.Encrypt(strQueryString, "!#$a54?3");

        }

        public static string DecryptString(string toDecrypt)
        {
            //try
            //{
            byte[] encodedDataAsBytes = Convert.FromBase64String(toDecrypt.Replace(" ", "+"));
            //return Encoding.UTF8.GetString(encodedDataAsBytes);
            string decoded = Encoding.UTF8.GetString(encodedDataAsBytes);
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            byte[] iv = { 8, 7, 6, 5, 4, 3, 2, 1 };

            PDP_Security enc = new PDP_Security(key, iv);
            return enc.Decrypt((string)decoded);
            //}
            //catch (Exception ex)
            //{
            //    //do your error handling here  
            //}
        }
    }

    public class PDP_Security
    {

        private TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        private UTF8Encoding utf8 = new UTF8Encoding();

        private byte[] keyValue;
        private byte[] iVValue;

        /// <summary>
        /// Key to use during encryption and decryption
        /// </summary>
        /// <remarks>
        /// <example>
        /// byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        /// </example>
        /// </remarks>
        public byte[] Key
        {
            get { return keyValue; }
            set { keyValue = value; }
        }

        /// <summary>
        /// Initialization vetor to use during encryption and decryption
        /// </summary>
        /// <remarks>
        /// <example>
        /// byte[] iv = { 8, 7, 6, 5, 4, 3, 2, 1 };
        /// </example>
        /// </remarks>
        public byte[] iV
        {
            get { return iVValue; }
            set { iVValue = value; }
        }

        /// <summary>
        /// Constructor, allows the key and initialization vetor to be provided
        /// </summary>
        /// <param name="key"><see cref="Key"/></param>
        /// <param name="iV"><see cref="iV"/></param>
        public PDP_Security(byte[] key, byte[] iV)
        {
            this.keyValue = key;
            this.iVValue = iV;
        }

        /// <summary>
        /// Decrypt bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Decrypted data as bytes</returns>
        public byte[] Decrypt(byte[] bytes)
        {
            return Transform(bytes, des.CreateDecryptor(this.keyValue, this.iVValue));
        }

        /// <summary>
        /// Encrypt bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Encrypted data as bytes</returns>
        public byte[] Encrypt(byte[] bytes)
        {
            return Transform(bytes, des.CreateEncryptor(this.keyValue, this.iVValue));
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Decrypted data as string</returns>
        public string Decrypt(string text)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = Transform(input, des.CreateDecryptor(this.keyValue, this.iVValue));
            return utf8.GetString(output);
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Encrypted data as string</returns>
        public string Encrypt(string text)
        {
            byte[] input = utf8.GetBytes(text);
            byte[] output = Transform(input, des.CreateEncryptor(this.keyValue, this.iVValue));
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Encrypt or Decrypt bytes.
        /// </summary>
        /// <remarks>
        /// This is used by the public methods
        /// </remarks>
        /// <param name="input">Data to be encrypted/decrypted</param>
        /// <param name="cryptoTransform">
        /// <example>des.CreateEncryptor(this.keyValue, this.iVValue)</example>
        /// </param>
        /// <returns>Byte data containing result of opperation</returns>
        private byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
        {
            // Create the necessary streams
            MemoryStream memory = new MemoryStream();
            CryptoStream stream = new CryptoStream(memory, cryptoTransform, CryptoStreamMode.Write);

            // Transform the bytes as requesed
            stream.Write(input, 0, input.Length);
            stream.FlushFinalBlock();

            // Read the memory stream and convert it back into byte array
            memory.Position = 0;
            byte[] result = new byte[memory.Length];
            memory.Read(result, 0, result.Length);

            // Clean up
            memory.Close();
            stream.Close();

            // Return result
            return result;
        }

    }
}
