﻿using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    internal class MapDto : IDto<int>
    {
        public string NameKey { get; set; }
        public byte[] Grid { get; set; }
        public int Id { get; set; }
    }
}