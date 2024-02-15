using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezebnebnik
{
        internal class Note
        {
            public string Title { get; set; }
            public string Description;
            public DateTime Data;

            public Note(string title, string description, DateTime date)
            {
                Title = title;
                Description = description;
                Data = date;
            }
        }
}
