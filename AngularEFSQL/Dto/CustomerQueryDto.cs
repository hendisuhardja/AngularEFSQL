namespace AngularEFSQL.Dto
{
    public class CustomerQueryDto
    {
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string SortBy { get; set; }

        public bool IsSortAscending { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
