﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SoapEx1
{
    [ServiceContract]
    public interface IWhoAmI
    {
        [OperationContract]
        string Get();
    }
}