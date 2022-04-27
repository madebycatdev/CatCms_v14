using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EuroCMSKeygen
{
    public partial class MachineKeyGen : Form
    {
        public MachineKeyGen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 64;

            switch (comboBox1.SelectedItem.ToString())
            { 
                case "SHA1":
                    a = 64;
                    break;
                case "AES":
                    a = 48;
                    break;
                 case "3DES":
                    a = 24;
                    break;
            }

            string decryptionKey = CreateKey(a);
            string validationKey = CreateKey(a);

            txtDecryptionKey.Text = decryptionKey;
            txtValidationKey.Text = validationKey;
        }

        static String CreateKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];

            rng.GetBytes(buff);
            return BytesToHexString(buff);
        }

        static String BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }
    }
}
