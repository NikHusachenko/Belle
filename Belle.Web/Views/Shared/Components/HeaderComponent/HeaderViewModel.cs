﻿namespace Belle.Web.Views.Shared.Components.HeaderComponent
{
    public class HeaderViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string Login { get; set; }
        public double WalletBalance { get; set; }
    }
}