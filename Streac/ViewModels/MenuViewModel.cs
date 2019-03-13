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

        public void OpenFileDialog()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
            }

            var parentConductor = (Conductor<object>)(this.Parent);
            parentConductor.ActivateItem(new PlayerViewModel());
        }

        public void CreateButton()
        {
            var parentConductor = (Conductor<object>)(this.Parent);
            parentConductor.ActivateItem(new CreateViewModel());
        }
    }
}
