using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuscaWebServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> listSitesServices = new List<string>();
            List<string> listSitesETBLIB = new List<string>();
            //string rutaServidor = "G:/inetpub/wwwroot/etbserver/";
            string rutaServidor = txtRuta.Text;

            List<string> urlSitiosServer = Directory.GetDirectories(rutaServidor).ToList();

            foreach (var sitio in urlSitiosServer)
            {
                List<string> carpetasSitio = Directory.GetDirectories(sitio).ToList();
                foreach (var item in carpetasSitio)
                {
                    if (item.Contains("Web References") || item.Contains("Service References"))
                    {
                        string ruta="";
                        if (item.Contains("Web References"))
                        {
                            ruta = item.Replace("Web References", "");
                        }
                        else if (item.Contains("Service References"))
                        {
                            ruta = item.Replace("Service References", "");
                        }
                        listSitesServices.Add((ruta.Replace(rutaServidor, "")).Replace("\\",""));
                    }
                    if (item.Contains("bin")) {
                        if (File.Exists(item + "/ETB.Lib.dll")) {
                            listSitesETBLIB.Add(item.Replace(rutaServidor, ""));
                        }
                    }
                }
            }

            listSitesServices = listSitesServices.Distinct().ToList();
            listSitesServices = listSitesServices.Union(listSitesETBLIB).ToList();

            string rutaTxt = txtRutaArchivo + txtNombreTxt.Text + ".txt";
            using (StreamWriter writer = new StreamWriter(rutaTxt))
            {
                foreach (var item in listSitesServices)
                {
                    writer.WriteLine(item);
                }
            }

            lblMsg.Text = "Listo " + txtNombreTxt.Text;
        }
    }
}
