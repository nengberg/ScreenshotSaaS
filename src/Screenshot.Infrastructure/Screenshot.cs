using System;

namespace Screenshot.Infrastructure
{
    public class Screenshot
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
    }
}