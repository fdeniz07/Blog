using System;
using CoreLayer.Utilities.Results.ComplexTypes;

namespace CoreLayer.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } //ResultStatus.Success // ResultStatus.Error
        public string Message { get;}
        public Exception Exception { get;}
    }
}
