using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.ViewModels.Posts.ViewModels
{
    public class AllPostViewModel
    {
        public List<IndexPostViewModel> Posts { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int NextPage
        {
            get
            {
                if (this.CurrentPage >= this.PagesCount)
                {
                    return 1;
                }

                return this.CurrentPage + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (this.CurrentPage <= 1)
                {
                    return this.PagesCount;
                }

                return this.CurrentPage - 1;
            }
        }
    }
}
