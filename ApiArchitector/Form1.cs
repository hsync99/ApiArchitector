using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            
            Panel panel = new Panel();
           
            panel.BorderStyle = BorderStyle.FixedSingle;
            draggblePanel = panel;
            panel.Location = new System.Drawing.Point(100, 100);
            panel.MouseDown += new MouseEventHandler(this.draggableButton_MouseDown);
            panel.MouseMove += new MouseEventHandler(this.draggableButton_MouseMove);
            panel.MouseUp += new MouseEventHandler(this.draggableButton_MouseUp);
            panel.Width = 300;
            panel.Height = 100;
            
            var combobox = new ComboBox();
            
            combobox.DropDown += new EventHandler(comboboxDropDownEvent);
           
            
            panel.Name = Guid.NewGuid().ToString();
            panelList.Add(panel.Name);
           
           
            panel.Controls.Add(combobox);
            this.Controls.Add(panel);
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
