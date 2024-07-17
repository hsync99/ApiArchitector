using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiArchitector
{
    public partial class BlockConfigurationForm : Form
    {
        public BlockConfClass conf;
        public BlockConfigurationForm()
        {
            InitializeComponent();
        }

        private void cb_request_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conf = new BlockConfClass();
            conf.URL = tb_url.Text;
            conf.Method = cb_request_type.SelectedIndex.ToString() ;
            conf.RequestBody = tb_body.Text;
        }
    }
}
