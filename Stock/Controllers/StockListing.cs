using System;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class StockListing
    {
        // ticker symbol e.g. AAPL, GOOG, IBM, MSFT
        [Required]
        public string TickerSymbol
        {
            get;
            set;
        }

        // price last trade in $
       
        public string Msg
        {
            get;
            set;
        }

        // todo: add low price, high price, volume etc.

    }
}

