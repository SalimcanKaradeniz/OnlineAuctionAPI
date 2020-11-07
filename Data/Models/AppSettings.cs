﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public JwtConfiguration JwtConfiguration { get; set; }
    }

    public class ConnectionStrings
    {
        public string SQLConnection { get; set; }
    }

    public class JwtConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
    }
}