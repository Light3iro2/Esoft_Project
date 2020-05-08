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
    public partial class FormDemand : Form
    {
        public FormDemand()
        {
            InitializeComponent();
            ShowAgent();
            ShowDemand();
            ShowClients();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgent.SelectedItem != null && comboBoxClient.SelectedItem != null && comboBoxType.SelectedItem != null)
            {
                // Создаем новый экземпляр класса предложений
                DemandSet demandSet = new DemandSet();
                // Из выбраной строки в comboBoxAgents отделяем ID риелтора и делаем ссылку supply.IdAgent
                demandSet.IdAgent = Convert.ToInt32(comboBoxAgent.SelectedItem.ToString().Split('.')[0]);
                // Так же отделяем и заносим ID клиента
                demandSet.IdClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                // Так же отделяем и заносим ID объекта недвижимости
                demandSet.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                demandSet.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                // Цена объекта недвижимости чаще всего превосходит миллион, поэтому используем Int64
                demandSet.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                demandSet.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                if (comboBoxType.SelectedIndex == 0)
                {
                    demandSet.Type = 0;
                    demandSet.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demandSet.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demandSet.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demandSet.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    demandSet.Type = 1;
                }
                else
                {
                    demandSet.Type = 2;
                    demandSet.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demandSet.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                }
                // Добавляем в таблицу SupplySet новый объект недвижимости supply
                Program.wftDb.DemandSet.Add(demandSet);
                // Сохраняем изменения
                Program.wftDb.SaveChanges();
                ShowDemand();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBoxAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        void ShowAgent()
        {
            comboBoxAgent.Items.Clear();
            foreach (AgentSet agentSet in Program.wftDb.AgentSet)
            {
                string[] item = { agentSet.Id.ToString() + ".", agentSet.FirstName, agentSet.MiddleName, agentSet.LastName };
                comboBoxAgent.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            comboBoxClient.Items.Clear();
            foreach (ClientSet clientSet in Program.wftDb.ClientSet)
            {
                string[] item = { clientSet.Id.ToString() + ".", clientSet.FirstName, clientSet.MiddleName, clientSet.LastName };
                comboBoxClient.Items.Add(string.Join(" ", item));
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0)
            {
                // Делаем видимыми нужные элементы
                listViewDemandType0.Visible = true;
                labelMinFloor.Visible = true;
                textBoxMinFloor.Visible = true;
                labelMinRooms.Visible = true;
                textBoxMinRooms.Visible = true;
                labelMaxFloor.Visible = true;
                textBoxMaxFloor.Visible = true;
                labelMaxRooms.Visible = true;
                textBoxMaxRooms.Visible = true;

                // Скрываем ненужные элементы
                listViewDemandType1.Visible = false;
                listViewDemandType2.Visible = false;
                labelMinFloors.Visible = false;
                textBoxMinFloors.Visible = false;
                labelMaxFloors.Visible = false;
                textBoxMaxFloors.Visible = false;

                //Очищаем все видимые элементы
                textBoxMinArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMaxFloor.Text = "";
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                // Делаем видимыми нужные элементы
                listViewDemandType1.Visible = true;

                // Скрываем ненужные элементы
                listViewDemandType0.Visible = false;
                listViewDemandType2.Visible = false;
                labelMinFloor.Visible = false;
                textBoxMinFloor.Visible = false;
                labelMinRooms.Visible = false;
                textBoxMinRooms.Visible = false;
                labelMinFloors.Visible = false;
                textBoxMinFloors.Visible = false;
                labelMaxFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                labelMaxRooms.Visible = false;
                textBoxMaxRooms.Visible = false;
                labelMaxFloors.Visible = false;
                textBoxMaxFloors.Visible = false;

                //Очищаем все видимые элементы
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
            }
            else if (comboBoxType.SelectedIndex == 2)
            {
                // Делаем видимыми нужные элементы
                listViewDemandType2.Visible = true;
                labelMinFloors.Visible = true;
                textBoxMinFloors.Visible = true;
                labelMaxFloors.Visible = true;
                textBoxMaxFloors.Visible = true;

                // Скрываем ненужные элементы
                listViewDemandType0.Visible = false;
                listViewDemandType1.Visible = false;
                labelMinFloor.Visible = false;
                textBoxMinFloor.Visible = false;
                labelMinRooms.Visible = false;
                textBoxMinRooms.Visible = false;
                labelMaxFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                labelMaxRooms.Visible = false;
                textBoxMaxRooms.Visible = false;

                //Очищаем все видимые элементы
                textBoxMinArea.Text = "";
                textBoxMinFloors.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMaxFloors.Text = "";
            }
        }
        void ShowDemand()
        {
            // Очищаем все listView
            listViewDemandType0.Items.Clear();
            listViewDemandType1.Items.Clear();
            listViewDemandType2.Items.Clear();
            //Проходим по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (DemandSet demandSet in Program.wftDb.DemandSet)
            {

                if (demandSet.Type == 0) // Отображение квартир в ListViewRealEstateSet_Apartment
                {
                    // Создадим новый элемент в listViewRealEstateSet_Apartment с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        // Указываем необходимые поля
                        demandSet.Id.ToString(),
                        demandSet.AgentSet.LastName + " " + demandSet.AgentSet.FirstName + " " + demandSet.AgentSet.MiddleName,
                        demandSet.ClientSet.LastName + " " + demandSet.ClientSet.FirstName + " " + demandSet.ClientSet.MiddleName,
                        "Квартира", demandSet.MinPrice.ToString(), demandSet.MaxPrice.ToString(), demandSet.MinArea.ToString(), demandSet.MaxArea.ToString(),
                        demandSet.MinRooms.ToString(), demandSet.MaxRooms.ToString(), demandSet.MinFloor.ToString(), demandSet.MaxFloor.ToString(),
                    });
                    // Указываем по какому тэгу выбраны элементы
                    item.Tag = demandSet;
                    // Добавляем элементы в listViewRealEstateSet_Apartment
                    listViewDemandType0.Items.Add(item);
                }
                else if (demandSet.Type == 1)
                {
                    // Создадим новый элемент в listViewRealEstateSet_Land с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demandSet.Id.ToString(),
                        demandSet.AgentSet.LastName + " " + demandSet.AgentSet.FirstName + " " + demandSet.AgentSet.MiddleName,
                        demandSet.ClientSet.LastName + " " + demandSet.ClientSet.FirstName + " " + demandSet.ClientSet.MiddleName,
                        "Земля", demandSet.MinPrice.ToString(), demandSet.MaxPrice.ToString(), demandSet.MinArea.ToString(), demandSet.MaxArea.ToString(),
                    });
                    item.Tag = demandSet;
                    listViewDemandType1.Items.Add(item);
                }
                else
                {
                    // Создадим новый элемент в listViewRealEstateSet_Land с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demandSet.Id.ToString(),
                        demandSet.AgentSet.LastName + " " + demandSet.AgentSet.FirstName + " " + demandSet.AgentSet.MiddleName,
                        demandSet.ClientSet.LastName + " " + demandSet.ClientSet.FirstName + " " + demandSet.ClientSet.MiddleName,
                        "Дом", demandSet.MinPrice.ToString(), demandSet.MaxPrice.ToString(), demandSet.MinArea.ToString(), demandSet.MaxArea.ToString(),
                        demandSet.MinFloors.ToString(), demandSet.MaxFloors.ToString(),
                    });
                    item.Tag = demandSet;
                    listViewDemandType2.Items.Add(item);
                }
            }
            // Выравниваем столбцы во всех listView
            listViewDemandType0.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewDemandType1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewDemandType2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0) // Выбран тип "Квартира", работа с listViewRealEstateSet_Apartment
            {
                if (listViewDemandType0.SelectedItems.Count == 1) // Если в listView выбран элемент, то...
                {
                    // Ищем элемент из таблицы по тэгу
                    DemandSet demand = listViewDemandType0.SelectedItems[0].Tag as DemandSet;
                    // Указываем что можно изменить
                    demand.IdAgent = Convert.ToInt32(comboBoxAgent.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                    demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                    demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                    // Сохраняем изменения в модели wftDb
                    Program.wftDb.SaveChanges();
                    ShowDemand();
                }
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                // Есои в listView выбран элемент, то...
                if (listViewDemandType1.SelectedItems.Count == 1) // Если в listView выбран элемент
                {
                    // Ищем элемент из таблицы по тэгу
                    DemandSet demand = listViewDemandType1.SelectedItems[0].Tag as DemandSet;
                    // Указываем, что может быть изменено
                    demand.IdAgent = Convert.ToInt32(comboBoxAgent.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                    demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                    demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                    // Сохраняем изменения в модели wftDb
                    Program.wftDb.SaveChanges();
                    // Отображаем в listViewRealEstateSet_House
                    ShowDemand();
                }
            }
            else // Выбран тип "Земля", работа с listViewRealEstateSet_Land
            {
                // Если в listView выбран элемент, то..
                if (listViewDemandType2.SelectedItems.Count == 1) // Если в listView выбран элемент
                {
                    // Ищем элемент из таблицы по тэгу
                    DemandSet demand = listViewDemandType2.SelectedItems[0].Tag as DemandSet;
                    // Указываем, что может быть изменено
                    demand.IdAgent = Convert.ToInt32(comboBoxAgent.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                    demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                    demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                    demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                    // Сохраняем изменения в модели wftDb
                    Program.wftDb.SaveChanges();
                    // Отображаем в listViewRealEstateSet_Land
                    ShowDemand();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewDemandType0.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
                    {
                        // Ищем элемент из таблицы по тэгу
                        DemandSet demand = listViewDemandType0.SelectedItems[0].Tag as DemandSet;
                        // Удаление выбранной строки из модели
                        Program.wftDb.DemandSet.Remove(demand);
                        // Сохранение изменений
                        Program.wftDb.SaveChanges();
                        // Отображение обновлённого списка
                    }
                    // Очищаем все поля
                    comboBoxAgent.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxRooms.Text = "";
                    textBoxMinFloor.Text = "";
                    textBoxMaxFloor.Text = "";
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewDemandType1.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
                    {
                        // Ищем элемент из таблицы по тэгу
                        DemandSet demand = listViewDemandType1.SelectedItems[0].Tag as DemandSet;
                        // Удаление выбранной строки из модели
                        Program.wftDb.DemandSet.Remove(demand);
                        // Сохранение изменений
                        Program.wftDb.SaveChanges();
                        // Отображение обновлённого списка
                    }
                    // Очищаем все поля
                    comboBoxAgent.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                }
                else
                {
                    if (listViewDemandType2.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
                    {
                        // Ищем элемент из таблицы по тэгу
                        DemandSet demand = listViewDemandType2.SelectedItems[0].Tag as DemandSet;
                        // Удаление выбранной строки из модели
                        Program.wftDb.DemandSet.Remove(demand);
                        // Сохранение изменений
                        Program.wftDb.SaveChanges();
                        // Отображение обновлённого списка
                    }
                    // Очищаем все поля
                    comboBoxAgent.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinFloors.Text = "";
                    textBoxMaxFloors.Text = "";
                }
                ShowDemand();
            }
            catch // Если возникла ошибка, то выводим окно
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ShowDemand();
        }

        private void listViewDemandType0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemandType0.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
            {
                DemandSet demand = listViewDemandType0.SelectedItems[0].Tag as DemandSet;
                // Указываем что может быть изменено
                // Находим в comboBoxAgents необходимую строку по ID риелтора из supply.IdAgent и задаем ей отображение в comboBox'у
                comboBoxAgent.SelectedIndex = comboBoxAgent.FindString(demand.IdAgent.ToString());
                // Так же делаем с comboBoxClients и comboBoxRealEstate
                comboBoxClient.SelectedIndex = comboBoxClient.FindString(demand.IdClient.ToString());
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinRooms.Text = demand.MinRooms.ToString();
                textBoxMaxRooms.Text = demand.MaxRooms.ToString();
                textBoxMinFloor.Text = demand.MinFloor.ToString();
                textBoxMaxFloor.Text = demand.MaxFloor.ToString();
            }
            else
            {
                comboBoxAgent.SelectedItem = null;
                comboBoxClient.SelectedItem = null;
                comboBoxType.SelectedItem = null;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxFloor.Text = "";
            }
        }

        private void listViewDemandType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemandType1.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
            {
                DemandSet demand = listViewDemandType1.SelectedItems[0].Tag as DemandSet;
                // Указываем что может быть изменено
                // Находим в comboBoxAgents необходимую строку по ID риелтора из supply.IdAgent и задаем ей отображение в comboBox'у
                comboBoxAgent.SelectedIndex = comboBoxAgent.FindString(demand.IdAgent.ToString());
                // Так же делаем с comboBoxClients и comboBoxRealEstate
                comboBoxClient.SelectedIndex = comboBoxClient.FindString(demand.IdClient.ToString());
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
            }
            else
            {
                comboBoxAgent.SelectedItem = null;
                comboBoxClient.SelectedItem = null;
                comboBoxType.SelectedItem = null;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
            }
        }

        private void listViewDemandType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemandType2.SelectedItems.Count == 1) // Если в listView выбран элемент, то..
            {
                DemandSet demand = listViewDemandType2.SelectedItems[0].Tag as DemandSet;
                // Указываем что может быть изменено
                // Находим в comboBoxAgents необходимую строку по ID риелтора из supply.IdAgent и задаем ей отображение в comboBox'у
                comboBoxAgent.SelectedIndex = comboBoxAgent.FindString(demand.IdAgent.ToString());
                // Так же делаем с comboBoxClients и comboBoxRealEstate
                comboBoxClient.SelectedIndex = comboBoxClient.FindString(demand.IdClient.ToString());
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinFloors.Text = demand.MinFloors.ToString();
                textBoxMaxFloors.Text = demand.MaxFloors.ToString();
            }
            else
            {
                comboBoxAgent.SelectedItem = null;
                comboBoxClient.SelectedItem = null;
                comboBoxType.SelectedItem = null;
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinFloors.Text = "";
                textBoxMaxFloors.Text = "";
            }
        }

        private void FormDemand_Load(object sender, EventArgs e)
        {

        }
    }
}
