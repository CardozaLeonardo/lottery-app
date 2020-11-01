using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IConfigurable
    {
        bool Configured { get; }
        void Configure();
    }
}
