using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Security.Cryptography;


/*
 * XML STRUCTURE
 *
 * <USERS>
  
 *  <MYUSER>
   
 *      <USER>filippo.balla</USER>
   
 *      <PASSWORD>fballa1</PASSWORD>
   
 *      <NAME>filippo</NAME>
   
 *      <SURNAME>balla</SURNAME>
  
 *  </MYUSER>  

 * </USERS>
 * 
 * 
 * 
 * <CLIENTS>
 * 
 *      <CLIENT>filippo.balla</CLIENT>
 *          
 *      <PASSWORD>fballa1</PASSWORD>
 *      
 * </CLIENTS>
*/

namespace ProgettoPDS_SERVER
{
    class XmlManager
    {
        private XmlDocument XmlDoc;
        private const string FileNameUsers = "XMLUsers.xml";
        private const string FileNameClients = "XMLClients.xml";
        private string FileName;
        private bool errorLoad;
      
        public XmlManager( char c )
        {
            string path;
            this.XmlDoc = new XmlDocument();
            this.errorLoad = false;

            if (c == 'U')
                this.FileName = XmlManager.FileNameUsers;
            else
                this.FileName = XmlManager.FileNameClients;

            path = Path.Combine(Environment.CurrentDirectory, this.FileName);

            if (File.Exists(path))
                this.XmlDoc.Load(FileName);
            else {
                MessageBox.Show("Il file \"" + path + "\" non è stato trovato!! Informazioni non caricate!!", "ERRORE",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorLoad = true;
            }
        }

        public bool Error
        {
            get { return this.errorLoad; }
            set { this.errorLoad = value; }
        }


        // Aggiunta di un nuovo Utente al file "XMLUsers.xml" .
        // La password inserita nel documento sarà cifrata.
        public void AddNewUser(User NuovoUtente)
        {
            XmlNode root = this.XmlDoc.DocumentElement;
            SHA1 shaM = new SHA1Managed();

            //Create a new node.
            XmlElement myUser = this.XmlDoc.CreateElement("MYUSER");
            XmlElement campo = this.XmlDoc.CreateElement("USERNAME");
            campo.InnerText = NuovoUtente.Username;
            myUser.AppendChild(campo);
            campo = this.XmlDoc.CreateElement("PASSWORD");
            byte[] pwd = Encoding.ASCII.GetBytes(NuovoUtente.Password);
            campo.InnerText = Encoding.ASCII.GetString(shaM.ComputeHash(pwd)); //Cifratura Password!!
            myUser.AppendChild(campo);
            campo = this.XmlDoc.CreateElement("NAME");
            campo.InnerText = NuovoUtente.Name;
            myUser.AppendChild(campo);
            campo = this.XmlDoc.CreateElement("SURNAME");
            campo.InnerText = NuovoUtente.Surname;
            myUser.AppendChild(campo);

            //Add the new Node
            root.AppendChild(myUser);

            this.XmlDoc.Save(this.FileName);
            this.XmlDoc.Save("..\\..\\" + this.FileName);

        }

        // Aggiunta di un nuovo "Client" al file "XMLClients.xml".
        // La funzione si asppetta di ricevere due parametri di tipo stringa: lo username e la
        // password ( già cifrata lato CLIENT ) dell'utente (client) che desidera usare il server,
        // durante la fase di autenticazione/registrazione.
        public void AddNewClient(string user, string pwd) 
        {
            XmlNode root = this.XmlDoc.DocumentElement;

            //Create a new node.
            XmlElement myClient = this.XmlDoc.CreateElement("CLIENT");
            XmlElement campo = this.XmlDoc.CreateElement("USER");
            campo.InnerText = user;
            myClient.AppendChild(campo);
            campo = this.XmlDoc.CreateElement("PASSWORD");
            campo.InnerText = pwd;
            myClient.AppendChild(campo);

            //Add the new Node
            root.AppendChild(myClient);

            this.XmlDoc.Save(this.FileName);
            this.XmlDoc.Save("..\\..\\" + this.FileName);
        }

        // Ricerca di un utente all'interno del file "XMLUsers.xml".
        // La funzione ritorna un oggetto User opportunamente inizializzato oppure null!!
        public User SearchUser(string user)
        {
            XmlNodeList xmlnodes;
            User aux;

            xmlnodes = this.XmlDoc.GetElementsByTagName("MYUSER");

            for (int i = 0; i < xmlnodes.Count; i++) {

                if (xmlnodes[i].ChildNodes.Item(0).InnerText == user) {
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

        // La funzione SearchClient cerca all'interno del file "XMLClients.xml" se è presente un determinato
        // utente che desidera usare il server ( user/client). Se è presente la funzione ritornerà la password
        // salvata nel file che dovrà essere confrontata con quella che è giunta al server dal client!!
        public string SearchClient(string user) 
        {
            string pwd = null;
            XmlNodeList xmlnodes = this.XmlDoc.GetElementsByTagName("CLIENT");

            for (int i = 0; i < xmlnodes.Count; i++) {

                if (xmlnodes[i].ChildNodes.Item(0).InnerText == user) {
                    pwd = xmlnodes[i].ChildNodes.Item(1).InnerText;
                    return pwd;
                }
            }

            return pwd;
        }

        // La funzione ModifyPwdUser cerca all'interno del file "XMLClients.xml" se è presente un determinato utente
        // e in caso affermativo aggiorna il suo campo password. La password è passata in chiaro come secondo parametro
        // alla funzione la quale si occuperà anche della cifratura.
        // La funzione ritornerà true se la modifica è stata effettuata, false altrimenti.
        public bool ModifyPwdUser(string user, string pwd)
        {
            XmlNodeList xmlnodes = this.XmlDoc.GetElementsByTagName("MYUSER");
            SHA1 shaM = new SHA1Managed();

            for (int i = 0; i < xmlnodes.Count; i++)
            {

                if (xmlnodes[i].ChildNodes.Item(0).InnerText == user)
                {
                    byte[] password = Encoding.ASCII.GetBytes(pwd);
                    xmlnodes[i].ChildNodes.Item(1).InnerText = Encoding.ASCII.GetString(shaM.ComputeHash(password));
                    this.XmlDoc.Save(this.FileName);
                    this.XmlDoc.Save("..\\..\\" + this.FileName);
                    return true;
                }
            }

            return false;
        }

        public void RemoveUsers(User u) 
        {
            XmlDocument documentOne = new XmlDocument();
            documentOne.Load(this.FileName);
            XmlNodeList xmlnodes = documentOne.GetElementsByTagName("MYUSER");
            bool res1, res2;

            for (int i = 0; i < xmlnodes.Count; i++) {
                res1 = (xmlnodes[i].ChildNodes.Item(0).InnerText == u.Username);
                res2 = (xmlnodes[i].ChildNodes.Item(1).InnerText == u.Password);

                if ( res1 && res2 ) {
                    
                    XmlNode a1 = xmlnodes[i].ParentNode;
                    xmlnodes[i].RemoveAll();
                    a1.RemoveChild(xmlnodes[i]);
                    documentOne.Save(this.FileName);
                    documentOne.Save("..\\..\\" + this.FileName);
                    break;
                }
            } 

        }

    }
}
