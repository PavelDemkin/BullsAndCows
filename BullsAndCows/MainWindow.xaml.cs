using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BullsAndCows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // объявление переменной для  генерации случайных чисел
        private Random rand = new Random();
        private int[] x = new int[4];
        // в строковой переменной впоследствии будем хранить строковое представление загаданного числа
        private string num1, num2, numCPU;
        // счетчики полного и частичного совпадения цифр в загаданном и введенном числах
        private int polnoeSovpadenie;
        private int chastichnoeSovpadenie;

        private bool player1 = false, player2 = false, cpu = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        // начало новой игры
        private void butnNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void butnZagadat_Click(object sender, RoutedEventArgs e)
        {
            if (player1 == false)
            {
                if (tbNumPlayer1.Text.Length != 4 || tbNumPlayer1.Text[0] == '0')
                {
                    MessageBox.Show("Введенные числа должны быть четырехзначными и не начинаться нулем");
                    return;
                }
                num1 = tbNumPlayer1.Text;
                player1 = true;
                tbNumPlayer1.Text = "";
                tbOut.Text += "OK\n";

                if (rbutnCPU.IsChecked == false)
                {
                    lbPlayer1.IsEnabled = false;
                    lbPlayer2.IsEnabled = true;
                    tbNumPlayer1.IsEnabled = false;
                    tbNumPlayer2.IsEnabled = true;

                    tbOut.Text += "Игрок 2 загадывает число... ";
                }
                else
                {
                    tbOut.Text += "Компьютер загадывает число... ";

                    num2 = NovoeChislo();

                    tbOut.Text += "OK\nИгра началась!\nХод Игрока 1... ";

                    butnZagadat.IsEnabled = false;
                    butnCheck.IsEnabled = true;
                    butnShow.IsEnabled = true;
                }
            }
            else if (player2 == false)
            {
                if (tbNumPlayer2.Text.Length != 4 || tbNumPlayer2.Text[0] == '0')
                {
                    MessageBox.Show("Введенные числа должны быть четырехзначными и не начинаться нулем");
                    return;
                }
                num2 = tbNumPlayer2.Text;

                player2 = false;
                player1 = true;

                tbNumPlayer2.Text = "";
                tbOut.Text += "OK\nИгра началась!\nХод Игрока 1... ";

                lbPlayer1.IsEnabled = true;
                lbPlayer2.IsEnabled = false;
                tbNumPlayer1.IsEnabled = true;
                tbNumPlayer2.IsEnabled = false;

                butnZagadat.IsEnabled = false;
                butnCheck.IsEnabled = true;

                butnShow.IsEnabled = true;
            }
        }

        // проверка введенного числа
        private void butnCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckNumber();
        }

        // вывод загаданного числа на экран
        private void butnShow_Click(object sender, RoutedEventArgs e)
        {
            // в label передаем загаданное число
            //lbChislo.Content = s;
            // обнуляем метку вывода результатов сравнения введенного и загаданного чисел
            if (rbutn2Players.IsChecked == true)
            {
                tbOut.Text += "\nЗагаданные числа:\tИгрок 1: " + num1 + "\tИгрок 2: " + num2;
            }
            else
            {
                tbOut.Text += "\nЗагаданные числа:\tИгрок 1: " + num1 + "\tКомпьютер: " + num2;
            }

            lbPlayer1.IsEnabled = false;
            lbPlayer2.IsEnabled = false;
            tbNumPlayer1.IsEnabled = false;
            tbNumPlayer2.IsEnabled = false;

            butnCheck.IsEnabled = false;
            butnShow.IsEnabled = false;

            // запрещаем ввод символов в текстбокс
            //tbNumber.IsReadOnly = true;
        }

        // метод новой игры
        private void NewGame()
        {
            lbPlayer1.IsEnabled = true;
            lbPlayer2.IsEnabled = false;
            tbNumPlayer1.IsEnabled = true;
            tbNumPlayer2.IsEnabled = false;

            player1 = false;
            player2 = false;
            cpu = false;

            butnZagadat.IsEnabled = true;
            butnCheck.IsEnabled = false;

            tbOut.Text = "Игрок 1 загадывает число... ";
        }

        // метод генерации нового числа
        private string NovoeChislo()
        {
            // флаг сравнения с предыдущими цифрами. совпадает - true 
            bool contains;
            // цикл заполнения массива нового числа новыми цифрами
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    contains = false;
                    // генерация новой цифры
                    if (i == 0)
                        x[i] = rand.Next(1, 10);
                    else
                        x[i] = rand.Next(10);
                    // цикл сравнения сгенерированной цифры с предыдущими
                    for (int k = 0; k < i; k++)
                        if (x[k] == x[i])
                            //если сгенериррованная цифра совпала с одной из предыдущих
                            // флаг сравнения делаем true для продолжения генерации
                            //несовпадающей цифры в элемент массива
                            contains = true;
                } while (contains);
            }
            return x[0].ToString() + x[1] + x[2] + x[3];// из элементов массива формируем строку
        }

        private void CheckNumber()  // КОНТРОЛЕР
        {
            if (player1 == true)
            {
                if (tbNumPlayer1.Text.Length != 4 || tbNumPlayer1.Text[0] == '0')
                {
                    // вывести сообщение об ошибке
                    MessageBox.Show("Введенные числа должны быть четырехзначными и не начинаться нулем");
                    return;
                }

                SravenieChisel();
            }
            else if (player2 == true)
            {
                if (tbNumPlayer2.Text.Length != 4 || tbNumPlayer2.Text[0] == '0')
                {
                    // вывести сообщение об ошибке
                    MessageBox.Show("Введенные числа должны быть четырехзначными и не начинаться нулем");
                    return;
                }

                SravenieChisel();
            }
        }

        private void SravenieChisel() // МОДЕЛЬ
        {
            // обнуление счетчиков
            polnoeSovpadenie = 0;
            chastichnoeSovpadenie = 0;
            // перевод содержимого текстбокса в символьный массив
            char[] ch = null;
            string s = "";

            if (player1)
            {
                ch = tbNumPlayer1.Text.ToCharArray();
                s = num2;
            }
            else if (player2)
            {
                ch = tbNumPlayer2.Text.ToCharArray();
                s = num1;
            }
            else if (cpu)
            {
                numCPU = rand.Next(1000, 10000).ToString();
                ch = numCPU.ToCharArray();
                s = num1;
            }

            // цикл проверки символов в массиве
            for (int i = 0; i < 4; i++)
            {
                // если строка s содержит в себе элемент массива
                if (s.Contains(ch[i]))
                {
                    // если номер символа в массиве совпадает с номером символа в строке
                    if (s[i] == ch[i])
                        // увеличиваем счетчик полного совпадения
                        polnoeSovpadenie++;
                    else
                        // если номер символа в массиве не совпадает с номером символа в строке
                        // увеличиваем счетчик неполного совпадения
                        chastichnoeSovpadenie++;
                }
            }

            RezultShow();
        }


        private void RezultShow()  // ПРЕДСТАВЛЕНИЕ
        {
            if (player1)
            {
                tbOut.Text += tbNumPlayer1.Text + " - быков: " + polnoeSovpadenie + ", коров: " + chastichnoeSovpadenie + "\n";
                if (polnoeSovpadenie == 4)
                {
                    tbOut.Text += "Число отгадано! Игрок 1 победил!";
                    lbPlayer1.IsEnabled = false;
                    tbNumPlayer1.IsEnabled = false;
                    butnCheck.IsEnabled = false;
                    return;
                }

                if (rbutnCPU.IsChecked == true)
                {
                    tbOut.Text += "Ход Компьютера... ";

                    tbNumPlayer1.Text = "";

                    player1 = false;
                    cpu = true;

                    SravenieChisel();
                }
                else
                {
                    tbOut.Text += "Ход Игрока 2... ";

                    tbNumPlayer1.Text = "";

                    player1 = false;
                    player2 = true;

                    lbPlayer1.IsEnabled = false;
                    lbPlayer2.IsEnabled = true;
                    tbNumPlayer1.IsEnabled = false;
                    tbNumPlayer2.IsEnabled = true;
                }
            }
            else if (player2)
            {
                tbOut.Text += tbNumPlayer2.Text + " - быков: " + polnoeSovpadenie + ", коров: " + chastichnoeSovpadenie + "\n";
                if (polnoeSovpadenie == 4)
                {
                    tbOut.Text += "Число отгадано! Игрок 2 победил!";
                    lbPlayer2.IsEnabled = false;
                    tbNumPlayer2.IsEnabled = false;
                    butnCheck.IsEnabled = false;
                    return;
                }

                tbOut.Text += "Ход Игрока 1... ";

                tbNumPlayer2.Text = "";

                player1 = true;
                player2 = false;

                lbPlayer1.IsEnabled = true;
                lbPlayer2.IsEnabled = false;
                tbNumPlayer1.IsEnabled = true;
                tbNumPlayer2.IsEnabled = false;
            }
            else if (cpu)
            {
                tbOut.Text += numCPU + " - быков: " + polnoeSovpadenie + ", коров: " + chastichnoeSovpadenie + "\n";
                if (polnoeSovpadenie == 4)
                {
                    tbOut.Text += "Число отгадано! Компьютер победил!";
                    lbPlayer1.IsEnabled = false;
                    tbNumPlayer1.IsEnabled = false;
                    butnCheck.IsEnabled = false;
                    return;
                }

                tbOut.Text += "Ход Игрока 1... ";

                player1 = true;
                cpu = false;
            }
        }

    }

}