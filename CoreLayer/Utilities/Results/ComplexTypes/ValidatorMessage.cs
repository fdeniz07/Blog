namespace CoreLayer.Utilities.Results.ComplexTypes
{
    public class ValidatorMessage
    {
        public string NotEmpty { get; } = " boş geçilmemelidir.";
        public string NotSmaller{ get; } = " karakterden küçük olmamalıdır.";
        public string NotBigger { get; } = " karakterden büyük olmamalıdır.";
        public string ValidFormat { get; } = " uygun formatta olmalıdır.";
    }
}
