﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.Models.Base
{
    public abstract class IEntity
    {
        public int Id { get; set; }
    }
}