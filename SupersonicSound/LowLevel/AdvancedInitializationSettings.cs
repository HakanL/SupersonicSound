﻿using FMOD;
using System.Runtime.InteropServices;

namespace SupersonicSound.LowLevel
{
    public struct AdvancedInitializationSettings
    {
        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  MPEG   codecs consume 30,528 bytes per instance and this number will determine how many MPEG   channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxMpegCodecs;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  ADPCM  codecs consume  3,128 bytes per instance and this number will determine how many ADPCM  channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxAdpcmCodecs;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  XMA    codecs consume 14,836 bytes per instance and this number will determine how many XMA    channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxXmaCodecs;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  CELT   codecs consume 25,408 bytes per instance and this number will determine how many CELT   channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxCeltCodecs;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  Vorbis codecs consume 23,256 bytes per instance and this number will determine how many Vorbis channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxVorbisCodecs;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  AT9    codecs consume  8,720 bytes per instance and this number will determine how many AT9    channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxAT9Codecs;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Number of channels available on the ASIO device.
        /// </summary>
        public int ASIONumChannels;

        //public IntPtr ASIOChannelList;             /* [r/w] Optional. Specify 0 to ignore. Pointer to an array of strings (number of entries defined by ASIONumChannels) with ASIO channel names. */

        //public IntPtr ASIOSpeakerList;             /* [r/w] Optional. Specify 0 to ignore. Pointer to a list of speakers that the ASIO channels map to.  This can be called after System::init to remap ASIO output. */

        /// <summary>
        /// [r/w] Optional. For use with FMOD_INIT_HRTF_LOWPASS.  The angle range (0-360) of a 3D sound in relation to the listener, at which the HRTF function begins to have an effect. 0 = in front of the listener. 180 = from 90 degrees to the left of the listener to 90 degrees to the right. 360 = behind the listener. Default = 180.0.
        /// </summary>
        public float HrtfMinAngle;

        /// <summary>
        /// [r/w] Optional. For use with FMOD_INIT_HRTF_LOWPASS.  The angle range (0-360) of a 3D sound in relation to the listener, at which the HRTF function has maximum effect. 0 = front of the listener. 180 = from 90 degrees to the left of the listener to 90 degrees to the right. 360 = behind the listener. Default = 360.0.
        /// </summary>
        public float HrtfMaxAngle;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_HRTF_LOWPASS.  The cutoff frequency of the HRTF's lowpass filter function when at maximum effect. (i.e. at HRTFMaxAngle).  Default = 4000.0.
        /// </summary>
        public float HrtfFreq;

        /// <summary>
        ///  [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_VOL0_BECOMES_VIRTUAL.  If this flag is used, and the volume is below this, then the sound will become virtual.  Use this value to raise the threshold to a different point where a sound goes virtual.
        /// </summary>
        public float Vol0VirtualVol;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For streams. This determines the default size of the double buffer (in milliseconds) that a stream uses.  Default = 400ms
        /// </summary>
        public uint DefaultDecodeBufferSize;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_ENABLE_PROFILE.  Specify the port to listen on for connections by the profiler application.
        /// </summary>
        public ushort ProfilePort;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. The maximum time in miliseconds it takes for a channel to fade to the new level when its occlusion changes.
        /// </summary>
        public uint GeometryMaxFadeTime;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_DISTANCE_FILTERING.  The default center frequency in Hz for the distance filtering effect. Default = 1500.0.
        /// </summary>
        public float DistanceFilterCenterFreq;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Out of 0 to 3, 3d reverb spheres will create a phyical reverb unit on this instance slot.  See FMOD_REVERB_PROPERTIES.
        /// </summary>
        public int Reverb3Dinstance;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Number of buffers in DSP buffer pool.  Each buffer will be DSPBlockSize * sizeof(float) * SpeakerModeChannelCount.  ie 7.1 @ 1024 DSP block size = 8 * 1024 * 4 = 32kb.  Default = 8.
        /// </summary>
        public int DSPBufferPoolSize;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the stack size for the FMOD Stream thread in bytes.  Useful for custom codecs that use excess stack.  Default 49,152 (48kb)
        /// </summary>
        public uint StackSizeStream;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the stack size for the FMOD_NONBLOCKING loading thread.  Useful for custom codecs that use excess stack.  Default 65,536 (64kb)
        /// </summary>
        public uint StackSizeNonBlocking;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the stack size for the FMOD mixer thread.  Useful for custom dsps that use excess stack.  Default 49,152 (48kb)
        /// </summary>
        public uint StackSizeMixer;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Resampling method used with fmod's software mixer.  See FMOD_DSP_RESAMPLER for details on methods.
        /// </summary>
        public uint ResamplerMethod;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the command queue size for thread safe processing.  Default 2048 (2kb)
        /// </summary>
        public uint CommandQueueSize;

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Seed value that FMOD will use to initialize its internal random number generators.
        /// </summary>
        public uint RandomSeed;

        internal ADVANCEDSETTINGS ToFmod()
        {
            return new ADVANCEDSETTINGS
            {
                cbSize = Marshal.SizeOf(typeof(ADVANCEDSETTINGS)),
                maxMPEGCodecs = MaxMpegCodecs,
                maxADPCMCodecs = MaxAdpcmCodecs,
                maxXMACodecs = MaxXmaCodecs,
                maxCELTCodecs = MaxCeltCodecs,
                maxVorbisCodecs = MaxVorbisCodecs,
                maxAT9Codecs = MaxAT9Codecs,
                ASIONumChannels = ASIONumChannels,
                HRTFMinAngle = HrtfMinAngle,
                HRTFMaxAngle = HrtfMaxAngle,
                HRTFFreq = HrtfFreq,
                vol0virtualvol = Vol0VirtualVol,
                defaultDecodeBufferSize = DefaultDecodeBufferSize,
                profilePort = ProfilePort,
                geometryMaxFadeTime = GeometryMaxFadeTime,
                distanceFilterCenterFreq = DistanceFilterCenterFreq,
                reverb3Dinstance = Reverb3Dinstance,
                DSPBufferPoolSize = DSPBufferPoolSize,
                stackSizeStream = StackSizeStream,
                stackSizeNonBlocking = StackSizeNonBlocking,
                stackSizeMixer = StackSizeMixer,
                resamplerMethod = ResamplerMethod,
                commandQueueSize = CommandQueueSize,
                randomSeed = RandomSeed
            };
        }
    }
}
