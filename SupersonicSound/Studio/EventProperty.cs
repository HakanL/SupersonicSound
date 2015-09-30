﻿using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(EVENT_PROPERTY))]
    public enum EventProperty
    {
        /// <summary>
        /// Priority to set on low-level channels created by this event instance (-1 to 256).
        /// </summary>
        ChannelPriority = EVENT_PROPERTY.CHANNELPRIORITY,

        /// <summary>
        /// Schedule delay to synchronized playback for multiple tracks in DSP clocks, or -1 for default.
        /// </summary>
        ScheduleDelay = EVENT_PROPERTY.SCHEDULE_DELAY,

        /// <summary>
        /// Schedule look-ahead on the timeline in DSP clocks, or -1 for default.
        /// </summary>
        ScheduleLookahead = EVENT_PROPERTY.SCHEDULE_LOOKAHEAD
    }
}
