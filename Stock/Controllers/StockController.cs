// a stock service
// return stock market prices for various tickers

// Swagger (Open API) enabled (Install-Package Swashbuckle.AspNetCore)
// see Startup.cs for adding/configuring Swagger middleware
// /swagger/v1/swagger.json for swagger API doc, /swagger for UI page

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Controllers
{
    [Produces("application/json")]       // restrict to JSON response only (camelCasing)
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        /* 
          * GET /api/stock/all         get all stock listings               
          * GET /api/stock/IBM         get price last trade for IBM
          */

        // the listings on this stock market
        private List<StockListing> listings;            // todo: store in a db

        // initialise the listings collection, stateless
        public StockController()
        {
            listings = new List<StockListing>()
                {
                    new StockListing { TickerSymbol = "a", Msg ="These people like to be leaders. They have courage and inspiration, and they are active and creative. They give priority to individual success and have the ability to use their initiative and determination to succeed. They should devote more time to other people. It is good to praise them. They are domineering. They have good management skills and may achieve glory or high social status./nWeaknesses, what should be learned:/nBeware of superiority and stubbornness. These people are hesitant and may have difficulty in finding their own way. They get too little help and support from others. They are often lonely." },
                    new StockListing { TickerSymbol = "b", Msg = "These people are tolerant and kind. They have very good intuition that can be used during various activities. It is good to listen to others, be patient and learn the art of diplomacy. These people lack the courage to implement their plans and often leave initiative to other people. They require devotion from their partners. Relationships, friendships and emotional life are very important to them. They have the ability to reconcile people who are in dispute and they can successfully cooperate with others./nWeaknesses, what should be learned:/nPeople with this Life Path Number cling to their property and are often bothered by small problems. They may be passive and have the tendency to give in to circumstances. Beware of subordination and hypersensitivity. These people poorly cope with stress and become distracted and nervous if there is not peace and quiet around them. People with this Life Path Number often avoid any form of leadership and responsibility."  },
                    new StockListing { TickerSymbol = "c", Msg = "These people are mentally alert and have creative minds. They are creative and original, and they have a good imagination. They hate boredom and like to cooperate with others. They are charming and witty. They are critical of others and they should therefore learn how to be more diplomatic. They need to work freely and without restraints. They like working in pairs. They often work better under stress, which activates their mental energy. They usually succeed quickly. They need to be more ambitious and to open up more. They are inventive and imaginative in solving problems. They make new contacts easily, but they are sometimes superficial. They make decisions fast but they are not always the correct ones./nWeaknesses, what should be learned:/nBeware of restlessness, distractibility, bad temper, irritability, fear of dullness and uneven financial life. Beware of carelessness."  },
                    new StockListing { TickerSymbol = "d", Msg = "These people are successful at what they do thanks to their perseverance and systematic work. Consistency and order are very important to them. They will achieve success later in life by working hard. Patience is important. These people are practical and conservative. They do not like changes but they do not like monotonous life either. They are usually physically strong, so they prefer activities where they can use their physical strength and dexterity. They are hardworking. They need to be sure of their partner´s loyalty./nWeaknesses, what should be learned:/nBeware of carelessness and a narrow view of life. These people should be careful when they are making decisions. What they experience should not be turned inward because it will affect the nervous system; it is therefore important to release cumulated energy. These people might lack confidence."  },
                    new StockListing { TickerSymbol = "e", Msg = "These people need movement and change, and they like to travel. They search for freedom and adventure. They are open to new things and ideas. They have an inclination for leadership. They are ambitious but they are also very sensitive and avoid routine and boredom. They are impressed by people who are able to convince them in some area./nWeaknesses, what should be learned:/nThese people should be careful not to succumb to sudden ideas, instability, inclination to nervousness and restlessness. They should keep a balance in the emotional, professional and financial areas. They do not like rules and restrictions and cannot be forced to do something.Beware of accidents."  },
                    new StockListing { TickerSymbol = "f", Msg = "These people will have to make choices very often and it is necessary to make the right decisions in the face of emerging opportunities. Romantic relationship, family and home are the foundations for success. They like helping others. They need a partner who meets their expectations. They long for the harmonisation of their relationships. They are usually successful in their love life and marriage. However, it is necessary to find willingness to make compromises. They often have a talent for business and a sense for aesthetics and art. They should regularly take good care of their health. They emphasise the material side of life. It is very important to clarify what is good and what is bad, and understand that the purpose of challenges is to strengthen and check person´s determination. Recognition and love, that improve self-confidence are very important./nWeaknesses, what should be learned:/nBeware of hesitation, restlessness and intolerance. Beware of excessive desire for perfection and irritability."  },
                    new StockListing { TickerSymbol = "g", Msg = "These people prefer mental work. They want to realise their potential and work hard in order to improve themselves. They need friendship in their life. Their marriage can be complicated because they want to be independent. There are unexpected changes in their lives. Do not force anything. In order to succeed it is necessary to communicate with others, experience life, and develop self-confidence. Friendships and relationships play a major role. Implementation of material goals is difficult, even if money come in unexpected ways. They are often diligent students; they may have rich spiritual life or go on a long journey. They feel good in the countryside, in the mountains or by the sea. These people may have different healing talents e.g. physical, emotional or spiritual./nWeaknesses, what should be learned:/nWatch out for excessive pessimism and curtness.Sometimes this person is too proud and inaccessible.Beware of loneliness and lack of realism.Impartiality is important."  },
                    new StockListing { TickerSymbol = "h", Msg = "These people are ambitious and have a desire for power and money. Risky journeys will bring them success. The problem is sometimes a lack of courage, endurance and mental balance. They have good management and organizational skills. They are tough and persistent, but also conservative. It is important for them to understand how to maintain balance in life. They need to understand causes and consequences. It is important to keep in balance giving and receiving; material property will not bring peace or satisfaction if it does not benefit others. They have a strong character and determination to overcome obstacles./nWeaknesses, what should be learned:/nThese people have to be careful not to abuse their power because it can have serious negative consequences.It is good if they learn to wait for their success, otherwise there is a risk of many failures; aggression; toughness; intolerance and impatience.They should not lose a sense of decency and respect for others otherwise there can be financial or legal problems.They think highly of themselves and they are picky and moody.Accidents or health problems may slow down their development."  },
                    new StockListing { TickerSymbol = "i", Msg = "These people are idealistic. They have an idealistic approach towards themselves and towards their surroundings. They will go on a journey that will help them gain experience and meet important people. They have a high level of mental energy and they are be able to handle all the difficulties and challenges. It is good if they become more devoted to their goals and become more sensitive and courageous. They are responsible. Sometimes they do not realise the depth of their wisdom. They have no need for high material security. Love, the truth and friendships are very important. They have a strong need to give much of themselves to others. At mature age they will sometimes experience unexpected success and the realisation of big plans. Broad communication with the public or with people abroad; they learn easily./nWeaknesses, what should be learned:/nBeware of the tendency to have delusions, be extremely emotional, and have moody and exaggerated reactions.Beware of tendency to emotional stress and mental excesses."  }

                };
        }

        // GET api/stock/all
        //[HttpGet("all")]
        //public IEnumerable<StockListing> GetAllListings()
        //{
        //    return listings.OrderBy(s => s.TickerSymbol);         // 200 OK, listings serialized in response body, camelCasing
        //}

        // GET api/stock/GOOG 
        [HttpGet("{ticker:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]          // for Swagger doc/UI
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<double> GetStockPrice(String ticker)
        {
            // LINQ query, find matching ticker (case-insensitive) or default value (null) if none matching
            StockListing listing = listings.SingleOrDefault(l => l.TickerSymbol.ToUpper() == ticker.ToUpper());
            if (listing == null)
            {
                return NotFound();
            }
            return Ok(listing.Msg);
        }

        
    }
}

