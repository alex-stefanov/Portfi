﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfiModels.Data
{
    public class PortfolioDownload
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid PortfolioId { get; set; }
        
    }
}
