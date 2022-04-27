using EuroCMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EuroCMSKeygen
{
    public partial class Keygen : Form
    {
        public Keygen()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            string domains = txtDomains.Text.Trim();

            if (domains.Length < 1)
            {
                MessageBox.Show("En az bir domain gereklidir!");
                return;
            }

            string formattedDomains = "," + domains + ",";
 
            RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4.PlainText = domains;

            string generatedKey = CmsHelper.EncodeBase64(rc4.EnDeCrypt());

            txtKey.Text = generatedKey;
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text;

            if (key.Length < 1)
            {
                MessageBox.Show("Bu işlemi yapabilmek için encode edilmiş bir key gereklidir!");
                return;
            }
   
            string decoded64 = CmsHelper.DecodeBase64(key);

            RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4.PlainText = decoded64;

            string generatedKey = rc4.EnDeCrypt();

            txtKey.Text = generatedKey;
        }

        // VB Start

        private void btnVBEncode_Click(object sender, EventArgs e)
        {
            string domains = txtDomains.Text.Trim();

            if (domains.Length < 1)
            {
                MessageBox.Show("En az bir domain gereklidir!");
                return;
            }

            string formattedDomains = "," + domains + ",";
 
            RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4.PlainText = domains;

            string generatedKey = CmsHelper.EncodeBase64(rc4.VBEnDeCrypt());

            txtKey.Text = generatedKey;
        }

        private void btnVBDecode_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text;

            if (key.Length < 1)
            {
                MessageBox.Show("Bu işlemi yapabilmek için encode edilmiş bir key gereklidir!");
                return;
            }
   
            string decoded64 = CmsHelper.DecodeBase64(key);

            RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4.PlainText = decoded64;

            string generatedKey = rc4.VBEnDeCrypt();

            txtKey.Text = generatedKey;
        }

        private void btnVBToCShapDecode_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text;

            if (key.Length < 1)
            {
                MessageBox.Show("Bu işlemi yapabilmek için encode edilmiş bir key gereklidir!");
                return;
            }
   
            string decoded64 = CmsHelper.DecodeBase64(key);

            RC4Encrypt rc4VB = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4VB.PlainText = decoded64;

            string domains = rc4VB.VBEnDeCrypt();

            if (domains.Length < 1)
            {
                MessageBox.Show("En az bir domain gereklidir!");
                return;
            }

            

            string formattedDomains = "," + domains + ",";

            RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4.PlainText = domains;

            string generatedKey = CmsHelper.EncodeBase64(rc4.EnDeCrypt());

            txtKey.Text = generatedKey;


        }

        // VB End

        private void machineKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineKeyGen form = new MachineKeyGen();
            form.Show();
        }
    }
}
