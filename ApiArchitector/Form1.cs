using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;

namespace ApiArchitector
{
    public partial class Form1 : Form
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        private Point lastPanel;
        private Panel draggblePanel;
        private ObservableCollection<string> panelList;
        private DialogForm dialog;
        public Form1()
        {
            InitializeComponent();
            panelList = new ObservableCollection<string>();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            var name = string.Empty;
            dialog = new DialogForm(panelList.ToList());
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                name = dialog.formName;

                Panel panel = new Panel();

                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Tag = name;
                panel.BackColor = Color.White;
                draggblePanel = panel;
                panel.Location = new System.Drawing.Point(100, 100);
                panel.MouseDown += new MouseEventHandler(this.draggableButton_MouseDown);
                panel.MouseMove += new MouseEventHandler(this.draggableButton_MouseMove);
                panel.MouseUp += new MouseEventHandler(this.draggableButton_MouseUp);
                panel.Width = 150;
                panel.Height = 100;
                var cfgbtn = new Button();

                Image gearimg = Properties.Resources.Two_gear_icon;
                cfgbtn.BackgroundImage = gearimg;
                cfgbtn.Width = 25;
                cfgbtn.Height = 25;
                cfgbtn.Location = new Point(125, 0);
                cfgbtn.FlatAppearance.BorderSize = 0;
                cfgbtn.FlatStyle = FlatStyle.Flat;
                cfgbtn.BackgroundImageLayout = ImageLayout.Stretch;
                var combobox = new ComboBox();
                var label1 = new Label();
                var blockname = new Label();
                blockname.Text = name;
                blockname.ForeColor = Color.Green;
                blockname.Location = new Point(5, 80);
                label1.Text = "следующий блок";
                label1.Width = 200;
                label1.Location = new System.Drawing.Point(5, 0);
                combobox.Location = new System.Drawing.Point(5,25);
                var button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Text = "Исполнить";
                button.Location = new Point(0,50);
                button.Click += new EventHandler(btn_executeClicked);

                combobox.DropDown += new EventHandler(comboboxDropDownEvent);


                panel.Name = name;
                panelList.Add(panel.Name);
                panel.Controls.Add(cfgbtn);
                panel.Controls.Add(button);
                panel.Controls.Add(blockname);
                panel.Controls.Add(label1);
                panel.Controls.Add(combobox);
                this.Controls.Add(panel);
            }
            else if (dialog.DialogResult == DialogResult.OK) {
                return;
            }
           
        }

        private void dialogClosed(object sender, FormClosingEventArgs e) { 
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }
        private void comboboxDropDownEvent(object sender, EventArgs e) {
            ComboBox cb = sender as ComboBox;
            if(cb.Items.Count < panelList.Count)
            {
                cb.Items.Clear();
                foreach (string element in panelList)
                {
                    cb.Items.Add(element);
                }
            }
            

           
            
        }
        private void draggableButton_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                draggblePanel = null;
            }
        }
        private void draggableButton_MouseMove(object sender, MouseEventArgs e) {
            if (isDragging)
            {
                Point newCursor = Cursor.Position;
                int dx = newCursor.X - lastCursor.X;
                int dy = newCursor.Y - lastCursor.Y;
                draggblePanel.Location = new Point(lastPanel.X + dx, lastPanel.Y + dy);
            }
        }
        private void btn_executeClicked(object sender, EventArgs e) {
            Button btn = sender as Button;
            Panel parenPanel = btn.Parent as Panel;
            string currentPanelName = parenPanel.Name;
            parenPanel.BackColor = Color.Green;
            Point cbPoit = new Point(5, 25);
            var cb = parenPanel.GetChildAtPoint(cbPoit) as ComboBox;
            var nextpanelname = cb.SelectedItem.ToString();
            Panel nextPanel = this.Controls.Find(nextpanelname, true)[0] as Panel;
            MessageBox.Show("блок" + currentPanelName + " исполнен!");
            parenPanel.BackColor = Color.White;
           
            Execute(nextPanel);
            
        }
        private void Execute(Panel p) {
            Point cbPoit = new Point(5, 25);
            p.BackColor = Color.Green;
           
            var cb = p.GetChildAtPoint(cbPoit) as ComboBox; ;
            var nextpanelName = cb.SelectedItem?.ToString();
            MessageBox.Show("блок " + p.Name + " исполнен");
            p.BackColor= Color.White;
            if (nextpanelName != null) {
                Panel nextPanel = this.Controls.Find(nextpanelName, true)[0] as Panel;
                Execute(nextPanel);
            }
           
        }
        private void draggableButton_MouseDown(object sender, MouseEventArgs e) {
            if(sender.GetType() == typeof(Panel))
            {
                draggblePanel = (Panel) sender; 
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = true;
                    lastCursor = Cursor.Position;
                    lastPanel = draggblePanel.Location;
                }
            }
        }

    }
}
