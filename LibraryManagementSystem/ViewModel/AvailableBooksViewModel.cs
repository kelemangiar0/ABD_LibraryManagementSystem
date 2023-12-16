using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class AvailableBooksViewModel : ViewModelBase
    {
        private ObservableCollection<AvailableBooksModel> _books;

        public ObservableCollection<AvailableBooksModel> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        public AvailableBooksViewModel()
        {
            // Initialize the Books collection with two dummy books
            Books = new ObservableCollection<AvailableBooksModel>
            {
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },
                new AvailableBooksModel { BookID = 1, MinAge = 12, Name = "Book 3", Author = "Author 3", Category = "Fiction", Description = "Description 1" },
                new AvailableBooksModel { BookID = 2, MinAge = 15, Name = "Book 4", Author = "Author 4 ", Category = "Science", Description = "Description 2" },


                // Add more data as needed
            };
        }
    }
}
