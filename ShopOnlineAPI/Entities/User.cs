﻿namespace ShopOnlineAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Address
        {
            get;
            set;
        } = "ABC";

    }
}
