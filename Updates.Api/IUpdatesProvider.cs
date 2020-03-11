﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updates.Api
{
    public interface IUpdatesProvider
    {
        Task<IEnumerable<IUpdate>> GetUpdates(long authorId);
    }
}