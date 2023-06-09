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
using System.Windows.Shapes;
using System.Reflection;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Shop.Controllers;
using Shop.Models;



namespace Shop.View
{
    public partial class OrderReceipt : Window
    {
        public Order order { get; set; }
        public OrderReceipt(Order order)
        {
            InitializeComponent();
            DateTime now = DateTime.Now;
            this.order = order;
            double sum = 0;
            string str =
                "=============================================\n" +
                "Cashier: " + App.Fio_Session + "\n" +
                "=============================================\n" +
                "Date: " + now.ToString("HH:mm:ss dd.MM.yyyy") + "\n" +
                "=============================================\n" +
                "Fio: " + order.Fio_client + "\n" +
                "Phone" + order.Phone_client + "\n" +
                "Email: " + order.Email_client + "\n" +
                "Address: " + order.Address_client + "\n" +
                "Status: " + Utils.StatusToString(order.status) + "\n" +
                "=============================================\n" +
                "Product count: " + order.products.Count + "\n" +
                "Products:\n";
            foreach (Product item in order.products)
            {
                str += "\tTitle: " + item.Title + "\n" +
                    "\tCount: " + item.Count + "\n" +
                    "\tPrice: " + item.Price + "\n\n";
                sum += item.Count * item.Price;
            }
            str += "Total price: " + Math.Round(sum, 2) + "\n" +
                "=============================================\n";
            receipt.Text = str;
        }

        private void Print_btn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument(new System.Windows.Documents.Paragraph(new Run(receipt.Text)));
                document.PagePadding = new Thickness(50);

                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Текст на печать");
            }
        }

        private void PrintToPDF_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF File";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        Document document = new Document();
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);
                        document.Open();
                        document.Add(new iTextSharp.text.Paragraph(receipt.Text));
                        document.Close();
                    }

                    MessageBox.Show("PDF file saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while saving the PDF file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PrintToTXT_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.Title = "Save Text File";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    File.WriteAllText(filePath, receipt.Text);
                    MessageBox.Show("Text saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while saving the file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
