using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketSeasons
{
    public partial class Home : System.Web.UI.Page
    {
        private WebScrape _WebScrape = new WebScrape();
        protected void Page_Load(object sender, EventArgs e)
        {
            Test.Text = _WebScrape.ReadWeb("GOOG").ToString();
        }
        
    }
}