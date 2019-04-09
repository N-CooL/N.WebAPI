namespace LR.WebAPI.Models
{
    public class ResponseData<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }       
        public TEntity Entity { get; set; }
    }
}