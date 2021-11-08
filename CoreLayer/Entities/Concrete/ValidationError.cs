namespace CoreLayer.Entities.Concrete
{
    public class ValidationError
    {
        public string PropertyName { get; set; } // CategoryName

        public string Message { get; set; } // CategoryName alani 100 karakterden büyük olmamalidir
    }
}
