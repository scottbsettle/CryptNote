using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public partial class OpenSource : Form
    {
        public OpenSource()
        {
            InitializeComponent();
        }
        public string GetSource()
        {
            return SourceText.Text;
        }

        private void CancelSource_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
