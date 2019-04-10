using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Атака_методом_неподвижной_точки
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //int N = Convert.ToInt32(textBox1.Text);
            BigInteger N = BigInteger.Parse("114381625757888867669235779976146612010218296721242362562561842935706935245733897830597123563958705058989075147599290026879543541");
            //int key_e = Convert.ToInt32(textBox2.Text);
            BigInteger key_e = BigInteger.Parse("9007");
            //int C = Convert.ToInt32(textBox3.Text);
            BigInteger C = BigInteger.Parse("96869613754622061477140922254355882905759991124574319874695120930816298225145708356931476622883989628013391990551829945157815154");
            //int k_max = Convert.ToInt32(textBox4.Text);
            //BigInteger k_max = 
            
            BigInteger NmodE = BigInteger.Divide(N, key_e);
            BigInteger EmultMod = BigInteger.Multiply(key_e, NmodE);
            BigInteger k_max = NmodE - 1;
            BigInteger count_iter = EmultMod - key_e;
            do
            {
                count_iter = count_iter + EmultMod;
                k_max += NmodE;
                while (count_iter < N)
                {
                    count_iter += key_e;
                    k_max++;
                }
                //count_iter = BigInteger.ModPow(count_iter + EmultMod, 1, N);
                //k_max += NmodE;
                count_iter = BigInteger.ModPow(count_iter, 1, N);
            }
            while (count_iter != 1);
            textBox1.Text = N.ToString();
            textBox2.Text = key_e.ToString();
            textBox3.Text = C.ToString();
            textBox4.Text = k_max.ToString();


            //int C_e = C.PowMod(key_e, N);
            BigInteger C_e = BigInteger.ModPow(C, key_e, N);
            //int C_last = C;
            BigInteger C_last = C;
            //int k = 1;
            BigInteger k = 1;

            /*
            comboBox1.Text = "Все итерации";
            comboBox1.Items.Clear();
            comboBox1.Items.Add(C.ToString());
            comboBox1.Items.Add(C_e.ToString());
            */
            
            while (C_e != C && k < k_max)
            {
                //ЛУЧШЕ считать до порядка e mod N
                C_last = C_e;
                //C_e = C_e.PowMod(key_e, N);
                C_e = BigInteger.ModPow(C_e, key_e, N);
                //comboBox1.Items.Add(C_e.ToString());
                Console.WriteLine(k.ToString());
                k++;
            }
            if (C_e == C)
                label1.Text = C_last.ToString();
            else
                label1.Text = "Неподвижная точка не найдена";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int N = Convert.ToInt32(textBox1.Text);
            int key_e = Convert.ToInt32(textBox2.Text);
            
            int k = 1;
            int count = key_e;
            do
            {
                count = (count + key_e).Mod(N);
                k++;
            }
            while (count != 1);
            textBox4.Text = k.ToString();
        }
    }
    public static class Int32Extensions
    {
        public static int GetInverse(this int a, int p)
        {
            // Реализован расширенный алгоритм Евклида
            int c = a, d = p, u, v;
            int uc = 1, vc = 0, ud = 0, vd = 1;

            while (c != 0)
            {
                int q = d / c;
                int temp;
                temp = c;
                c = d - q * c;
                d = temp;

                temp = uc;
                uc = ud - q * uc;
                ud = temp;

                temp = vc;
                vc = vd - q * vc;
                vd = temp;
            }
            u = ud < 0 ? ud + p : ud;
            v = vd < 0 ? vd + p : vd;

            return (d == 1) ? u : 0;
        }
        public static int PowMod(this int a, int pow, int mod)
        {
            a = a.Mod(mod);
            int res = 1;
            int buf = a;
            for (int i = 1; i <= pow; i *= 2)
            {
                if (i > 1)
                    buf = (buf * buf).Mod(mod);
                if ((pow & i) > 0)
                {
                    res *= buf;
                }
            }
            return res.Mod(mod);
        }
        public static int Mod(this int a, int p)
        {
            return ((a % p) + p) % p;
        }
    }
}
