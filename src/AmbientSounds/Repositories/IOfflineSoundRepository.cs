﻿using AmbientSounds.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmbientSounds.Repositories;

public interface IOfflineSoundRepository
{
    /// <summary>
    /// Retrieves list of sound data from storage.
    /// </summary>
    Task<IReadOnlyList<T>> GetLocalSoundsAsync<T>() where T : Sound;
    
    /// <summary>
    /// Saves the given list of sound meta data to storage.
    /// </summary>
    Task SaveLocalSoundsAsync(IList<Sound> sounds);
}