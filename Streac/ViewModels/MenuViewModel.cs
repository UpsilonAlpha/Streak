using System.Windows.Forms;
using Caliburn.Micro;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;

namespace Streac.ViewModels
{
    public class MenuViewModel : Caliburn.Micro.Screen
    {
        public static string FileName { get; set; }

        public async void OpenFileDialog()
        {
            /*
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
            }
            */
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.quizlet.com/2.0/sets/373203818/terms?client_id=R3snf5zu9W&whitespace=1");

            var proxy = WebRequest.GetSystemWebProxy();
            Uri testUrl = new Uri("http://proxy.example.com");
            Uri proxyUrl = proxy.GetProxy(testUrl);
            if (proxyUrl != testUrl)
            {
                WebProxy myProxy = new WebProxy("http://proxy2.eq.edu.au:80/");
                myProxy.Credentials = new NetworkCredential("uadhi2", "ScienceMathTech1");
                request.Proxy = myProxy;
            }

            HttpWebResponse response =  await request.GetResponseAsync() as HttpWebResponse;
            var responseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(responseStream);
            var json = myStreamReader.ReadToEnd();
            FileName = json;
            Debug.WriteLine(FileName);

            var parentConductor = (Conductor<object>)(this.Parent);
            parentConductor.ActivateItem(new PlayerViewModel());
        }
    }
}
