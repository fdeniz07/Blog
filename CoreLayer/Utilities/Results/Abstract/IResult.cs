﻿using System;
using System.Collections.Generic;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results.ComplexTypes;

namespace CoreLayer.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } //ResultStatus.Success // ResultStatus.Error
        public string Message { get;}
        public Exception Exception { get;}

        public IEnumerable<ValidationError> ValidationErrors { get; set; } // ValidationErrors.Add --> Bu islem yapilamaz. Yani disaridan yenilemeyi IEnurable ile kapatiliyor.
    }
}
