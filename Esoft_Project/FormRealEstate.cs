using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Esoft_Project
{
    public partial class FormRealEstate : Form
    {
        public FormRealEstate()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            ShowRealEstateSet();
        }

        private void labelTotalArea_Click(object sender, EventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0)
            {
                // Делаем видимыми нужные элементы
                listViewRealEstateSet_Apartment.Visible = true;
                labelFloor.Visible = true;
                textBoxFloor.Visible = true;
                labelRooms.Visible = true;
                textBoxRooms.Visible = true;

                // Скрываем ненужные элементы
                listViewRealEstateSet_Land.Visible = false;
                listViewRealEstateSet_House.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;

                //Очищаем все видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                // Делаем видимыми нужные элементы
                listViewRealEstateSet_Land.Visible = true;

                // Скрываем ненужные элементы
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;

                //Очищаем все видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
            }
            else if (comboBoxType.SelectedIndex == 2)
            {
                // Делаем видимыми нужные элементы
                listViewRealEstateSet_House.Visible = true;
                labelTotalFloors.Visible = true;
                textBoxTotalFloors.Visible = true;

                // Скрываем ненужные элементы
                listViewRealEstateSet_Land.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;

                //Очищаем все видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            RealEstateSet realEstateSet = new RealEstateSet();
            // Делаем ссылку на объект, который хранится в textBox'ах
            realEstateSet.Address_City = textBoxAddress_City.Text;
            realEstateSet.Address_House = textBoxAddress_House.Text;
            realEstateSet.Address_Street = textBoxAddress_Street.Text;
            realEstateSet.Address_Number = textBoxAddress_Number.Text;
            realEstateSet.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
            realEstateSet.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
            realEstateSet.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
            // Дополнительные поля для типа "Квартира"
            if (comboBoxType.SelectedIndex == 0)
            {
                realEstateSet.Type = 0;
                realEstateSet.Rooms = Convert.ToInt32(textBoxRooms.Text);
                realEstateSet.Floor = Convert.ToInt32(textBoxFloor.Text);
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                realEstateSet.Type = 1;
            }
            else
            {
                realEstateSet.Type = 2;
                realEstateSet.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text);
            }
            Program.wftDb.RealEstateSet.Add(realEstateSet);
            Program.wftDb.SaveChanges();
            ShowRealEstateSet();
        }
        void ShowRealEstateSet()
        {
            // Очищаем все listView
            listViewRealEstateSet_Apartment.Items.Clear();
            listViewRealEstateSet_Land.Items.Clear();
            listViewRealEstateSet_House.Items.Clear();
            //Проходим по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (RealEstateSet realEstate in Program.wftDb.RealEstateSet)
            {
                
                if (realEstate.Type == 0) // Отображение квартир в ListViewRealEstateSet_Apartment
                {
                    // Создадим новый элемент в listViewRealEstateSet_Apartment с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        // Указываем необходимые поля
                        realEstate.Address_City, realEstate.Address_Street, realEstate.Address_House,
                        realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(),
                        realEstate.Coordinate_longitude.ToString(), realEstate.TotalArea.ToString(),
                        realEstate.Rooms.ToString(), realEstate.Floor.ToString()
                    });
                    // Указываем по какому тэгу выбраны элементы
                    item.Tag = realEstate;
                    // Добавляем элементы в listViewRealEstateSet_Apartment
                    listViewRealEstateSet_Apartment.Items.Add(item);
                }
                else if (realEstate.Type == 1) // Отображение домов в ListViewRealEstate_Land
                {
                    // Создадим новый элемент в listViewRealEstateSet_Land с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        // Указываем необходимые поля
                        realEstate.Address_City, realEstate.Address_Street, realEstate.Address_House,
                        realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(),
                        realEstate.Coordinate_longitude.ToString(), realEstate.TotalArea.ToString(),
                    });
                    // Указываем по какому тэгу выбраны элементы
                    item.Tag = realEstate;
                    // Добавляем элементы в listViewRealEstateSet_House
                    listViewRealEstateSet_Land.Items.Add(item);
                }
                else // Отображение домов в ListViewRealEstateSet_House
                {
                    //Создадим новый элемент в listViewRealEstateSet_House с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        // Указываем необходимые поля
                        realEstate.Address_City, realEstate.Address_Street, realEstate.Address_House,
                        realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(),
                        realEstate.Coordinate_longitude.ToString(), realEstate.TotalArea.ToString(), realEstate.TotalFloors.ToString()
                    });
                    // Указываем по какому тэгу выбраны элементы
                    item.Tag = realEstate;
                    // Добавляем элементы в listViewRealEstateSet_Land 
                    listViewRealEstateSet_House.Items.Add(item);
                }
            }
            // Выравниваем столбцы во всех listView
            listViewRealEstateSet_Apartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0) // Выбран тип "Квартира", работа с listViewRealEstateSet_Apartment
            {
                if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1) // Если в listView выбран элемент
                {
                    // Ищем элемент из таблицы по тэгу
                    RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                    // Указываем, что может быть изменено
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text);
                    realEstate.Floor = Convert.ToInt32(textBoxFloor.Text);
                    // Сохраняем изменения в модели wftDb
                    Program.wftDb.SaveChanges();
                    // Отображаем в listViewRealEstateSet_Apartment
                    ShowRealEstateSet();
                }
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                // Есои в listView выбран элемент, то...
                if (listViewRealEstateSet_Land.SelectedItems.Count == 1) // Если в listView выбран элемент
                {
                    // Ищем элемент из таблицы по тэгу
                    RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                    // Указываем, что может быть изменено
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    // Сохраняем изменения в модели wftDb
                    Program.wftDb.SaveChanges();
                    // Отображаем в listViewRealEstateSet_House
                    ShowRealEstateSet();
                }
            }
            else // Выбран тип "Земля", работа с listViewRealEstateSet_Land
            {
                // Если в listView выбран элемент, то..
                if (listViewRealEstateSet_House.SelectedItems.Count == 1) // Если в listView выбран элемент
                {
                    // Ищем элемент из таблицы по тэгу
                    RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                    // Указываем, что может быть изменено
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text);
                    // Сохраняем изменения в модели wftDb
                    Program.wftDb.SaveChanges();
                    // Отображаем в listViewRealEstateSet_Land
                    ShowRealEstateSet();
                }
            }
        }

        private void listViewRealEstateSet_House_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Если выбран один элемент, то..
            if (listViewRealEstateSet_House.SelectedItems.Count == 1)
            {
                // Ищем элемент из таблицы по тэгу
                RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                //Указываем, что можем изменить
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxTotalFloors.Text = realEstate.TotalArea.ToString();
            }
            else
            {
                // Если не выбран ни один элемент, то будут пустые строки
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try // Пробуем совершить действие
            {
                //Выбран тип "Квартира", работа с listViewRealEstateSet_Apartment
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1) // Если в listView выбран элемент
                    {
                        // Ищем этот элемент базы по тэгу
                        RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                        // Удаляем из модели базы данных
                        Program.wftDb.RealEstateSet.Remove(realEstate);
                        // Сохраняем изменения
                        Program.wftDb.SaveChanges();
                        // Отображаем обновлённый список
                        ShowRealEstateSet();
                    }
                    // Очищаем текстовые поля
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxRooms.Text = "";
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1) // Если в listView выбран элемент
                    {
                        // Ищем этот элемент базы по тэгу
                        RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                        // Удаляем из модели базы данных
                        Program.wftDb.RealEstateSet.Remove(realEstate);
                        // Сохраняем изменения
                        Program.wftDb.SaveChanges();
                        // Отображаем обновлённый список
                        ShowRealEstateSet();
                    }
                    // Очищаем текстовые поля
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                }
                else
                {
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1) // Если в listView выбран элемент
                    {
                        // Ищем этот элемент базы по тэгу
                        RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                        // Удаляем из модели базы данных
                        Program.wftDb.RealEstateSet.Remove(realEstate);
                        // Сохраняем изменения
                        Program.wftDb.SaveChanges();
                        // Отображаем обновлённый список
                        ShowRealEstateSet();
                    }
                    // Очищаем текстовые поля
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxTotalFloors.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxCoordinate_longitude_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAddress_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void listViewRealEstateSet_Apartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
            {
                RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                //Указываем, что можем изменить
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxRooms.Text = realEstate.Rooms.ToString();
                textBoxFloor.Text = realEstate.Floor.ToString();
            }
            else
            {
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";
            }
        }

        private void textBoxCoordinate_latitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number !=45)
            {
                e.Handled = true;
            }
        }

        private void textBoxCoordinate_longitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void textBoxTotalArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void textBoxTotalFloors_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void textBoxRooms_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void textBoxFloor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void labelAddress_Street_Click(object sender, EventArgs e)
        {

        }

        private void textBoxAddress_Number_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelAddress_Number_Click(object sender, EventArgs e)
        {

        }

        private void labelTotalFloors_Click(object sender, EventArgs e)
        {

        }

        private void FormRealEstate_Load(object sender, EventArgs e)
        {

        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged_1(object sender, EventArgs e)
        {
                if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                {
                    RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                    //Указываем, что можем изменить
                    textBoxAddress_City.Text = realEstate.Address_City;
                    textBoxAddress_Street.Text = realEstate.Address_Street;
                    textBoxAddress_House.Text = realEstate.Address_House;
                    textBoxAddress_Number.Text = realEstate.Address_Number;
                    textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                    textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                    textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                    textBoxRooms.Text = realEstate.Rooms.ToString();
                    textBoxFloor.Text = realEstate.Floor.ToString();
                }
                else
                {
                    textBoxAddress_City.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxRooms.Text = "";
                    textBoxFloor.Text = "";
                }
        }

        private void listViewRealEstateSet_House_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_House.SelectedItems.Count == 1)
            {
                RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                //Указываем, что можем изменить
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxTotalFloors.Text = realEstate.Floor.ToString();
            }
            else
            {
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
            {
                RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                //Указываем, что можем изменить
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
            }
            else
            {
                textBoxAddress_City.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
            }
        }
    }
}
