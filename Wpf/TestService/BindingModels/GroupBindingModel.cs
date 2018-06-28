﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TestModels;

namespace TestService.BindingModels
{
    [DataContract]
    public  class GroupBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public  List<User> Users { get; set; }
    }
}
