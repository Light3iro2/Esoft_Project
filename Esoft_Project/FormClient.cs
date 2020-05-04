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
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            ShowClient();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Создаём новый экземпляр класса Клиент
            ClientSet clientsSet = new ClientSet();
            //делаем ссылку на объект, который хранится в textBox'ах
            clientsSet.FirstName = textBoxFirstName.Text;
            clientsSet.MiddleName = textBoxMiddleName.Text;
            clientsSet.LastName = textBoxLastName.Text;
            clientsSet.Phone = textBoxPhone.Text;
            clientsSet.Email = textBoxEmail.Text;
            // Добавляем в таблицу ClientSet нового клиента clientSet
            Program.wftDb.ClientSet.Add(clientsSet);
            // Сохраняем изменения в модели wftDb
            Program.wftDb.SaveChanges();
            ShowClient();
        }
        void ShowClient()
        {
            // Очищаем listView
            listViewClient.Items.Clear();
            // Проходим по коллекции клиентов, которые есть в БД с помощью foreach
            foreach (ClientSet clientsSet in Program.wftDb.ClientSet)
            {
                // Создаём новый элемент в listView
                // Создаем новый массив строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    clientsSet.Id.ToString(), clientsSet.FirstName, clientsSet.MiddleName, clientsSet.LastName, clientsSet.Phone, clientsSet.Email
                });
                //Указываем по какому тэгу будем брать элементы
                item.Tag = clientsSet;
                // Добавляем элементы в listView для отображения их
                listViewClient.Items.Add(item);
            }
            listViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            // Выравниваем колонки
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewClient.SelectedItems.Count == 1)
            {
                // Ищем элемент из таблицы по тэгу
                ClientSet clientSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                // Указываем, что может быть изменено
                clientSet.FirstName = textBoxFirstName.Text;
                clientSet.MiddleName = textBoxMiddleName.Text;
                clientSet.LastName = textBoxLastName.Text;
                clientSet.Phone = textBoxPhone.Text;
                clientSet.Email = textBoxEmail.Text;
                // Сохраняем изменения в модели
                Program.wftDb.SaveChanges();
                // Отоброжаем в listView
                ShowClient();
            }
        }

        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClient.SelectedItems.Count == 1)
            {
                // Ищем элемент из таблицы по тэгу
                ClientSet clientSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                // Указываем, что может быть изменено
                textBoxFirstName.Text = clientSet.FirstName;
                textBoxMiddleName.Text = clientSet.MiddleName;
                textBoxLastName.Text = clientSet.LastName;
                textBoxPhone.Text = clientSet.Phone;
                textBoxEmail.Text = clientSet.Email;
            }
            else
            {
                // Если не выбран ни один элемент, то поля будут пустные
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewClient.SelectedItems.Count == 1)
                {
                    ClientSet clientSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                    Program.wftDb.ClientSet.Remove(clientSet);
                    Program.wftDb.SaveChanges();
                    ShowClient();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, это запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBoxMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBoxLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelFirstName_Click(object sender, EventArgs e)
        {

        }
    }
}
