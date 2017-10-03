using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace NoMoreViews
{
    class Encoder
    {
        private string RKey = "dofkadsosxcaxofchjaosrdedofkrrpl";
        private string IV = "zxcvbnmdfrasdfgh";

        public Encoder(string Key)
        {
            int x = 0;
            while(Key.Length < 32)
            {
                Key += Key[x];
                if ((x + 1) > Key.Length)
                    x = 0;
                else
                    x++;
            }
            RKey = Key;
        }

        public Encoder(){}

        public void FileEncode(string path)
        {
            FileInfo fli = new FileInfo(path);
            byte[] bits = File.ReadAllBytes(fli.FullName);
            string str = Encoding.Default.GetString(bits);
            str = fli.Extension + "\n" + str;
            File.WriteAllBytes(fli.FullName.Substring(0, fli.FullName.LastIndexOf('.')) + ".nmv", Encoding.Default.GetBytes(Encript(str)));
            File.Delete(path);
        }

        public void FileDecode(string path)
        {
            FileInfo fli = new FileInfo(path);
            byte[] bits = File.ReadAllBytes(fli.FullName);
            string str = Decrypt(Encoding.Default.GetString(bits));
            string ext = str.Split(new string[] {"\n"}, StringSplitOptions.None)[0];
            str = str.Remove(0,str.IndexOf("\n") + 1);
            File.WriteAllBytes(fli.FullName.Substring(0, fli.FullName.LastIndexOf('.')) + ext, Encoding.Default.GetBytes(str));
            File.Delete(path);
        }

        public void FolderEncode(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo fi in dir.GetFiles())
                FileEncode(fi.FullName);
            foreach (DirectoryInfo di in dir.GetDirectories())
                FolderEncode(di.FullName);
        }
        
        public void FolderDecode(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo fi in dir.GetFiles())
                if (fi.Extension == ".nmv")
                    FileDecode(fi.FullName);
            foreach (DirectoryInfo di in dir.GetDirectories())
                FolderDecode(di.FullName);
        }

        private string Encript(string str)
        {
            byte[] plaintextbytes = Encoding.Default.GetBytes(str);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = Encoding.Default.GetBytes(RKey);
            aes.IV = Encoding.Default.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            crypto.Dispose();
            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string str)
        {
            try
            {
                byte[] encryptedbytes = Convert.FromBase64String(str);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = Encoding.Default.GetBytes(RKey);
                aes.IV = Encoding.Default.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
                crypto.Dispose();
                return Encoding.Default.GetString(secret);
            }
            catch (Exception)
            {
                MessageBox.Show("La contraseña no era la acertada");
                return "Error";
            }
        }
    }
}
