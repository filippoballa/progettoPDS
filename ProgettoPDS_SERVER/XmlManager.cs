using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ProgettoPDS_SERVER
{
    class XmlManager
    {
        private string root = "C:\\Users\\filippo\\Documents\\GitHub\\progettoPDS\\ProgettoPDS_SERVER\\";// Path.GetDirectoryName(Application.UserAppDataPath);
        private string FileName;
        private static string FileNameUsers = "XMLUsers.xml";
        private static string FileNameClients = "XMLClients.xml";
        private XmlDocument XmlDoc;
        private Rijndael myRijndael;
        private byte[] Key = new byte[32] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 };
        private byte[] IV = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5};

         public XmlManager(char c)
        {
            if (c == 'C')
                FileName = FileNameClients;
            else if(c =='U')
                FileName = FileNameUsers;

            this.XmlDoc = new XmlDocument();
            string p;

            if (File.Exists(p = Path.Combine(root, FileName)))
                XmlDoc.Load(p);
            else
                MessageBox.Show(p + "\n file non trovato!");

            myRijndael = Rijndael.Create();
            myRijndael.Key = Key;
            myRijndael.IV = IV;
        }
         public string SearchUser(string user)//potrebbe essere un'operazione pesante
         {
             string pswd = null;
             int i;
             XmlNodeList xmlnode;
             byte[] encrypted;

             try{

                 xmlnode = XmlDoc.GetElementsByTagName("MYUSER");

                 for (i = 0; i <= xmlnode.Count - 1; i++)//scorre tutti  i nodi MYUSER
                 {
                     encrypted = GetBytes(xmlnode[i].ChildNodes.Item(0).InnerText.Trim());//decodifica il campo 0 (USER) codificato sul file
                     if (user == DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV))//se è uguale
                     {
                         encrypted = GetBytes(xmlnode[i].ChildNodes.Item(1).InnerText.Trim());//decodifica il campo 1 (PASSWORD) codificato sul file
                         pswd = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);//e lo decodifica
                     }
                 }
             }catch(Exception e)
             {
                 MessageBox.Show(e.Message);
             }
             return pswd;
         }
         static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
         {
             // Check arguments.
             if (cipherText == null || cipherText.Length <= 0)
                 throw new ArgumentNullException("cipherText");
             if (Key == null || Key.Length <= 0)
                 throw new ArgumentNullException("Key");
             if (IV == null || IV.Length <= 0)
                 throw new ArgumentNullException("Key");

             // Declare the string used to hold
             // the decrypted text.
             string plaintext = null;

             // Create an Rijndael object
             // with the specified key and IV.
             using (Rijndael rijAlg = Rijndael.Create())
             {
                 rijAlg.Key = Key;
                 rijAlg.IV = IV;

                 // Create a decrytor to perform the stream transform.
                 ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                 // Create the streams used for decryption.
                 using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                 {
                     using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                     {
                         using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                         {

                             // Read the decrypted bytes from the decrypting stream
                             // and place them in a string.
                             plaintext = srDecrypt.ReadToEnd();
                         }
                     }
                 }

             }

             return plaintext;

         }
         static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
         {
             // Check arguments.
             if (plainText == null || plainText.Length <= 0)
                 throw new ArgumentNullException("plainText");
             if (Key == null || Key.Length <= 0)
                 throw new ArgumentNullException("Key");
             if (IV == null || IV.Length <= 0)
                 throw new ArgumentNullException("Key");
             byte[] encrypted;
             // Create an Rijndael object
             // with the specified key and IV.
             using (Rijndael rijAlg = Rijndael.Create())
             {
                 rijAlg.Key = Key;
                 rijAlg.IV = IV;

                 // Create a decrytor to perform the stream transform.
                 ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                 // Create the streams used for encryption.
                 using (MemoryStream msEncrypt = new MemoryStream())
                 {
                     using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                     {
                         using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                         {

                             //Write all data to the stream.
                             swEncrypt.Write(plainText);
                         }
                         encrypted = msEncrypt.ToArray();
                     }
                 }
             }


             // Return the encrypted bytes from the memory stream.
             return encrypted;

         }
         static byte[] GetBytes(string str)
         {
             byte[] bytes = new byte[str.Length * sizeof(char)];
             System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
             return bytes;
         }

         static string GetString(byte[] bytes)
         {
             char[] chars = new char[bytes.Length / sizeof(char)];
             System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
             return new string(chars);
         }
        public void AddNewUser(string user, string pswd)
        {
            try
            {
                string p;
                if (File.Exists(p = Path.Combine(this.root, FileNameUsers)))
                    XmlDoc.Load(p);
                else
                    MessageBox.Show(p + "\n file non trovato!");

                XmlNode root = XmlDoc.DocumentElement;

                //Create a new node.
                XmlElement myUser = XmlDoc.CreateElement("MYUSER");

                XmlElement campo = XmlDoc.CreateElement("USER");
                campo.InnerText = GetString(EncryptStringToBytes(user, myRijndael.Key, myRijndael.IV));
                myUser.AppendChild(campo);

                campo = XmlDoc.CreateElement("PASSWORD");
                campo.InnerText = GetString(EncryptStringToBytes(user, myRijndael.Key, myRijndael.IV));
                myUser.AppendChild(campo);

                //Add the new Node
                root.AppendChild(myUser);

                XmlDoc.Save(p);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }
        public void AddNewClient()
        {}

    }
}
