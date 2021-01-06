using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace MyFinance.Utils
{
    public class ReflectorUtility
    {
        public static object FollowPropertyPath(object value, string path)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (path == null) throw new ArgumentNullException("path");

            Type currentType = value.GetType();

            object obj = value;
            foreach (string propertyName in path.Split('.'))
            {
                if (currentType != null)
                {
                    PropertyInfo property = null;
                    int brackStart = propertyName.IndexOf("[");
                    int brackEnd = propertyName.IndexOf("]");

                    property = currentType.GetProperty(brackStart > 0 ? propertyName.Substring(0, brackStart) : propertyName);
                    if (property != null)
                    {
                        obj = property.GetValue(obj, null);

                        if (brackStart > 0)
                        {
                            string index = propertyName.Substring(brackStart + 1, brackEnd - brackStart - 1);
                            foreach (Type iType in obj.GetType().GetInterfaces())
                            {
                                if (iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                                {
                                    obj = typeof(ReflectorUtility).GetMethod("GetDictionaryElement")
                                                         .MakeGenericMethod(iType.GetGenericArguments())
                                                         .Invoke(null, new object[] { obj, index });
                                    break;
                                }
                                if (iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IList<>))
                                {
                                    obj = typeof(ReflectorUtility).GetMethod("GetListElement")
                                        .MakeGenericMethod(iType.GetGenericArguments())
                                        .Invoke(null, new object[] { obj, index });
                                    break;
                                }
                            }
                        }

                        currentType = obj != null ? obj.GetType() : null; //property.PropertyType;
                    }
                    else return null;
                }
                else return null;
            }
            return obj;
        }
        public static TValue GetDictionaryElement<TKey, TValue>(IDictionary<TKey, TValue> dict, object index)
        {
            TKey key = (TKey)Convert.ChangeType(index, typeof(TKey), null);
            return dict[key];
        }
        public static T GetListElement<T>(IList<T> list, object index)
        {
            int m_Index = Convert.ToInt32(index);
            T m_T = list.Count > m_Index ? list[m_Index] : default(T);

            return m_T;
        }
    }

    public class XmlUtility
    {
        public static string Serialize<T>(T value)
        {
            string m_Xml = string.Empty;

            XmlSerializer m_XmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream m_MemoryStream = new MemoryStream())
            {
                m_XmlSerializer.Serialize(m_MemoryStream, value);
                m_MemoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader m_StreamReader = new StreamReader(m_MemoryStream))
                {
                    m_Xml = m_StreamReader.ReadToEnd();
                }
            }

            return m_Xml;
        }
        public static T DeSerialize<T>(string xml)
        {
            T m_Value = default(T);

            if (!string.IsNullOrEmpty(xml))
            {
                XmlSerializer m_XmlSerializer = new XmlSerializer(typeof(T));
                using (StringReader m_StringReader = new StringReader(xml))
                {
                    m_Value = (T)m_XmlSerializer.Deserialize(m_StringReader);
                }
            }

            return m_Value;
        }
        public static string Serialize<T>(T value, Type[] types)
        {
            string m_Xml = string.Empty;

            XmlSerializer m_XmlSerializer = new XmlSerializer(typeof(T), types);
            using (MemoryStream m_MemoryStream = new MemoryStream())
            {
                m_XmlSerializer.Serialize(m_MemoryStream, value);
                m_MemoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader m_StreamReader = new StreamReader(m_MemoryStream))
                {
                    m_Xml = m_StreamReader.ReadToEnd();
                }
            }

            return m_Xml;
        }
    }
    public class EncryptDecryptUtility
    {
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            return Encrypt(toEncrypt, useHashing, Default_Key);
        }
        public static string Encrypt(string toEncrypt, bool useHashing, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string cipherString, bool useHashing)
        {
            return Decrypt(cipherString, useHashing, Default_Key);
        }
        public static string Decrypt(string cipherString, bool useHashing, string key)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private static string Default_Key = "P@ssw0rd";

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class XsltUtility
    {
        public string Transform(string xmlData, string xsl)
        {
            if (string.IsNullOrEmpty(xsl) || string.IsNullOrEmpty(xmlData))
                return string.Empty;

            string m_Html = string.Empty;

            //Xsl
            XmlNode m_XmlNodeXsl = this.CreateXmlNode(xsl);
            XslCompiledTransform m_XslCompiledTransform = new XslCompiledTransform();
            XsltSettings m_XsltSettings = XsltSettings.Default;
            m_XslCompiledTransform.Load(m_XmlNodeXsl, m_XsltSettings, new XmlUrlResolver());

            using (StringReader m_StringReader = new StringReader(xmlData))
            {
                using (XmlReader m_XmlReader = XmlReader.Create(m_StringReader))
                {
                    using (StringWriter m_StringWriter = new StringWriter())
                    {
                        m_XslCompiledTransform.Transform(m_XmlReader, null, m_StringWriter);
                        m_Html = m_StringWriter.ToString();
                    }
                }
            }

            return m_Html;
        }

        private XmlNode CreateXmlNode(string xmlData)
        {
            XmlNode m_XmlNodeData = null;

            XmlDocument m_XmlDocument = new XmlDocument();
            m_XmlDocument.LoadXml(xmlData);
            if (m_XmlDocument.ChildNodes.Count > 0)
                m_XmlNodeData = m_XmlDocument.ChildNodes.Cast<XmlNode>().LastOrDefault();

            return m_XmlNodeData;
        }
    }

}