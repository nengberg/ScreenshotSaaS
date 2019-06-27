using System;

namespace Screenshot.Domain
{
    public class Screenshot
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
    }
}