﻿using System;
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
        internal XmlDocument documentOne, documentTwo;
        private string FileUsers, FileServers;

        #region Constructor

        public XmlManager() 
        {
            this.documentOne = new XmlDocument();
            this.documentTwo = new XmlDocument();
            this.FileUsers = "XMLUsers.xml";
            this.FileServers = "XMLServers.xml";
            this.documentOne.Load(this.FileUsers);
            this.documentTwo.Load(this.FileServers);
        }

        #endregion

        #region Methods related to Users

        public User SearchUser(string user) 
        {
            XmlNodeList xmlnodes;
            User aux;

            xmlnodes = this.documentOne.GetElementsByTagName("MYUSER");

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

        public bool ModifyPwdUser(string user, string pwd) 
        {
            XmlNodeList xmlnodes = this.documentOne.GetElementsByTagName("MYUSER");
            SHA1 shaM = new SHA1Managed();

            for (int i = 0; i < xmlnodes.Count; i++) {

                if (xmlnodes[i].ChildNodes.Item(0).InnerText == user) {
                    byte[] password = Encoding.ASCII.GetBytes(pwd);
                    xmlnodes[i].ChildNodes.Item(1).InnerText = Encoding.ASCII.GetString(shaM.ComputeHash(password));
                    this.documentOne.Save(this.FileUsers);
                    this.documentOne.Save("..\\..\\" + this.FileUsers);
                    return true;
                }
            } 

            return false;
        }

        public void AddNewUser( User NuovoUtente ) 
        {
            XmlNode root = this.documentOne.DocumentElement;
            SHA1 shaM = new SHA1Managed();

            //Create a new node.
            XmlElement myUser = this.documentOne.CreateElement("MYUSER");
            XmlElement campo = this.documentOne.CreateElement("USERNAME");
            campo.InnerText = NuovoUtente.Username;
            myUser.AppendChild(campo);
            campo = this.documentOne.CreateElement("PASSWORD");
            byte[] pwd = Encoding.ASCII.GetBytes(NuovoUtente.Password);
            campo.InnerText = Encoding.ASCII.GetString(shaM.ComputeHash(pwd)); //Cifratura Password!!
            myUser.AppendChild(campo);
            campo = this.documentOne.CreateElement("NAME");
            campo.InnerText = NuovoUtente.Name;
            myUser.AppendChild(campo);
            campo = this.documentOne.CreateElement("SURNAME");
            campo.InnerText = NuovoUtente.Surname;
            myUser.AppendChild(campo);

            //Add the new Node
            root.AppendChild(myUser);

            this.documentOne.Save(this.FileUsers);
            this.documentOne.Save("..\\..\\" + this.FileUsers);

        }

        #endregion

        #region Methods related to Servers

        public bool AddNewServer(Server s)
        {
            if (FindServer(s)) {
                MessageBox.Show("Questa Configurazione è stata già salvata in precedenza!!", "AVVISO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            XmlNode root = this.documentTwo.DocumentElement;

            //Create a new node.
            XmlElement myServer = this.documentTwo.CreateElement("MYSERVER");
            XmlElement campo = this.documentTwo.CreateElement("HOSTNAME");
            campo.InnerText = s.HostName;
            myServer.AppendChild(campo);
            campo = this.documentTwo.CreateElement("IP_ADDRESS");
            campo.InnerText = s.IPAddr;
            myServer.AppendChild(campo);
            campo = this.documentTwo.CreateElement("PORT");
            campo.InnerText = s.Porta.ToString();
            myServer.AppendChild(campo);

            //Add the new Node
            root.AppendChild(myServer);

            this.documentTwo.Save(this.FileServers);
            this.documentTwo.Save("..\\..\\" + this.FileServers);

            return true;
            
        }

        public bool FindServer(Server s) 
        {
            XmlNodeList xmlnodes = this.documentTwo.GetElementsByTagName("MYSERVER");

            for (int i = 0; i < xmlnodes.Count; i++) {

                if (xmlnodes[i].ChildNodes.Item(0).InnerText == s.HostName &&
                    xmlnodes[i].ChildNodes.Item(1).InnerText == s.IPAddr &&
                     xmlnodes[i].ChildNodes.Item(2).InnerText == s.Porta.ToString())
                    return true;               
            }

            return false;
        }

        public void RemoveServers()
        {
            XmlNodeList xmlnodes = this.documentTwo.GetElementsByTagName("SERVERS");

            xmlnodes[0].RemoveAll();
            this.documentTwo.Save(this.FileServers);
            this.documentTwo.Save("..\\..\\" + this.FileServers);

        }

        public void RemoveUsers(User u) 
        {
            XmlNodeList xmlnodes = this.documentOne.GetElementsByTagName("MYUSER");
            bool res1, res2;

            for (int i = 0; i < xmlnodes.Count; i++) {
                res1 = (xmlnodes[i].ChildNodes.Item(0).InnerText == u.Username);
                res2 = (xmlnodes[i].ChildNodes.Item(1).InnerText == u.Password);

                if ( res1 && res2 ) {
                    XmlNode a1 = xmlnodes[i].ParentNode;
                    xmlnodes[i].RemoveAll();
                    a1.RemoveChild(xmlnodes[i]);
                    this.documentOne.Save(this.FileUsers);
                    this.documentOne.Save("..\\..\\" + this.FileUsers);
                    break;
                }
            }

        }

        #endregion
    }
}
