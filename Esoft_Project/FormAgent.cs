using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormAgent : Form
    {
        public FormAgent()
        {
            InitializeComponent();
            ShowAgent();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    AgentSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentSet;
                    Program.wftDb.AgentSet.Remove(agentSet);
                    Program.wftDb.SaveChanges();
                    ShowAgent();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDearShare.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, это запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    AgentSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentSet;
                    agentSet.FirstName = textBoxFirstName.Text;
                    agentSet.MiddleName = textBoxMiddleName.Text;
                    agentSet.LastName = textBoxLastName.Text;
                    agentSet.DealShare = Convert.ToInt32(textBoxDearShare.Text);
                    Program.wftDb.SaveChanges();
                    ShowAgent();
                }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
                AgentSet agentSet = new AgentSet();
                agentSet.FirstName = textBoxFirstName.Text;
                agentSet.MiddleName = textBoxMiddleName.Text;
                agentSet.LastName = textBoxLastName.Text;
                agentSet.DealShare = Convert.ToInt32(textBoxDearShare.Text);
                Program.wftDb.AgentSet.Add(agentSet);
                Program.wftDb.SaveChanges();
                ShowAgent();
        }

        void ShowAgent()
        {
            // Очищаем listView
            listViewAgent.Items.Clear();
            // Проходим по коллекции клиентов, которые есть в БД с помощью foreach
            foreach (AgentSet agentSet in Program.wftDb.AgentSet)
            {
                // Создаём новый элемент в listView
                // Создаем новый массив строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    agentSet.Id.ToString(), agentSet.FirstName, agentSet.MiddleName, agentSet.LastName, Convert.ToString(agentSet.DealShare)
                });
                //Указываем по какому тэгу будем брать элементы
                item.Tag = agentSet;
                // Добавляем элементы в listView для отображения их
                listViewAgent.Items.Add(item);
            }
            listViewAgent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            // Выравниваем колонки
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void listViewAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAgent.SelectedItems.Count == 1)
            {
                // Ищем элемент из таблицы по тэгу
                AgentSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentSet;
                // Указываем, что может быть изменено
                textBoxFirstName.Text = agentSet.FirstName;
                textBoxMiddleName.Text = agentSet.MiddleName;
                textBoxLastName.Text = agentSet.LastName;
                textBoxDearShare.Text = Convert.ToString(agentSet.DealShare);
            }
            else
            {
                // Если не выбран ни один элемент, то поля будут пустные
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDearShare.Text = "";
            }
        }

        private void labelFirstName_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDearShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }
    }
}
