using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caliburn.Micro;

namespace Streac.ViewModels
{
    class CreateViewModel : Caliburn.Micro.Screen
    {
        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                NotifyOfPropertyChange(() => Code);
            }
        }

        public async void SaveFileDialog()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var URL = "https://api.quizlet.com/2.0/sets/" + Code + "/terms?client_id=R3snf5zu9W&whitespace=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            var proxy = WebRequest.GetSystemWebProxy();
            Uri testUrl = new Uri("http://proxy.example.com");
            Uri proxyUrl = proxy.GetProxy(testUrl);

            if (proxyUrl != testUrl)
            {
                WebProxy myProxy = new WebProxy("http://proxy2.eq.edu.au:80/");
                myProxy.Credentials = new NetworkCredential("uadhi2", "ScienceMathTech2");
                request.Proxy = myProxy;
            }

            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            var responseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(responseStream);
            var json = myStreamReader.ReadToEnd();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog.FileName;
                File.WriteAllText(name, json);
            }


            var parentConductor = (Conductor<object>)(this.Parent);
            parentConductor.ActivateItem(new MenuViewModel());
        }
    }
}
