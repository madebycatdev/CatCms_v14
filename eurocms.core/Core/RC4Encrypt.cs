using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroCMS.Core
{
    public class RC4Encrypt
    {
        protected int[] sbox = new int[256];
        protected int[] key = new int[256];

        public string PlainText
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public RC4Encrypt()
        {
        }

        public RC4Encrypt(string password)
        {
            this.Password = password;
        }

        public RC4Encrypt(string password, string text)
        {
            this.Password = password;
            this.PlainText = text;
        }

        private void RC4Initialize(string strPwd)
        {
            // Get the length of the password
            // Instead of Len(), we need to use the Length property
            // of the string
            int intLength = strPwd.Length;


            // Set up our for loop.  In C#, we need to change our syntax.

            // The first argument is the initializer.  Here we declare a
            // as an integer and set it equal to zero.

            // The second argument is expression that is used to test
            // for the loop termination.  Since our arrays have 256
            // elements and are always zero based, we need to loop as long
            // as a is less than or equal to 255.

            // The third argument is an iterator used to increment the
            // value of a by one each time through the loop.  Note that
            // we can use the ++ increment notation instead of a = a + 1
            for (int a = 0; a <= 255; a++)
            {
                // Since we don't have Mid()  in C#, we use the C#
                // equivalent of Mid(), String.Substring, to get a
                // single character from strPwd.  We declare a character
                // variable, ctmp, to hold this value.

                // A couple things to note.  First, the Mod keyword we
                // used in VB need to be replaced with the %
                // operator C# uses.  Next, since the return type of
                // String.Substring is a string, we need to convert it to
                // a char using String.ToCharArray() and specifying that
                // we want the first value in the array, [0].
               // char ctmp = (strPwd.Substring( (a % intLength) + 1, 1).ToCharArray()[0]);

                string ctmp = strPwd.Substring((a % intLength), 1);


                // We now have our character and need to get the ASCII
                // code for it.  C# doesn't have the  VB Asc(), but that
                // doesn't mean we can't use it.  In the beginning of our
                // code, we imported the Microsoft.VisualBasic namespace.
                // This allows us to use many of the native VB functions
                // in C#

                // Note that we need to use [] instead of () for our
                // array members.
                key[a] = RetAsciiCode(ctmp); //Microsoft.VisualBasic.Strings.Asc(ctmp);
                sbox[a] = a;
            }

            // Declare an integer x and initialize it to zero.
            int b = 0;

            // Again, create a for loop like the one above.  Note that we
            // need to use a different variable since we've already
            // declared a above.
            for (int a = 0; a <= 255; a++)
            {
                b = (b + sbox[a] + key[a]) % 256;
                int tempSwap = sbox[a];
                sbox[a] = sbox[b];
                sbox[b] = tempSwap;
            }
        }

        public string EnDeCrypt()
        {
            //if (string.IsNullOrEmpty(this.PlainText))
            //    throw new Exception("The PlainText property can not be empty");
            return EnDeCrypt(this.PlainText);
        }

        public string EnDeCrypt(string text)
        {
            int i = 0;
            int j = 0;
            string cipher = "";

            //if (string.IsNullOrEmpty(text))
            //    throw new Exception("The text parameter can not be empty");

            //if (string.IsNullOrEmpty(this.Password))
            //    throw new Exception("The Password property can not be empty");

            // Call our method to initialize the arrays used here.
            RC4Initialize(this.Password);

            // Set up a for loop.  Again, we use the Length property
            // of our String instead of the Len() function
            for (int a = 1; a <= text.Length; a++)
            {
                // Initialize an integer variable we will use in this loop
                int temp = 0;

                // Like the RC4Initialize method, we need to use the %
                // in place of Mod
                i = (i + 1) % 256;
                j = (j + sbox[i]) % 256;
                temp = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = temp;

                int k = sbox[(sbox[i] + sbox[j]) % 256];

                // Again, since the return type of String.Substring is a
                // string, we need to convert it to a char using
                // String.ToCharArray() and specifying that we want the
                // first value, [0].
                // char ctmp = PlainText.Substring(a - 1, 1).ToCharArray()[0];

                // Use Asc() from the Microsoft.VisualBasic namespace
                // temp = Microsoft.VisualBasic.Strings.Asc(ctmp);

                // Here we need to use ^ operator that C# uses for Xor
                //  int cipherby = temp ^ k;
                int cipherby = RetAsciiCode(text.Substring(a - 1, 1)) ^ k; //Microsoft.VisualBasic.Strings.Asc(text.Substring(a - 1, 1)) ^ k;
                // Use Chr() from the Microsoft.VisualBasic namespace
                cipher += RetAsciiChar(cipherby); //Microsoft.VisualBasic.Strings.Chr(cipherby);
            }

            // Return the value of cipher as the return value of our
            // method
            return cipher;
        }



        // VB Start

        private void VBRC4Initialize(string strPwd)
        {
            // Get the length of the password
            // Instead of Len(), we need to use the Length property
            // of the string
            int intLength = strPwd.Length;


            // Set up our for loop.  In C#, we need to change our syntax.

            // The first argument is the initializer.  Here we declare a
            // as an integer and set it equal to zero.

            // The second argument is expression that is used to test
            // for the loop termination.  Since our arrays have 256
            // elements and are always zero based, we need to loop as long
            // as a is less than or equal to 255.

            // The third argument is an iterator used to increment the
            // value of a by one each time through the loop.  Note that
            // we can use the ++ increment notation instead of a = a + 1
            for (int a = 0; a <= 255; a++)
            {
                // Since we don't have Mid()  in C#, we use the C#
                // equivalent of Mid(), String.Substring, to get a
                // single character from strPwd.  We declare a character
                // variable, ctmp, to hold this value.

                // A couple things to note.  First, the Mod keyword we
                // used in VB need to be replaced with the %
                // operator C# uses.  Next, since the return type of
                // String.Substring is a string, we need to convert it to
                // a char using String.ToCharArray() and specifying that
                // we want the first value in the array, [0].
                // char ctmp = (strPwd.Substring( (a % intLength) + 1, 1).ToCharArray()[0]);

                string ctmp = strPwd.Substring((a % intLength), 1);


                // We now have our character and need to get the ASCII
                // code for it.  C# doesn't have the  VB Asc(), but that
                // doesn't mean we can't use it.  In the beginning of our
                // code, we imported the Microsoft.VisualBasic namespace.
                // This allows us to use many of the native VB functions
                // in C#

                // Note that we need to use [] instead of () for our
                // array members.
                key[a] = Microsoft.VisualBasic.Strings.Asc(ctmp);
                sbox[a] = a;
            }

            // Declare an integer x and initialize it to zero.
            int b = 0;

            // Again, create a for loop like the one above.  Note that we
            // need to use a different variable since we've already
            // declared a above.
            for (int a = 0; a <= 255; a++)
            {
                b = (b + sbox[a] + key[a]) % 256;
                int tempSwap = sbox[a];
                sbox[a] = sbox[b];
                sbox[b] = tempSwap;
            }
        }

        public string VBEnDeCrypt()
        {
            //if (string.IsNullOrEmpty(this.PlainText))
            //    throw new Exception("The PlainText property can not be empty");
            return VBEnDeCrypt(this.PlainText);
        }

        public string VBEnDeCrypt(string text)
        {
            int i = 0;
            int j = 0;
            string cipher = "";

            //if (string.IsNullOrEmpty(text))
            //    throw new Exception("The text parameter can not be empty");

            //if (string.IsNullOrEmpty(this.Password))
            //    throw new Exception("The Password property can not be empty");

            // Call our method to initialize the arrays used here.
            VBRC4Initialize(this.Password);

            // Set up a for loop.  Again, we use the Length property
            // of our String instead of the Len() function
            for (int a = 1; a <= text.Length; a++)
            {
                // Initialize an integer variable we will use in this loop
                int temp = 0;

                // Like the RC4Initialize method, we need to use the %
                // in place of Mod
                i = (i + 1) % 256;
                j = (j + sbox[i]) % 256;
                temp = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = temp;

                int k = sbox[(sbox[i] + sbox[j]) % 256];

                // Again, since the return type of String.Substring is a
                // string, we need to convert it to a char using
                // String.ToCharArray() and specifying that we want the
                // first value, [0].
                // char ctmp = PlainText.Substring(a - 1, 1).ToCharArray()[0];

                // Use Asc() from the Microsoft.VisualBasic namespace
                // temp = Microsoft.VisualBasic.Strings.Asc(ctmp);

                // Here we need to use ^ operator that C# uses for Xor
                //  int cipherby = temp ^ k;
                int cipherby = Microsoft.VisualBasic.Strings.Asc(text.Substring(a - 1, 1)) ^ k;
                // Use Chr() from the Microsoft.VisualBasic namespace
                cipher += Microsoft.VisualBasic.Strings.Chr(cipherby);
            }

            // Return the value of cipher as the return value of our
            // method
            return cipher;
        }

        // VB End



        public int RetAsciiCode(string MyString)
        {

            if (MyString.Length == 0)

                return 0;

            else if (MyString.Length > 1)

                MyString = MyString[0].ToString();

            int AsciiCodeO = (int)System.Convert.ToChar(MyString);

            byte[] AsciiCodeB = System.Text.Encoding.ASCII.GetBytes(MyString);

            //int AsciiCode = System.Convert.ToInt32(AsciiCodeB);

            return AsciiCodeO;

        }

        public string RetAsciiChar(int AsciiCode)
        {

            return System.Convert.ToChar(AsciiCode).ToString();

        }

    
    
    
    }
}
