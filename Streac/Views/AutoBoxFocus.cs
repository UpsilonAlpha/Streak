using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Streac.Views
{
    public class AutoBoxFocus : AutoCompleteBox
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var textBox = Template.FindName("Text", this) as TextBox;
            if (textBox != null) Keyboard.Focus(textBox);
        }
    }
}
