using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyScriptureJournal.Pages.Journals
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Journal> Journal { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string JournalBook { get; set; }
        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            // Use LINQ to get list of books.
            IQueryable<string> booksQuery = from j in _context.Journal
                                             orderby j.Book
                                             select j.Book;

            IQueryable<Journal> sortedJournal = from s in _context.Journal
                                                select s;

            var journals = from j in _context.Journal
                           select j;

            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (!string.IsNullOrEmpty(SearchString))
            {
                journals = journals.Where(s => s.Note.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(JournalBook))
            {
                journals = journals.Where(x => x.Book == JournalBook);
            }

            switch (sortOrder)
            {
                case "book_desc":
                    sortedJournal = sortedJournal.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    sortedJournal = sortedJournal.OrderBy(s => s.RecordDate);
                    break;
                case "date_desc":
                    sortedJournal = sortedJournal.OrderByDescending(s => s.RecordDate);
                    break;
                default:
                    sortedJournal = sortedJournal.OrderBy(s => s.Book);
                    break;
            }

            Books = new SelectList(await booksQuery.Distinct().ToListAsync());
            Journal = await sortedJournal.AsNoTracking().ToListAsync();
        }
    }
}
