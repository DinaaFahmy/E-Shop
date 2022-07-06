namespace Shop.Models.Wrappers
{
        public class Page<T>
        {
            public int? NextPage { get; }
            public int PageNumber { get; }
            public int? PreviousPage { get; }
            public int PageSize { get; }
            public int RecordsTotalCount { get; }
            public int TotalPages { get; }
            public List<T> Records { get; }

            public Page(){}

            public Page(List<T> records, int pageNumber, int pageSize, long recordsTotalCount)
            {
                if (records.Count() == 0 && pageNumber > 0)
                    throw new Exception("You've reached last page");

                PageNumber = pageNumber;
                PageSize = pageSize;
                Records = records;
                RecordsTotalCount = (int)recordsTotalCount;
                
                TotalPages = (Records?.Count() > 0 && Records?.Count() == pageSize) ? (int)Math.Ceiling((double)(recordsTotalCount / Records?.Count())) : pageNumber;

                NextPage = (recordsTotalCount > pageSize && pageNumber != this.TotalPages) ? pageNumber + 1 : (int?)null;
                PreviousPage = (pageNumber > 0) ? pageNumber - 1 : (int?)null;
            }
        }
    }
