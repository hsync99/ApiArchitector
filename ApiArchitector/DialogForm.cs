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
    public partial class DialogForm : Form
    {
        public string formName { get; set; }
        private List<string> panels;
        public DialogForm(List<string> panelList)
        {
            InitializeComponent();
            if (panelList == null)
            {
                panels = new List<string>();
            }
            else {
                panels = panelList;
            }
            
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox1.Text))
            {
                formName = textBox1.Text;
                if (panels.Contains(formName))
                {
                    DialogResult = DialogResult.Cancel;
                    MessageBox.Show($"Блок с именем: [{formName}] уже существует");
                    Close();
                }
                else {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();    
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {

        }
    }
}
