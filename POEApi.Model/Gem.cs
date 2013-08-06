﻿using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace POEApi.Model
{
    public class Gem : Item
    {
        public bool IsQuality { get; private set; }
        public int Quality { get; private set; }
        public List<Requirement> Requirements { get; set; }
        public int Socket { get; set; }
        public string Color { get; set; }

        public Gem(JSONProxy.Item item) : base(item)
        {
            this.Properties = ProxyMapper.GetProperties(item.Properties);
            this.ItemType = Model.ItemType.Gem;

            this.Socket = item.Socket;
            this.Color = item.Color;
            this.Requirements = ProxyMapper.GetRequirements(item.Requirements);

            if (!Properties.Any(p => p.Name == "Quality"))
                return;

            IsQuality = true;
            Quality = ProxyMapper.GetQuality(item.Properties);

            this.UniqueIDHash = base.getHash();
        }

        protected override int getConcreteHash()
        {
            var anonomousType = new
            {
                f1 = Quality,
                f2 = this.Requirements != null ? string.Join(string.Empty, this.Requirements.Select(r => string.Concat(r.Name, r.Value)).ToArray()) : string.Empty,
                f3 = Color,
                f4 = Socket
            };

            return anonomousType.GetHashCode();
        }
    }
}
