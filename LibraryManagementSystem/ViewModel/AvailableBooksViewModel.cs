﻿using LibraryManagementSystem.Model;
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
            List<AvailableBooksModel> localBooks = new List<AvailableBooksModel>();
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from book in context.Books
                            select new
                            {
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
    }
}
