using Minimal_API_Movies.Utils;

namespace Minimal_API_Movies.DTOs
{
    public class PaginationDTO
    {
        public const int pageInitialValue = 1;
        public const int recordsPerPageInitialValue = 10;
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

        public static ValueTask<PaginationDTO> BindAsync(HttpContext context)
        {
            var page = context.ExtractValueOrDefault(nameof(Page), pageInitialValue);
            var recordsPerPage = context.ExtractValueOrDefault(nameof(RecordsPerPage), recordsPerPageInitialValue);
            var response = new PaginationDTO
            {
                Page = page,
                RecordsPerPage = recordsPerPage
            };
            return ValueTask.FromResult(response);
        }
    }
}
