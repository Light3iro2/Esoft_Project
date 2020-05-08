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
    public partial class FormSupply : Form
    {
        public FormSupply()
        {
            InitializeComponent();
            ShowAgents();
            ShowClients();
            ShowRealEstate();
            ShowSupplySet();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && comboBoxRealEstate.SelectedItem != null && textBoxPrice.Text != "")
            {
                // Создаем новый экземпляр класса предложений
                SupplySet supply = new SupplySet();
                // Из выбраной строки в comboBoxAgents отделяем ID риелтора и делаем ссылку supply.IdAgent
                supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                // Так же отделяем и заносим ID клиента
                supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                // Так же отделяем и заносим ID объекта недвижимости
                supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                // Цена объекта недвижимости чаще всего превосходит миллион, поэтому используем Int64
                supply.Price = Convert.ToInt64(textBoxPrice.Text);
                // Добавляем в таблицу SupplySet новый объект недвижимости supply
                Program.wftDb.SupplySet.Add(supply);
                // Сохраняем изменения
                Program.wftDb.SaveChanges();
                ShowSupplySet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ShowAgents()
        {
            comboBoxAgents.Items.Clear();
            foreach (AgentSet agentSet in Program.wftDb.AgentSet)
            {
                // Добавляем информацию, которую ходим видить в строке ComboBox'a
                // Можно настроить по своему усмотрению
                // Главное не убирать ID!!!
                string[] item = { agentSet.Id.ToString() + ".", agentSet.FirstName, agentSet.MiddleName, agentSet.LastName };
                comboBoxAgents.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            comboBoxClients.Items.Clear();
            foreach (ClientSet clientSet in Program.wftDb.ClientSet)
            {
                // Добавляем информацию, которую ходим видить в строке ComboBox'a
                // Можно настроить по своему усмотрению
                // Главное не убирать ID!!!
                string[] item = { clientSet.Id.ToString() + ".", clientSet.FirstName, clientSet.MiddleName, clientSet.LastName };
                comboBoxClients.Items.Add(string.Join(" ", item));
            }
        }
        void ShowRealEstate()
        {
            comboBoxRealEstate.Items.Clear();
            foreach (RealEstateSet realEstateSet in Program.wftDb.RealEstateSet)
            {
                // Добавляем информацию, которую ходим видить в строке ComboBox'a
                // Можно настроить по своему усмотрению
                // Главное не убирать ID!!!
                string[] item = { realEstateSet.Id.ToString() + ".", realEstateSet.Address_City + ",", realEstateSet.Address_Street + ",",
                    "дом. " + realEstateSet.Address_House + ",", "кв. " + realEstateSet.Address_Number };
                comboBoxRealEstate.Items.Add(string.Join(" ", item));
            }
        }
        void ShowSupplySet()
        {
            listViewSupplySet.Items.Clear(); // Инзначально очищаем все listView
            foreach (SupplySet supply in Program.wftDb.SupplySet) // Проходим по коллекции клиентов, которые в таблице
            {
                // Создадим новый элемент в listView с помощью массива строк
                ListViewItem item = new ListViewItem(new string[]
                    {
                    supply.IdAgent.ToString(),
                    supply.AgentSet.LastName + " " + supply.AgentSet.FirstName + " " + supply.AgentSet.MiddleName,
                    
                    supply.IdClient.ToString(),
                    supply.ClientSet.LastName + " " + supply.ClientSet.FirstName + " " + supply.ClientSet.MiddleName,

                    supply.IdRealEstate.ToString(),
                    "город: " + supply.RealEstateSet.Address_City + ", улица: " + supply.RealEstateSet.Address_Street + ", дом: " + supply.RealEstateSet.Address_House
                    + ", кв. " + supply.RealEstateSet.Address_Number,

                    supply.Price.ToString()
                    });
                item.Tag = supply;
                listViewSupplySet.Items.Add(item);
            }
            listViewSupplySet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1) // Если в listView выбран элемент, то...
            {
                // Ищем элемент из таблицы по тэгу
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                // Указываем что можно изменить
                supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                supply.Price = Convert.ToInt64(textBoxPrice.Text);
                // Сохраняем изменения в модели wftDb
                Program.wftDb.SaveChanges();
                ShowSupplySet();
            }
        }

        private void listViewSupplySet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
            {
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                // Указываем что может быть изменено
                // Находим в comboBoxAgents необходимую строку по ID риелтора из supply.IdAgent и задаем ей отображение в comboBox'у
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(supply.IdAgent.ToString());
                // Так же делаем с comboBoxClients и comboBoxRealEstate
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(supply.IdClient.ToString());
                comboBoxRealEstate.SelectedIndex = comboBoxRealEstate.FindString(supply.IdRealEstate.ToString());
                textBoxPrice.Text = supply.Price.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstate.SelectedItem = null;
                textBoxPrice.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e) // Удаление из таблицы
        {
            try
            {
                if (listViewSupplySet.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
                {
                    // Ищем элемент из таблицы по тэгу
                    SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                    // Удаление выбранной строки из модели
                    Program.wftDb.SupplySet.Remove(supply);
                    // Сохранение изменений
                    Program.wftDb.SaveChanges();
                    // Отображение обновлённого списка
                    ShowSupplySet();
                }
                // Очищаем все поля
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstate = null;
                textBoxPrice.Text = "";
            }
            catch // Если возникла ошибка, то выводим окно
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
