using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class AllStockViewModel : ViewModelBase
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

        public AllStockViewModel()
        {
            Refresh();
        }
        public void Refresh()
        {
            List<AvailableBooksModel> localBooks = new List<AvailableBooksModel>();
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from book in context.Books
                            select new
                            {
                                Stock = book.Stock,
                                BookID = book.BookID,
                                MinAge = book.MinAge,
                                Name = book.Name,
                                Author = book.Author,
                                Category = book.Category,
                                Description = book.Description
                            };
                foreach (var it in query)
                {
                    AvailableBooksModel result = new AvailableBooksModel
                    {
                        Stock = it.Stock ?? 0,
                        BookID = it.BookID,
                        MinAge = it.MinAge,
                        Author = it.Author,
                        Category = it.Category,
                        Description = it.Description,
                        Name = it.Name
                    };
                    localBooks.Add(result);
                }
            }
            Books = new ObservableCollection<AvailableBooksModel>(localBooks);
        }

        public void ApplyFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                Books = new ObservableCollection<AvailableBooksModel>(_books);
                return;
            }

            var filteredBooks = _books.Where(b =>
                b.Name.Contains(filter) ||
                b.Author.Contains(filter) ||
                b.Category.Contains(filter) ||
                b.Description.Contains(filter)
            ).ToList();

            Books = new ObservableCollection<AvailableBooksModel>(filteredBooks);
        }


    }
}
