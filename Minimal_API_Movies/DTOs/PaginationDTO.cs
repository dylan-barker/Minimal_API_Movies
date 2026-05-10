namespace Minimal_API_Movies.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; }
        private int recordsPerPage = 10;
        private readonly int maxRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                if (value > recordsPerPage)
                {
                    recordsPerPage = maxRecordsPerPage;
                }
                else
                {
                    recordsPerPage = value;
                }
            }
        }
    }
}
