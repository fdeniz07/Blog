using CoreLayer.Utilities.Results.ComplexTypes;

namespace CoreLayer.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
    }
}
