﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfiModels.Data
{
    public class PortfolioDownload
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public Guid PortfolioId { get; set; } 

        [Required]
        [ForeignKey(nameof(PortfolioId))]
        public Portfolio Portfolio { get; set; } = null!;

    }
}
