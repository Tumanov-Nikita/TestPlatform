﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestView;
using TestService.ViewModels;

namespace TestView
{
    public partial class FormMain : Form
    {
        public Color colorBack;
        public Color colorFont;

        public FormMain()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                List<PatternViewModel> list =
                    Task.Run(() => ApiClient.GetRequestData<List<PatternViewModel>>("api/Patterns/GetList")).Result;
                if (list != null)
                {
                    dataGridViewAvailablePatterns.DataSource = list;
                    dataGridViewAvailablePatterns.Columns[0].Visible = false;
                    dataGridViewAvailablePatterns.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                List<TestViewModel> listС =
                    Task.Run(() => ApiClient.GetRequestData<List<TestViewModel>>("api/PassedTests/GetList")).Result;
                if (listС != null)
                {
                    dataGridViewPassedTests.DataSource = listС;
                    dataGridViewPassedTests.Columns[0].Visible = false;
                    dataGridViewPassedTests.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
                     
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                colorFont = cd.Color;
                buttonChangeColorFont.BackColor = colorFont;
            }
            this.ForeColor = colorFont;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                colorBack = cd.Color;
                buttonChangeColorFont.BackColor = colorBack;
            }
            this.BackColor = colorBack;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TestingForm Testing = new TestingForm();
            Close();
            Testing.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            FormAuthorization Authorization = new FormAuthorization();
            Close();
            Authorization.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //textBoxCurrentUser.Text = ;
            //textBoxGroupUser.Text = ;

            this.ForeColor = colorFont;
            this.BackColor = colorBack;
        }

        private void buttonStatistic_Click(object sender, EventArgs e)
        {
            StatisticForm statisticForm = new StatisticForm();
            statisticForm.Show();
        }

        private void dataGridViewAvailablePatterns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAvailablePatterns.SelectedRows.Count == 1)
            {
                int Id = Convert.ToInt32(dataGridViewAvailablePatterns.SelectedRows[0].Cells[0].Value);
                List<PatternViewModel> list =
                Task.Run(() => ApiClient.GetRequestData<List<PatternViewModel>>("api/Patterns/GetList/" + Id)).Result;
                if (list != null)
                {
                    dataGridViewAvailablePatterns.DataSource = list;
                    dataGridViewAvailablePatterns.Columns[0].Visible = false;
                    dataGridViewAvailablePatterns.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void dataGridViewPassedTests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPassedTests.SelectedRows.Count == 1)
            {
                int Id = Convert.ToInt32(dataGridViewPassedTests.SelectedRows[0].Cells[0].Value);
                List<TestViewModel> list =
                Task.Run(() => ApiClient.GetRequestData<List<TestViewModel>>("api/PassedTests/GetList/" + Id)).Result;
                if (list != null)
                {
                    dataGridViewPassedTests.DataSource = list;
                    dataGridViewPassedTests.Columns[0].Visible = false;
                    dataGridViewPassedTests.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }
        
    }
}
