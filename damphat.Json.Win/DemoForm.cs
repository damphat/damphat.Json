using System;
using System.Windows.Forms;

namespace damphat.Json.Win
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private void DemoForm_Load(object sender, EventArgs e)
        {
        }

        private void Update()
        {
            try
            {
                var src = txtSrc.Text;
                var obj = JSON.Parse(src);
                txtDes.Text = JSON.Stringify(obj, 2);
            }
            catch (Exception e)
            {
                txtDes.Text = e.Message;
            }
        }

        private void txtSrc_TextChanged(object sender, EventArgs e)
        {
            Update();
        }
    }
}