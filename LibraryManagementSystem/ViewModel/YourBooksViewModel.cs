using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class YourBooksViewModel : ViewModelBase
    {
        private ObservableCollection<YourBooksModel> _books;

        public ObservableCollection<YourBooksModel> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        public YourBooksViewModel()
        {
            // Initialize the Books collection with two dummy books
            Books = new ObservableCollection<YourBooksModel>
            {
                new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed
                 new YourBooksModel { BookID = 1, MinAge = 12, Name = "Book 1", Author = "Author 1", Category = "Fiction", Description = "Description 1" },
                new YourBooksModel { BookID = 2, MinAge = 15, Name = "Book 2", Author = "Author 2", Category = "Science", Description = "Description 2" },
                // Add more data as needed

                // Add more data as needed
            };
        }
    }
 }
