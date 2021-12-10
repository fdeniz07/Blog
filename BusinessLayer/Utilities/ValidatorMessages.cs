namespace BusinessLayer.Utilities
{
    public class ValidatorMessages
    {
        public string NotEmpty { get; } = " boş geçilmemelidir.";
        public string NotSmaller { get; } = " karakterden küçük olmamalıdır.";
        public string ValidFormat { get; } = " uygun formatta olmalıdır.";
        public string NotBigger { get; } = " karakterden büyük olmamalıdır.";
    }
}
