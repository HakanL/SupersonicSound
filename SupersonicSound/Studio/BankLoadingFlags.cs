﻿using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(LOAD_BANK_FLAGS))]
    public enum BankLoadingFlags
        : uint
    {
        /// <summary>
        /// Standard behaviour.
        /// </summary>
        Normal = LOAD_BANK_FLAGS.NORMAL,

        /// <summary>
        /// Bank loading occurs asynchronously rather than occurring immediately.
        /// </summary>
        NonBlocking = LOAD_BANK_FLAGS.NONBLOCKING
    }
}
