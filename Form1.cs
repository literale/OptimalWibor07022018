using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimalWibor07022018
{
    public partial class Form1 : Form
    {
        struct TItem
        {
            public int weight;
            public int cost;
        }
        static int n = 15; //кол-во предметов
        TItem[] a = new TItem[n];
        int maxWeight;
        int maxCost;//текущая наибольшая стоимость
        int totCost;//текущая стоимость
        int s;//текущая выборка
        int sopt;//оптимальная выборка
        public Form1()
        {
            InitializeComponent();
           // btnOpt.Enabled = false;
            dataGridView1.RowCount = 3;
            dataGridView1.ColumnCount = n;
           
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Columns[i].Width = 45;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void Opt(int i, int weight, int cost)
        {

            if (weight + a[i].weight <= maxWeight)
            {
                s |= 1 << i;
                if (i < n - 1)
                    Opt(i + 1, weight + a[i].weight, cost);
                else
                    if (cost > maxCost)
                {
                    maxCost = cost;
                    sopt = s;
                }
                s &= ~(1 << i);
            }
            int tmp = cost - a[i].cost;
            if (tmp > maxCost)
                if (i < n - 1)
                    Opt(i + 1, weight, tmp);
                else
                {
                    maxCost = tmp; sopt = s;
                }

        }

        private void btnOpt_Click(object sender, EventArgs e)
        {
            totCost = 0;
            for (int i = 0; i<n; i++)
            {
                a[i].weight = Convert.ToInt32(dataGridView1[i, 0].Value);
                a[i].cost = Convert.ToInt32(dataGridView1[i, 1].Value);
                totCost = totCost + a[i].cost;
                dataGridView1[i, 2].Value = "";

            }
            maxWeight = Convert.ToInt32(textBox1.Text);
            maxCost = 0;
            s = 0;
            sopt = 0;
            Opt(0, 0, totCost);
            for (int i = 0; i <= n-1; i++)
                if ((sopt & (1 << i)) != 0)
                    dataGridView1[i, 2].Value = "V";
        }

        private void btnRand_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            a = new TItem[n];
            for (int i = 0; i < n; i++)
            {
                a[i].cost = rnd.Next(10) + 1;
                a[i].weight = rnd.Next(20) + 1;
                dataGridView1[i, 0].Value = a[i].weight;
                dataGridView1[i, 1].Value = a[i].cost;
                dataGridView1[i, 2].Value = "";
            }
            btnOpt.Enabled = true;

        }
    }
}
