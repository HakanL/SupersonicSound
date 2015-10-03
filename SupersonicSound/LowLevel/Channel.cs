﻿using System;
using System.Collections.Generic;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct Channel
        : IEquatable<Channel>, IChannelControl
    {
        public FMOD.Channel FmodChannel { get; private set; }

        private CallbackHandler _callbackHandler;

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

        private bool _throwStolen;
        public bool SuppressChannelStolen
        {
            get { return !_throwStolen; }
            set { _throwStolen = !value; }
        }

        private Channel(FMOD.Channel channel)
            : this()
        {
            FmodChannel = channel;
        }

        public static Channel FromFmod(FMOD.Channel channel)
        {
            if (channel == null)
                throw new ArgumentNullException("channel");
            return new Channel(channel);
        }

        #region equality
        public bool Equals(Channel other)
        {
            return other.FmodChannel == FmodChannel;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Channel))
                return false;

            return Equals((Channel)obj);
        }

        public override int GetHashCode()
        {
            return (FmodChannel != null ? FmodChannel.GetHashCode() : 0);
        }
        #endregion

        #region Channel specific control functionality
        public float? Frequency
        {
            get
            {
                float freq;
                return FmodChannel.getFrequency(out freq).CheckBox(freq, ErrorChecking.Suppress(_throwHandle, _throwStolen));
            }
            set
            {
                FmodChannel.setFrequency(value.Unbox()).Check(ErrorChecking.Suppress(_throwHandle, _throwStolen));
            }
        }

        public int? Priority
        {
            get
            {
                int priority;
                return FmodChannel.getPriority(out priority).CheckBox(priority, ErrorChecking.Suppress(_throwHandle, _throwStolen));
            }
            set
            {
                FmodChannel.setPriority(value.Unbox()).Check();
            }
        }

        public void SetPosition(uint position, TimeUnit unit)
        {
            FmodChannel.setPosition(position, (TIMEUNIT)unit).Check();
        }

        public uint GetPosition(TimeUnit unit)
        {
            uint pos;
            FmodChannel.getPosition(out pos, (TIMEUNIT)unit).Check();
            return pos;
        }

        public ChannelGroup ChannelGroup
        {
            get
            {
                FMOD.ChannelGroup group;
                FmodChannel.getChannelGroup(out group).Check();
                return ChannelGroup.FromFmod(group);
            }
            set
            {
                FmodChannel.setChannelGroup(value.FmodGroup).Check();
            }
        }

        public int LoopCount
        {
            get
            {
                int count;
                FmodChannel.getLoopCount(out count).Check();
                return count;
            }
            set
            {
                FmodChannel.setLoopCount(value).Check();
            }
        }

        public void SetLoopPoints(uint start, TimeUnit startUnit, uint end, TimeUnit endUnit)
        {
            FmodChannel.setLoopPoints(start, (TIMEUNIT)startUnit, end, (TIMEUNIT)endUnit);
        }

        public void GetLoopPoints(out uint start, TimeUnit startUnit, out uint end, TimeUnit endUnit)
        {
            FmodChannel.getLoopPoints(out start, (TIMEUNIT)startUnit, out end, (TIMEUNIT)endUnit);
        }

        public void Stop()
        {
            FmodChannel.stop().Check(ErrorChecking.SuppressInvalidHandle);
        }

        public bool Pause
        {
            get
            {
                bool value;
                FmodChannel.getPaused(out value).Check();

                return value;
            }
            set
            {
                FmodChannel.setPaused(value).Check();
            }
        }

        public float Volume
        {
            get
            {
                float value;
                FmodChannel.getVolume(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setVolume(value).Check();
            }
        }

        public bool VolumeRamp
        {
            get
            {
                bool value;
                FmodChannel.getVolumeRamp(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setVolumeRamp(value).Check();
            }
        }

        public float Audibility
        {
            get
            {
                float value;
                FmodChannel.getAudibility(out value).Check();
                return value;
            }
        }

        public float Pitch
        {
            get
            {
                float value;
                FmodChannel.getPitch(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setPitch(value).Check();
            }
        }

        public bool Mute
        {
            get
            {
                bool value;
                FmodChannel.getMute(out value).Check();

                return value;
            }
            set
            {
                FmodChannel.setMute(value).Check();
            }
        }

        public float Pan
        {
            set
            {
                FmodChannel.setPan(value).Check();
            }
        }

        public bool IsPlaying
        {
            get
            {
                bool value;
                FmodChannel.isPlaying(out value).Check();

                return value;
            }
        }

        public Mode Mode
        {
            get
            {
                MODE value;
                FmodChannel.getMode(out value).Check();
                return (Mode)value;
            }
            set
            {
                FmodChannel.setMode((MODE)value).Check();
            }
        }

        public float LowPassGain
        {
            get
            {
                float value;
                FmodChannel.getLowPassGain(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setLowPassGain(value).Check();
            }
        }
        #endregion

        #region Information only functions
        public bool IsVirtual
        {
            get
            {
                bool virt;
                FmodChannel.isVirtual(out virt).Check();
                return virt;
            }
        }

        public Sound CurrentSound
        {
            get
            {
                FMOD.Sound sound;
                FmodChannel.getCurrentSound(out sound).Check();
                return Sound.FromFmod(sound);
            }
        }

        public int Index
        {
            get
            {
                int index;
                FmodChannel.getIndex(out index).Check();
                return index;
            }
        }
        #endregion

        #region Callback functions
        public void SetCallback(Action<ChannelControlCallbackType, IntPtr, IntPtr> callback)
        {
            if (_callbackHandler == null)
                _callbackHandler = new CallbackHandler(FmodChannel);
            _callbackHandler.SetCallback(callback);
        }

        public void RemoveCallback()
        {
            _callbackHandler.RemoveCallback();
        }
        #endregion
    }
}
