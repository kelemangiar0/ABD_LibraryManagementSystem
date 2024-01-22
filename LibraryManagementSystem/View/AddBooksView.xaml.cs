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

namespace LibraryManagementSystem.View
{
    public partial class AddBooksView : UserControl
    {
        public AddBooksView()
        {
            InitializeComponent();
        }
        private bool firstLetterCondition(string text)
        {
            return !string.IsNullOrEmpty(text) && char.IsUpper(text[0]);
        }
        private bool verifyConditions()
        {
            var emptyConditions = title.Text != "" && author.Text != "" && category.Text != "" && description.Text != "" && minimumAge.Text != "" && stock.Text != "";
            var numericConditions = int.TryParse(minimumAge.Text, out _) && int.TryParse(stock.Text, out _);
            var capitalLetterConditions = firstLetterCondition(title.Text) && firstLetterCondition(author.Text) && firstLetterCondition(category.Text) && firstLetterCondition(description.Text);
            return emptyConditions && numericConditions && capitalLetterConditions;
        }


        private void LoadData()
        {
            title.Clear();
            author.Clear();
            category.Clear();
            description.Clear();
            minimumAge.Clear();
            stock.Clear();
        }

        private void butonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(verifyConditions() != true)
                {
                    throw new Exception();
                }

                using (var context = new UncensoredLibraryEntities())
                {
                    var newBook = new Book
                    {
                        Name = title.Text,
                        Author = author.Text,
                        Category = category.Text,
                        Description = description.Text,
                        MinAge = int.Parse(minimumAge.Text),
                        Stock = int.Parse(stock.Text)
                    };

                    context.Books.Add(newBook);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show($"Book named {newBook.Name} added succesfully!");
                    
                }

            }
           catch(Exception)
            {
                MessageBox.Show("The details of the book you want to enter in the library are not valid!");
            }

           
        }
    }
}
