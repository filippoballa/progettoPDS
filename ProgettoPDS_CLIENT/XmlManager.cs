using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ProgettoPDS_CLIENT
{
    class XmlManager
    {
        private XmlDocument document;
        private string FileName;

        public XmlManager(string file) 
        {
            this.document = new XmlDocument();
            this.FileName = file;
            this.document.Load(file);
        }

        public User SearchUser(string user) 
        {
            XmlNodeList xmlnodes;
            User aux;

            xmlnodes = this.document.GetElementsByTagName("MYUSER");

            for (int i = 0; i < xmlnodes.Count; i++) {

                if ( xmlnodes[i].ChildNodes.Item(0).InnerText == user) {
                    aux = new User();
                    aux.Username = xmlnodes[i].ChildNodes.Item(0).InnerText;
                    aux.Password = xmlnodes[i].ChildNodes.Item(1).InnerText;
                    aux.Name = xmlnodes[i].ChildNodes.Item(2).InnerText;
                    aux.Surname = xmlnodes[i].ChildNodes.Item(3).InnerText;

                    return aux;
                }
            }

            return null;
        }

        public void AddNewUser( User NuovoUtente ) 
        {
            XmlNode root = this.document.DocumentElement;
            SHA1 shaM = new SHA1Managed();

            //Create a new node.
            XmlElement myUser = this.document.CreateElement("MYUSER");
            XmlElement campo = this.document.CreateElement("USERNAME");
            campo.InnerText = NuovoUtente.Username;
            myUser.AppendChild(campo);
            campo = this.document.CreateElement("PASSWORD");
            byte[] pwd = Encoding.ASCII.GetBytes(NuovoUtente.Password);
            campo.InnerText = Encoding.ASCII.GetString(shaM.ComputeHash(pwd)); //Cifratura Password!!
            myUser.AppendChild(campo);
            campo = this.document.CreateElement("NAME");
            campo.InnerText = NuovoUtente.Name;
            myUser.AppendChild(campo);
            campo = this.document.CreateElement("SURNAME");
            campo.InnerText = NuovoUtente.Surname;
            myUser.AppendChild(campo);

            //Add the new Node
            root.AppendChild(myUser);

            this.document.Save(this.FileName);
            this.document.Save("..\\..\\" + this.FileName);

        }
        

    }
}
