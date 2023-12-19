using LibraryManagementSystem.Model;
using LibraryManagementSystem.View;
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

        public void RefreshBooks()
        {
            List<YourBooksModel> booksOwnedModels = new List<YourBooksModel>();
            int userID = findUserID(StudentWindow.username) ?? 0;
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from booksOwned in context.BooksOwneds
                            where booksOwned.UserID == userID
                            join book in context.Books on booksOwned.BookID equals book.BookID
                            select new YourBooksModel
                            {
                                BookID = book.BookID,
                                MinAge = book.MinAge,
                                Name = book.Name,
                                Author = book.Author,
                                Category = book.Category,
                                Description = book.Description
                            };

                booksOwnedModels = query.ToList();
              
            }

            Books = new ObservableCollection<YourBooksModel>(booksOwnedModels);
        }

        int? findUserID(string username)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from accounts in context.Accounts
                            where accounts.Username == username
                            select accounts.UserID;
                return query.SingleOrDefault();
            }

        }

        public YourBooksViewModel()
        {
            RefreshBooks();
        }
    }
 }
