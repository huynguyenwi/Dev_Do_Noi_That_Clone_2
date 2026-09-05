namespace core.Models
{
    public class PagingModel
    {
        public PagingModel(int pagenumber = 1, int pagesize = 10, string search = "", int totalrows = 1, int rowstart = 1, int rowend = 1, string orderby = "", string callfunction = null)
        {
            this.offset = pagenumber;
            this.limit = pagesize;
            this.search = search;
            this.totalRow = totalrows;
            this.rowStart = rowstart;
            this.rowEnd = rowend;
            this.totalPage = this.CalcTotalPage();
            this.orderby = orderby;
            this.callFunction = callfunction;
        }

        public int offset { get; set; } = 1;
        public int limit { get; set; } = 10;
        public string search;
        public string orderby;
        public string callFunction;
        public int totalRow;
        public int rowStart;
        public int rowEnd;
        public int totalPage { get; set; }
        private int CalcTotalPage()
        {
            int totalpage = this.totalRow % this.limit == 0 ? (this.totalRow / this.limit) : (this.totalRow / this.limit) + 1;
            return totalpage;
        }
    }
}
